namespace PX.Objects.AR.CCPaymentProcessing
{
	using PX.Data;
	using System;
	public class CCCustomerInformationManagerGraph : PXGraph<CCCustomerInformationManagerGraph>
	{
		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetOrCreatePaymentProfile(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter, ...) instead.")]
		public void GetOrCreatePaymentProfile<TPaymentMethodType, TDetialsType>(PXGraph graph
				, PXSelectBase<TPaymentMethodType> customerPaymentMethodView
				, PXSelectBase<TDetialsType> detailsView)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			CCCustomerInformationManager.GetOrCreatePaymentProfile(graph, customerPaymentMethodView, detailsView);
		}

		public virtual void GetOrCreatePaymentProfile(PXGraph graph
			, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter
			, ICustomerPaymentMethodDetailAdapter customerPaymentMethodDetailAdapter)
		{
			CCCustomerInformationManager.GetOrCreatePaymentProfile(graph, customerPaymentMethodAdapter, customerPaymentMethodDetailAdapter);
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetOrCreatePaymentProfileForm(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter, ...) instead.")]
		public void GetCreatePaymentProfileForm<TPaymentMethodType>(PXGraph graph,
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			CCCustomerInformationManager.GetCreatePaymentProfileForm(graph, customerPaymentMethodView, currentCustomerPaymentMethod);
		}

		public virtual void GetCreatePaymentProfileForm(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter)
		{
			CCCustomerInformationManager.GetCreatePaymentProfileForm(graph, customerPaymentMethodAdapter);
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
		public void GetManagePaymentProfileForm<TPaymentMethodType>(PXGraph graph, TPaymentMethodType currentCutomerPaymenMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
		{
			CCCustomerInformationManager.GetManagePaymentProfileForm(graph, currentCutomerPaymenMethod);
		}

		public virtual void GetManagePaymentProfileForm(PXGraph graph, CustomerPaymentMethod currentCutomerPaymenMethod)
		{
			CCCustomerInformationManager.GetManagePaymentProfileForm(graph, currentCutomerPaymenMethod);
		}

		[Obsolete("The method is obsolete and will be removed in the later Acumatica versions. " +
			"Use GetNewPaymentProfiles(PXGraph graph, ICustomerPaymentMethodAdapter customerPaymentMethodAdapter...) instead.")]
		public void GetNewPaymentProfiles<TPaymentMethodType, TDetialsType>(PXGraph graph,
			PXSelectBase<TPaymentMethodType> customerPaymentMethodView,
			PXSelectBase<TDetialsType> detailsView,
			TPaymentMethodType currentCustomerPaymentMethod)
			where TPaymentMethodType : CustomerPaymentMethod, new()
			where TDetialsType : CustomerPaymentMethodDetail, new()
		{
			CCCustomerInformationManager.GetNewPaymentProfiles(graph, customerPaymentMethodView, detailsView, currentCustomerPaymentMethod);
		}

		public virtual void GetNewPaymentProfiles(PXGraph graph,
			ICustomerPaymentMethodAdapter customerPaymentMethodAdapter,
			ICustomerPaymentMethodDetailAdapter customerPaymentMethodDetailAdapter)
		{
			CCCustomerInformationManager.GetNewPaymentProfiles(graph, customerPaymentMethodAdapter, customerPaymentMethodDetailAdapter);
		}

		public virtual void GetPaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			CCCustomerInformationManager.GetPaymentProfile(graph, customerPaymentMethodView, detailsView);
		}

		public virtual PXResultset<CustomerPaymentMethodDetail> GetAllCustomersCardsInProcCenter(PXGraph graph, int? BAccountID, string CCProcessingCenterID)
		{
			return CCCustomerInformationManager.GetAllCustomersCardsInProcCenter(graph, BAccountID, CCProcessingCenterID);
		}

		public virtual void DeletePaymentProfile(PXGraph graph, PXSelectBase<CustomerPaymentMethod> customerPaymentMethodView, PXSelectBase<CustomerPaymentMethodDetail> detailsView)
		{
			CCCustomerInformationManager.DeletePaymentProfile(graph, customerPaymentMethodView, detailsView);
		}
	}
}