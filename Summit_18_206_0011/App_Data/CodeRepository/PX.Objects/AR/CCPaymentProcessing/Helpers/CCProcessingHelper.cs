﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using PX.CCProcessingBase;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.CA;

namespace PX.Objects.AR.CCPaymentProcessing.Helpers
{
	public static class CCProcessingHelper
	{
		#region Processing Center Methods

		public static IEnumerable GetPMdetails(PXGraph graph, int? PMInstanceID, bool CCPIDCondition, bool OtherDetailsCondition)
		{
			foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> res in PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>,
				And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>,
					And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>,
				Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Select(graph, PMInstanceID))
			{
				PaymentMethodDetail pmd = res;
				bool isCCPid = pmd.IsCCProcessingID == true;
				if (CCPIDCondition && isCCPid
					|| OtherDetailsCondition && !isCCPid)
				{
					yield return res;
				}
			}
		}

		public static string GetExpirationDateFormat(PXGraph graph, string ProcessingCenterID)
		{
			PXResultset<CCProcessingCenter> pc = PXSelectJoin<CCProcessingCenter, LeftJoin<CCProcessingCenterDetail,
				On<CCProcessingCenterDetail.processingCenterID, Equal<CCProcessingCenter.processingCenterID>,
					And<CCProcessingCenterDetail.detailID, Equal<Required<CCProcessingCenterDetail.detailID>>>>>,
				Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>
				.Select(graph, InterfaceConstants.ExpDateFormatDetailID, ProcessingCenterID);
			if (pc.Count == 0)
			{
				return null;
			}
			CCProcessingCenterDetail detail = pc[0].GetItem<CCProcessingCenterDetail>();
			if (string.IsNullOrEmpty(detail.DetailID))
			{
				return null;
			}
			return detail.Value;
		}
		
		public static bool IsCCPIDFilled(PXGraph graph, int? PMInstanceID)
		{
			if (PMInstanceID == null || PMInstanceID.Value < 0)
				return false;

			CustomerPaymentMethod cpm = PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID,
				Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Select(graph, PMInstanceID);

			if (cpm == null)
				return false;

			PXResultset<PaymentMethodDetail> paymentMethodDetail = PXSelectJoin<PaymentMethodDetail, LeftJoin<CustomerPaymentMethodDetail,
				On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>,
					And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>,
						And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>,
						And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>>>>,
				Where<PaymentMethodDetail.isCCProcessingID, Equal<True>,
					And<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>>>>.Select(graph, PMInstanceID, cpm.PaymentMethodID);

			PaymentMethodDetail pmIDDetail = paymentMethodDetail.Count > 0 ? paymentMethodDetail[0].GetItem<PaymentMethodDetail>() : null;
			CustomerPaymentMethodDetail ccpIDDetail = paymentMethodDetail.Count > 0 ? paymentMethodDetail[0].GetItem<CustomerPaymentMethodDetail>() : null;

			if (IsTokenizedPaymentMethod(graph, PMInstanceID) && pmIDDetail == null)
			{
				throw new PXException(Messages.PaymentMethodNotConfigured);
			}
			return ccpIDDetail != null && !string.IsNullOrEmpty(ccpIDDetail.Value);
		}

		public static bool IsTokenizedPaymentMethod(PXGraph graph, int? PMInstanceID)
		{
			return IsFeatureSupported(graph, PMInstanceID, CCProcessingFeature.ProfileManagement);
		}

