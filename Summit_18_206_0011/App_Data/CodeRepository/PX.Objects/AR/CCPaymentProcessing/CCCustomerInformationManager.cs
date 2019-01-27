using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Compilation;
using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using PX.Objects.CR;
using PX.Objects.SO;
namespace PX.Objects.AR.CCPaymentProcessing
{
	public class CCCustomerInformationManager
	{
		private CCProcessingFeature _feature;
		private ICCPaymentProcessingRepository _repository;
		private IBaseProfileProcessingWrapper _profileProcessingWrapper => BaseProfileProcessingWrapper.GetBaseProfileProcessingWrapper(_pluginObject, _context);
		private IHostedFromProcessingWrapper _hfProcessor => HostedFromProcessingWrapper.GetBaseProfileProcessingWrapper(_pluginObject, _context);
		private IExtendedProfileProcessingWrapper _extendedProfileProcessor => ExtendedProfileProcessingWrapper.GetExtendedProfileProcessingWrapper(_pluginObject, _context);
		private CCProcessingContext _context;
		private PXGraph ReadingGraph => _repository.Graph;
		private object _pluginObject;

		protected int? PMInstanceID
		{
			set { _context.aPMInstanceID = value; }
		}

		protected int? CustomerID
		{
			set { _context.aCustomerID = value; }
		}

		protected string PrefixForCustomerCD
		{
			set { _context.PrefixForCustomerCD = value; }
		}

		protected PXGraph CallerGraph
		{
			set { _context.callerGraph = value; }
		}

		protected String2DateConverterFunc String2DateConverter
		{
			set { _context.expirationDateConverter = value; }
		}

		private object InitializePlugin(CCProcessingCenter aProcCenter)
		{
			object plugin = null;
			try
			{
				Type processorType = PXBuildManager.GetType(aProcCenter.ProcessingTypeName, true);
				plugin = Activator.CreateInstance(processorType);
			}
			catch (Exception)
			{
				throw new PXException(Messages.ERR_ProcessingCenterTypeInstanceCreationFailed, aProcCenter.ProcessingTypeName, aProcCenter.ProcessingCenterID);
			}
			return plugin;
		}

		protected CCCustomerInformationManager(string processingCenterID, CCProcessingFeature feature)
		{
			_feature = feature;
			_context = new CCProcessingContext();
			_repository = CCPaymentProcessingRepository.GetCCPaymentProcessingRepository();
			CCProcessingCenter processingCenter = _repository.GetCCProcessingCenter(processingCenterID);
			CCProcessingFeatureHelper.CheckProcessing(processingCenter, _feature, _context);
			_pluginObject = InitializePlugin(_repository.GetCCProcessingCenter(processingCenterID));
		}

		protected virtual string CreateCustomerProfile()
		{
			return _profileProcessingWrapper.CreateCustomerProfile();
		}

		protected virtual string CreatePaymentProfile()
		{
			return _profileProcessingWrapper.CreatePaymentProfile();
		}

		protected virtual CreditCardData GetPaymentProfile()
		{
			return _profileProcessingWrapper.GetPaymentProfile();
		}

		protected virtual void DeletePaymentProfile()
		{
			_profileProcessingWrapper.DeletePaymentProfile();
		}

		protected virtual IEnumerable<CreditCardData>  GetAllPaymentProfiles()
		{
			return _extendedProfileProcessor.GetAllPaymentProfiles();
		}

		protected virtual void GetCreatePaymentProfileForm()
		{
			_hfProcessor.GetCreateForm();
		}

		protected virtual IEnumerable<CreditCardData> GetMissingPaymentProfiles()
		{
			return _hfProcessor.GetMissingPaymentProfiles();
		}

