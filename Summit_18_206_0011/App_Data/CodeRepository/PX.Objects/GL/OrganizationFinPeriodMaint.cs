using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.GraphBaseExtensions;
using System.Collections;

namespace PX.Objects.GL
{
	public class OrganizationFinPeriodMaint : PXGraph<OrganizationFinPeriodMaint, OrganizationFinYear>
	{
		public class OrganizationFinPeriodStatusActionsGraphExtension : FinPeriodStatusActionsGraphBaseExtension<OrganizationFinPeriodMaint, OrganizationFinYear> { }

		public PXSelect<
			OrganizationFinYear,
			Where<MatchWithOrganization<OrganizationFinYear.organizationID>>>
			OrgFinYear;

		public PXSelect<
			OrganizationFinPeriod,
			Where<OrganizationFinPeriod.organizationID, Equal<Current<OrganizationFinYear.organizationID>>,
				And<OrganizationFinPeriod.finYear, Equal<Current<OrganizationFinYear.year>>>>,
			OrderBy<
				Asc<OrganizationFinPeriod.periodNbr>>>
			OrgFinPeriods;

		public OrganizationFinPeriodMaint()
		{
			OrgFinYear.Cache.AllowInsert =
			OrgFinYear.Cache.AllowUpdate =
			OrgFinYear.Cache.AllowDelete = false;

			OrgFinPeriods.Cache.AllowInsert =
			OrgFinPeriods.Cache.AllowUpdate =
			OrgFinPeriods.Cache.AllowDelete = false;

			PXUIFieldAttribute.SetVisible<OrganizationFinPeriod.iNClosed>(OrgFinPeriods.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
			PXUIFieldAttribute.SetVisible<OrganizationFinPeriod.fAClosed>(OrgFinPeriods.Cache, null, PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>());
		}

		[Api.Export.PXOptimizationBehavior(IgnoreBqlDelegate = true)]
		protected IEnumerable orgFinYear()
		{
			if(PXView.NeedDefaultPrimaryViewObject)
			{
				OrganizationFinYear defaultYear = PXSelect<
					OrganizationFinYear, 
					Where<OrganizationFinYear.organizationID, Equal<Required<OrganizationFinYear.organizationID>>>, 
					OrderBy<
						Desc<OrganizationFinYear.year>>>
					.SelectSingleBound(this, new object[] { }, PXAccess.GetParentOrganizationID(Accessinfo.BranchID));
				return new object[] { defaultYear };
			}
			else
			{
				// standard data member select
				return null;
			}
		}
	}
}