		public static bool IsHFPaymentMethod(PXGraph graph, int? PMInstanceID)
		{
			CustomerPaymentMethod current = GetCustomerPaymentMethod(graph, PMInstanceID);
			if (current == null)
				return false;
			CCProcessingCenter processingCenter = GetProcessingCenter(graph, current.CCProcessingCenterID);
			return CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.HostedForm) && (processingCenter.AllowDirectInput != true);
		}

		public static bool IsFeatureSupported(PXGraph graph, int? PMInstanceID, CCProcessingFeature FeatureName)
		{
			CustomerPaymentMethod current = GetCustomerPaymentMethod(graph, PMInstanceID);
			if (current == null)
				return false;
			CCProcessingCenter processingCenter = GetProcessingCenter(graph, current.CCProcessingCenterID);
			return CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, FeatureName);
		}

		public static CustomerPaymentMethod GetCustomerPaymentMethod(PXGraph graph, int? PMInstanceID)
		{
			CustomerPaymentMethod current = null;
			if (PMInstanceID != null)
			{
				current = PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Select(graph, PMInstanceID);
			}
			if (current == null)
			{
				//assumin that payment method was just deleted
				IEnumerator cpmEnumerator = graph.Caches[typeof(CustomerPaymentMethod)].Deleted.GetEnumerator();
				if (cpmEnumerator.MoveNext())
				{
					current = (CustomerPaymentMethod)cpmEnumerator.Current;
				}
			}
			return current;
		}

		public static CCProcessingCenter GetProcessingCenter(PXGraph graph, string ProcessingCenterID)
		{
			return PXSelect<CCProcessingCenter, Where<CCProcessingCenter.processingCenterID, Equal<Required<CCProcessingCenter.processingCenterID>>>>.Select(graph, ProcessingCenterID);
		}

		public static bool? CCProcessingCenterNeedsExpDateUpdate(PXGraph graph, CCProcessingCenter ProcessingCenter)
		{
			if (CCProcessingFeatureHelper.IsFeatureSupported(ProcessingCenter, CCProcessingFeature.ProfileManagement))
			{
				PXResultset<CustomerPaymentMethod> unupdatedCpms = PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.cCProcessingCenterID,
					Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>, And<CustomerPaymentMethod.expirationDate, IsNull>>>.Select(graph, ProcessingCenter.ProcessingCenterID);
				return unupdatedCpms.Count != 0;
			}
			return null;
		}

		public static string GetTokenizedPMsString(PXGraph graph)
		{
			List<CCProcessingCenter> tokenizedPCs = new List<CCProcessingCenter>();
			HashSet<string> pmSet = new HashSet<string>();
			foreach (CCProcessingCenter pc in PXSelect<CCProcessingCenter, Where<CCProcessingCenter.isActive, Equal<True>>>.Select(graph))
			{
				if (CCProcessingFeatureHelper.IsFeatureSupported(pc, CCProcessingFeature.ProfileManagement)
					&& CCProcessingCenterNeedsExpDateUpdate(graph, pc) != false)
				{
					tokenizedPCs.Add(pc);
				}
			}

			foreach (CCProcessingCenter pc in tokenizedPCs)
			{
				foreach (PXResult<CustomerPaymentMethod, PaymentMethod> tokenizedPM in PXSelectJoinGroupBy<CustomerPaymentMethod,
					InnerJoin<PaymentMethod, On<CustomerPaymentMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>>,
					Where<CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>>,
					Aggregate<GroupBy<CustomerPaymentMethod.paymentMethodID>>>.Select(graph, pc.ProcessingCenterID))
				{
					PaymentMethod pm = tokenizedPM;
					pmSet.Add(pm.Descr);
				}
			}

			if (pmSet.Count == 0)
			{
				return string.Empty;
			}

			StringBuilder sb = new StringBuilder();

			foreach (string descr in pmSet)
			{
				if (sb.Length > 0)
				{
					sb.Append(", ");
				}
				sb.Append(descr);
			}

			return sb.ToString();
		}
		#endregion

		#region Multi Credit Card Methods

		public static bool IsCreditCardCountEnough(int creditCardCount, int limit)
		{
			return creditCardCount != 0 && creditCardCount % limit == 0;
		}

		public static int CustomerProfileCountPerCustomer(PXGraph graph, int? aBAccountID, string aCCProcessingCenterID)
		{
			PXResult<CustomerProcessingCenterID> result =
				PXSelectGroupBy<CustomerProcessingCenterID,
				Where<CustomerProcessingCenterID.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>,
					And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>>>,
				Aggregate<Count<CustomerProcessingCenterID.customerCCPID>>>
					.Select(graph, aBAccountID, aCCProcessingCenterID);
			int customerProfileCount = result.RowCount ?? 0;
			return customerProfileCount;
		}
		public static string BuildPrefixForCustomerCD(int customerProfileCount, CCProcessingCenter processingCenter)
		{
			return (customerProfileCount + 1) + "__";
		}
		#endregion
		public static string ExtractStreetAddress(IAddressBase aAddress)
		{
			string result = aAddress.AddressLine1;
			if (string.IsNullOrEmpty(aAddress.AddressLine1))
			{
				if (!string.IsNullOrEmpty(result))
					result += " ";
				result += aAddress.AddressLine1;
			}
			if (string.IsNullOrEmpty(aAddress.AddressLine2))
			{
				if (!string.IsNullOrEmpty(result))
					result += " ";
				result += aAddress.AddressLine2;
			}
			return result;
		}
	}
}
