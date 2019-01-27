using PX.Data;
using PX.Objects.CR;
using PX.Objects.SO;

namespace PX.Objects.AR
{
	class CustomerOrOrganizationRestrictorAttribute : PXRestrictorAttribute
	{
		public CustomerOrOrganizationRestrictorAttribute() :
			base(
				typeof(Where<Customer.type, IsNotNull,
					Or<BAccountR.type, Equal<BAccountType.branchType>,
					Or<BAccountR.type, Equal<BAccountType.organizationBranchCombinedType>,
					Or<BAccountR.type, Equal<BAccountType.organizationType>>>>>),
				Messages.CustomerOrOrganization)
		{
		}
	}

	class CustomerOrOrganizationInNoUpdateDocRestrictorAttribute : PXRestrictorAttribute
	{
		public CustomerOrOrganizationInNoUpdateDocRestrictorAttribute() :
			base(
				typeof(Where<Customer.type, IsNotNull,
					Or<Current<SOOrder.aRDocType>, Equal<ARDocType.noUpdate>,
					And<Current<SOOrder.behavior>, Equal<SOOrderTypeConstants.salesOrder>,
					And<
						Where<BAccountR.type, Equal<BAccountType.branchType>,
							Or<BAccountR.type, Equal<BAccountType.organizationBranchCombinedType>,
							Or<BAccountR.type, Equal<BAccountType.organizationType>>>>>>>>),
				Messages.CustomerOrOrganization)
		{
		}
	}
}