		protected virtual void GetManagePaymentProfileForm()
		{
			_hfProcessor.GetManageForm();
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetCreatePaymentProfileForm(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter) instead.")]
		public static void GetCreatePaymentProfileForm<TPaymentMethodType>(PXGraph graph,
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			GenericCustomerPaymentMethodAdaper<TPaymentMethodType> adapter = new GenericCustomerPaymentMethodAdaper<TPaymentMethodType>(customerPaymentMethodView);
			GetCreatePaymentProfileForm(graph,adapter);
		}

		public static void GetCreatePaymentProfileForm(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter)
		{	
			if (graph == null || customerPaymentMethodAdapter == null)
				return;
			CustomerPaymentMethod customerPaymentMethod = customerPaymentMethodAdapter.Current;
			CCCustomerInformationManager cim = new CCCustomerInformationManager(customerPaymentMethod.CCProcessingCenterID, CCProcessingFeature.HostedForm)
			{
				CustomerID = customerPaymentMethod.BAccountID,
				PMInstanceID = customerPaymentMethod.PMInstanceID,
				CallerGraph = graph
			};
		
			string id = customerPaymentMethod.CustomerCCPID;
			if (id == null )
			{
				id = cim.CreateCustomerProfile();
				CustomerPaymentMethod cpm = customerPaymentMethodAdapter.Current.CreateCopy(customerPaymentMethodAdapter.Cache);
				cpm.CustomerCCPID = id;
				customerPaymentMethodAdapter.Cache.Update(cpm);
			}
			var processingCenter = cim._context.processingCenter;
			if (processingCenter.CreateAdditionalCustomerProfiles == true)
			{
				int customerProfileCount = CCProcessingHelper.CustomerProfileCountPerCustomer(graph, 
					customerPaymentMethod.BAccountID, 
					customerPaymentMethod.CCProcessingCenterID); // Total customer profile count per customer
			
				var cardLimit = processingCenter.CreditCardLimit;
				if (cardLimit != null && cardLimit > 0)
				{
					int allPaymentProfileCount = cim.GetAllPaymentProfiles().Count();
					if (CCProcessingHelper.IsCreditCardCountEnough(allPaymentProfileCount, cardLimit.Value))
					{
						cim.PrefixForCustomerCD = CCProcessingHelper.BuildPrefixForCustomerCD(customerProfileCount, processingCenter);
						id = cim.CreateCustomerProfile();
						CustomerPaymentMethod cpm = customerPaymentMethodAdapter.Current.CreateCopy(customerPaymentMethodAdapter.Cache);
						cpm.CustomerCCPID = id;
						customerPaymentMethodAdapter.Cache.Update(cpm);
					}
				}
			}
			cim.GetCreatePaymentProfileForm();
		}

		public static PXResultset<CustomerPaymentMethodDetail> GetAllCustomersCardsInProcCenter(PXGraph graph, int? BAccountID, string CCProcessingCenterID)
		{
			return PXSelectJoin<CustomerPaymentMethodDetail,
				InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>,
						And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>>>,
					InnerJoin<CustomerPaymentMethod,
						On<CustomerPaymentMethodDetail.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>>>,
				Where<CustomerPaymentMethod.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>,
					And<CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>,
						And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>>.Select(graph, BAccountID, CCProcessingCenterID);
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetNewPaymentProfiles(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter...) instead.")]
		public static void GetNewPaymentProfiles<TPaymentMethodType, TDetialsType>(PXGraph graph, 
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			PXSelectBase<TDetialsType> detailsView, 
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			ICustomerPaymentMethodAdapter adapter = new GenericCustomerPaymentMethodAdaper<TPaymentMethodType>(customerPaymentMethodView);
			ICustomerPaymentMethodDetailAdapter detailAdapter = new GenericCustomerPaymentMethodDetailAdapter<TDetialsType>(detailsView);
			GetNewPaymentProfiles(graph,adapter,detailAdapter);
		}

		public static void GetNewPaymentProfiles(PXGraph graph, 
			ICustomerPaymentMethodAdapter customerPaymentMethodAdapter,
			ICustomerPaymentMethodDetailAdapter customerPaymentMethodDetailAdapter
			)
		{
			if (graph == null || customerPaymentMethodAdapter == null || customerPaymentMethodDetailAdapter == null)
				return;
			CustomerPaymentMethod currentCustomerPaymentMethod = customerPaymentMethodAdapter.Current;
			CCCustomerInformationManager cim = new CCCustomerInformationManager(currentCustomerPaymentMethod.CCProcessingCenterID, CCProcessingFeature.HostedForm)
			{
				CustomerID = currentCustomerPaymentMethod.BAccountID,
				PMInstanceID = currentCustomerPaymentMethod.PMInstanceID,
				CallerGraph = graph
			};

			int attempt = 1;
			CreditCardData newCard = null;
			//AuthorizeNet sometimes failes to process new card in time when using Hosted Form Method
			while ((attempt <= (cim._context.processingCenter.SyncRetryAttemptsNo ?? 0) + 1) && newCard == null)
			{
				Thread.Sleep(cim._context.processingCenter.SyncRetryDelayMs ?? 0);
				List<CreditCardData> newCards = null;

				try
				{
					newCards = cim.GetMissingPaymentProfiles().ToList();
				}
				catch (Exception e)
				{
					throw new PXException(e.Message + ". " + Messages.FailedToSyncCC);
				}

				CustomerPaymentMethod customerPaymentMethod = customerPaymentMethodAdapter.Current;

				if (newCards != null && newCards.Count > 1)
				{
					newCards.Sort(new InterfaceExtensions.CreditCardDataComparer());
					newCard = newCards[0];
				}
				else if (newCards != null && newCards.Count == 1)
				{
					newCard = newCards[0];
				}

				if (newCard != null)
				{
					foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> det in customerPaymentMethodDetailAdapter.Select())
					{
						CustomerPaymentMethodDetail cpmd = det;
						PaymentMethodDetail pmd = (PaymentMethodDetail) det;
						if (pmd.IsCCProcessingID == true)
						{
							cpmd.Value = newCard.PaymentProfileID;
							customerPaymentMethodDetailAdapter.Cache.Update(cpmd);
						}
						else if (pmd.IsIdentifier == true)
						{
							cpmd.Value = newCard.CardNumber;
							customerPaymentMethodDetailAdapter.Cache.Update(cpmd);
						}
					}
					//getting unmasked expiration date
					newCard = cim.GetPaymentProfile();
					if (newCard.CardExpirationDate != null)
					{
						customerPaymentMethodAdapter.Cache.SetValueExt<CustomerPaymentMethod.expirationDate>(customerPaymentMethod, newCard.CardExpirationDate);
						customerPaymentMethodAdapter.Cache.Update(customerPaymentMethod);
					}
				}
				attempt++;
			}
			if (newCard == null)
			{
				throw new PXException(Messages.FailedToSyncCC);
			}
		}

		public static void GetManagePaymentProfileForm<TPaymentMethodType>(PXGraph graph, TPaymentMethodType currentCutomerPaymenMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			if (graph == null || currentCutomerPaymenMethod == null)
				return;
			CustomerPaymentMethodDetail ccpID = PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail,
					On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>,
						And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>,
				Where<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<CustomerPaymentMethodDetail.pMInstanceID,
					Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>>.SelectWindowed(graph, 0, 1, currentCutomerPaymenMethod.PMInstanceID);
			if (ccpID != null && !string.IsNullOrEmpty(ccpID.Value))
			{
				CCCustomerInformationManager cim = new CCCustomerInformationManager(currentCutomerPaymenMethod.CCProcessingCenterID, CCProcessingFeature.HostedForm)
				{
					CustomerID = currentCutomerPaymenMethod.BAccountID,
					PMInstanceID = currentCutomerPaymenMethod.PMInstanceID,
					CallerGraph = graph
				};
				cim.GetManagePaymentProfileForm();
			}
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetOrCreatePaymentProfile(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter...) instead.")]
		public static void GetOrCreatePaymentProfile<TPaymentMethodType, TDetialsType>(PXGraph graph
				, PXSelectBase<TPaymentMethodType> customerPaymentMethodView
				, PXSelectBase<TDetialsType> detailsView)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			ICustomerPaymentMethodAdapter paymentAdapter = new GenericCustomerPaymentMethodAdaper<TPaymentMethodType>(customerPaymentMethodView);
			ICustomerPaymentMethodDetailAdapter paymentDetailAdapter = new GenericCustomerPaymentMethodDetailAdapter<TDetialsType>(detailsView);
			GetOrCreatePaymentProfile(graph,paymentAdapter,paymentDetailAdapter);
		}

		public static void GetOrCreatePaymentProfile(PXGraph graph
				, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter
				, ICustomerPaymentMethodDetailAdapter customerPaymentMethodDetailAdapter)
		{
			bool isHF = CCProcessingHelper.IsHFPaymentMethod(graph, customerPaymentMethodAdapter.Current.PMInstanceID);
			bool isConverting = customerPaymentMethodAdapter.Current.Selected == true;
			isHF = isHF && !isConverting;
			CustomerPaymentMethodDetail CCPIDDet = null;
			bool isIDFilled = false;
			bool isOtherDetsFilled = false;
			foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> det in customerPaymentMethodDetailAdapter.Select())
			{
			    CustomerPaymentMethodDetail cpmd = det;
				PaymentMethodDetail cmd = det;
				if (cmd.IsCCProcessingID == true)
				{
					isIDFilled = cpmd.Value != null;
					CCPIDDet = cpmd.CreateCopy(customerPaymentMethodDetailAdapter.Cache);
				}
				else
				{
					isOtherDetsFilled = cpmd.Value != null || isOtherDetsFilled;
				}
			}
			if (CCPIDDet == null)
			{
				//something's very wrong
				throw new PXException(Messages.NOCCPID, customerPaymentMethodAdapter.Current.Descr);
			}
			if (isIDFilled && isOtherDetsFilled)
			{
				return;
			}

			if ((isIDFilled || isOtherDetsFilled) && !isHF || isIDFilled && !isOtherDetsFilled)
			{
				CCCustomerInformationManager cim =
					new CCCustomerInformationManager(customerPaymentMethodAdapter.Current.CCProcessingCenterID,
						CCProcessingFeature.ProfileManagement)
					{
						CustomerID = customerPaymentMethodAdapter.Current.BAccountID,
						PMInstanceID = customerPaymentMethodAdapter.Current.PMInstanceID,
						CallerGraph = graph,
						String2DateConverter =
							s => CustomerPaymentMethodMaint.ParseExpiryDate(graph, customerPaymentMethodAdapter.Current, s),
					};

				var currentCustomerPaymentMethod = customerPaymentMethodAdapter.Current;
				cim.String2DateConverter = s => CustomerPaymentMethodMaint.ParseExpiryDate(graph, customerPaymentMethodAdapter.Current, s);
				string id = currentCustomerPaymentMethod.CustomerCCPID;
				if (currentCustomerPaymentMethod.CustomerCCPID == null)
				{
					id = cim.CreateCustomerProfile();
					CustomerPaymentMethod cpm =  customerPaymentMethodAdapter.Current.CreateCopy(customerPaymentMethodAdapter.Cache);
					cpm.CustomerCCPID = id;
					customerPaymentMethodAdapter.Cache.Update(cpm);
				}
				var processingCenter = cim._context.processingCenter;
				if (processingCenter.CreateAdditionalCustomerProfiles == true)
				{
					int customerProfileCount = CCProcessingHelper.CustomerProfileCountPerCustomer(graph,
						currentCustomerPaymentMethod.BAccountID,
						currentCustomerPaymentMethod.CCProcessingCenterID); // Total customer profile count per customer

					var cardLimit = processingCenter.CreditCardLimit;
					if (cardLimit != null && cardLimit > 0)
					{
						int allPaymentProfileCount = cim.GetAllPaymentProfiles().Count();
						if (CCProcessingHelper.IsCreditCardCountEnough(allPaymentProfileCount, cardLimit.Value))
						{
							cim.PrefixForCustomerCD = CCProcessingHelper.BuildPrefixForCustomerCD(customerProfileCount, processingCenter);
							id = cim.CreateCustomerProfile();
							CustomerPaymentMethod cpm = customerPaymentMethodAdapter.Current.CreateCopy(customerPaymentMethodAdapter.Cache);
							cpm.CustomerCCPID = id;
							customerPaymentMethodAdapter.Cache.Update(cpm);
						}
					}
				}

				if (isOtherDetsFilled)
				{
					string newPMId = cim.CreatePaymentProfile();
					CCPIDDet.Value = newPMId;
					CCPIDDet = customerPaymentMethodDetailAdapter.Cache.Update(CCPIDDet) as CustomerPaymentMethodDetail;
				}
				CreditCardData cardData = cim.GetPaymentProfile();
				if (cardData != null && !string.IsNullOrEmpty(cardData.PaymentProfileID))
				{
					foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> det in customerPaymentMethodDetailAdapter.Select())
					{
						CustomerPaymentMethodDetail cpmd = det;
						PaymentMethodDetail pmd = (PaymentMethodDetail) det;
						if (cpmd.DetailID == CCPIDDet.DetailID)
							continue;
						string detailValue = null;
						if (pmd.IsCCProcessingID != true && pmd.IsIdentifier == true && !string.IsNullOrEmpty(cardData.CardNumber))
						{
							detailValue = cardData.CardNumber;
						}
						CustomerPaymentMethodDetail newcpmd = (CustomerPaymentMethodDetail) cpmd.CreateCopy(customerPaymentMethodDetailAdapter.Cache);
						newcpmd.Value = detailValue;
						customerPaymentMethodDetailAdapter.Cache.Update(newcpmd);
					}
					if (cardData.CardExpirationDate != null)
					{
						CustomerPaymentMethod cpm = customerPaymentMethodAdapter.Current.CreateCopy(customerPaymentMethodAdapter.Cache);
						customerPaymentMethodAdapter.Cache.SetValueExt<CustomerPaymentMethod.expirationDate>(cpm, cardData.CardExpirationDate);
						customerPaymentMethodAdapter.Cache.Update(cpm);
					}
				}
				else
				{
					throw new PXException(Messages.CouldntGetPMIDetails, customerPaymentMethodAdapter.Current.Descr);
				}
			}
		}

		public static void GetPaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			string CCPID = null;
			foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> det in detailsView.Select())
			{
				CustomerPaymentMethodDetail cpmd = (CustomerPaymentMethodDetail) det;
				PaymentMethodDetail pmd = (PaymentMethodDetail) det;
				if (pmd.IsCCProcessingID == true)
				{
					CCPID = cpmd.Value;
					break;
				}
			}
			if (String.IsNullOrEmpty(CCPID))
			{
				throw new PXException(Messages.CreditCardTokenIDNotFound);
			}
			CCCustomerInformationManager cim = new CCCustomerInformationManager(customerPaymentMethodView.Current.CCProcessingCenterID, CCProcessingFeature.ProfileManagement)
			{
				CustomerID = customerPaymentMethodView.Current.BAccountID,
				PMInstanceID = customerPaymentMethodView.Current.PMInstanceID,
				CallerGraph = graph
			};
			CreditCardData cardData = cim.GetPaymentProfile();
			if (cardData == null)
			{
				throw new PXException(Messages.CreditCardNotFoundInProcCenter, CCPID, customerPaymentMethodView.Current.CCProcessingCenterID);
			}
			foreach (PXResult<CustomerPaymentMethodDetail, PaymentMethodDetail> det in detailsView.Select())
			{
				CustomerPaymentMethodDetail cpmd = (CustomerPaymentMethodDetail) det;
				PaymentMethodDetail pmd = (PaymentMethodDetail) det;
				if (pmd.IsCCProcessingID != true && pmd.IsIdentifier == true && !string.IsNullOrEmpty(cardData.CardNumber))
				{
					cpmd.Value = cardData.CardNumber;
					detailsView.Update(cpmd);
				}
			}
			if (cardData.CardExpirationDate != null)
			{
				CustomerPaymentMethod cpm = (CustomerPaymentMethod) customerPaymentMethodView.Cache.CreateCopy(customerPaymentMethodView.Current);
				customerPaymentMethodView.Cache.SetValueExt<CustomerPaymentMethod.expirationDate>(cpm, cardData.CardExpirationDate);
				customerPaymentMethodView.Update(cpm);
			}
		}

		public static void DeletePaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			IEnumerator cpmEnumerator = customerPaymentMethodView.Cache.Deleted.GetEnumerator();
			if (cpmEnumerator.MoveNext())
			{
				CustomerPaymentMethod current = (CustomerPaymentMethod) cpmEnumerator.Current;
				CCProcessingCenter processingCenter = CCProcessingHelper.GetProcessingCenter(graph, current.CCProcessingCenterID);
				if (!string.IsNullOrEmpty(current.CCProcessingCenterID) && processingCenter.SyncronizeDeletion == true)
				{
					CCCustomerInformationManager cim = new CCCustomerInformationManager(current.CCProcessingCenterID, CCProcessingFeature.ProfileManagement)
					{
						CustomerID = current.BAccountID,
						PMInstanceID = current.PMInstanceID
					};
					cim.CallerGraph = cim.ReadingGraph;
					CustomerPaymentMethodDetail ccpidCPMDet = null;
					PaymentMethodDetail ccpidPMDet = PXSelect<PaymentMethodDetail,
						Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>,
							And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>>.Select(graph, current.PaymentMethodID);
					foreach (CustomerPaymentMethodDetail deletedDet in detailsView.Cache.Deleted)
					{
						if (deletedDet.DetailID == ccpidPMDet.DetailID)
						{
							ccpidCPMDet = deletedDet;
							break;
						}
					}
					if (ccpidCPMDet != null && !string.IsNullOrEmpty(ccpidCPMDet.Value))
					{
						cim.DeletePaymentProfile();
					}
				}
			}
		}
	}
}
