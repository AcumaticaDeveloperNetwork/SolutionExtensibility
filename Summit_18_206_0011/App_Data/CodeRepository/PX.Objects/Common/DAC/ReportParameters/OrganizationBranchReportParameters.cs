using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.DAC;
using PX.Objects.GL.Attributes;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Descriptor;

namespace PX.Objects.Common.DAC.ReportParameters
{
	public class OrganizationBranchReportParameters : IBqlTable
	{
		#region OrganizationID
		public abstract class organizationID : IBqlField { }

		[Organization(
			false,
			typeof(Search2<Organization.organizationID,
				InnerJoin<Branch,
					On<Organization.organizationID, Equal<Branch.organizationID>>>,
				Where<Branch.branchID, Equal<Current<AccessInfo.branchID>>,
					And2<FeatureInstalled<FeaturesSet.branch>,
					And<MatchWithBranch<Branch.branchID>>>>>))]
		public int? OrganizationID { get; set; }
		#endregion

		#region BranchID
		public abstract class branchID : IBqlField { }

		[BranchOfOrganization(
			typeof(organizationID),
			onlyActive: false,
			sourceType: typeof(Search2<Branch.branchID,
				InnerJoin<Organization,
					On<Branch.organizationID, Equal<Organization.organizationID>>,
				CrossJoin<FeaturesSet>>,
				Where<FeaturesSet.branch, Equal<True>,
					And<Organization.organizationType, NotEqual<OrganizationTypes.withoutBranches>,
					And<Branch.branchID, Equal<Current<AccessInfo.branchID>>>>>>))]
		public int? BranchID { get; set; }
		#endregion

		#region LedgerID
		public abstract class ledgerID : IBqlField { }

		[LedgerOfOrganization(typeof(organizationID), typeof(branchID))]
		public virtual int? LedgerID { get; set; }
		#endregion

		#region NotBudgetLedgerID
		public abstract class notBudgetLedgerID : IBqlField { }

		[LedgerOfOrganization(typeof(organizationID), typeof(branchID), typeof(Where<Ledger.balanceType, NotEqual<LedgerBalanceType.budget>>))]
		public virtual int? NotBudgetLedgerID { get; set; }
		#endregion

		#region BudgetLedgerID
		public abstract class budgetLedgerID : IBqlField { }

		[LedgerOfOrganization(typeof(organizationID),
			typeof(branchID), null,
			typeof(Search<Ledger.ledgerID>),
			typeof(Where<Ledger.balanceType, Equal<LedgerBalanceType.budget>>))]
		public virtual int? BudgetLedgerID { get; set; }
		#endregion

		#region FinPeriodID
		public abstract class finPeriodID : IBqlField { }
		
		[FinPeriodSelector(null, 
			typeof(AccessInfo.businessDate),
			typeof(branchID),
			typeof(organizationID),
			takeBranchForSelectorFromQueryParams: true,
			takeOrganizationForSelectorFromQueryParams: true, 
			useMasterOrganizationIDByDefault: true,
			masterPeriodBasedOnOrganizationPeriods: false)]
		public string FinPeriodID { get; set; }
		#endregion

		#region FinPeriodIDByOrganization
		public abstract class finPeriodIDByOrganization : IBqlField { }

		[FinPeriodSelector(null,
			typeof(AccessInfo.businessDate),
			organizationSourceType: typeof(organizationID),
			takeOrganizationForSelectorFromQueryParams: true,
			useMasterOrganizationIDByDefault: true,
			masterPeriodBasedOnOrganizationPeriods: false)]
		public string FinPeriodIDByOrganization { get; set; }
		#endregion

		#region MasterFinPeriodID
		public abstract class masterFinPeriodID : IBqlField { }

		[FinPeriodSelector(null,
			typeof(AccessInfo.businessDate),
			null,
			null,
			useMasterOrganizationIDByDefault: true,
			masterPeriodBasedOnOrganizationPeriods: false)]
		public string MasterFinPeriodID { get; set; }
		#endregion

		#region FinYear
		public abstract class finYear : IBqlField { }

		[GenericFinYearSelector(null,
			typeof(AccessInfo.businessDate),
			typeof(branchID),
			typeof(organizationID),
			takeBranchForSelectorFromQueryParams: true,
			takeOrganizationForSelectorFromQueryParams: true,
			useMasterOrganizationIDByDefault: true)]
		public string FinYear { get; set; }
		#endregion

		#region StartYearPeriodID
		public abstract class startYearPeriodID : IBqlField { }

		[FinPeriodSelector(null,
			null,
			branchSourceType: typeof(branchID),
			organizationSourceType: typeof(organizationID),
			defaultType: typeof(Search2<
				FinPeriod.finPeriodID, 
				InnerJoin<FinYear, On<FinPeriod.finYear, Equal<FinYear.year>,
					And<FinPeriod.organizationID, Equal<FinYear.organizationID>>>>,
				Where<FinYear.startDate, LessEqual<Current<AccessInfo.businessDate>>,
					And<FinYear.endDate, GreaterEqual<Current<AccessInfo.businessDate>>>>, 
				OrderBy<
					Asc<FinPeriod.finPeriodID>>>),
			takeBranchForSelectorFromQueryParams: true,
			takeOrganizationForSelectorFromQueryParams: true,
			useMasterOrganizationIDByDefault: true,
			masterPeriodBasedOnOrganizationPeriods: false)]
		public string StartYearPeriodID { get; set; }
		#endregion

		#region EndYearPeriodID
		public abstract class endYearPeriodID : IBqlField { }

		[FinPeriodSelector(null,
			null,
			branchSourceType: typeof(branchID),
			organizationSourceType: typeof(organizationID),
			defaultType: typeof(Search2<
				FinPeriod.finPeriodID,
				InnerJoin<FinYear, On<FinPeriod.finYear, Equal<FinYear.year>,
					And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
					And<FinPeriod.organizationID, Equal<FinYear.organizationID>>>>>,
				Where<FinYear.startDate, LessEqual<Current<AccessInfo.businessDate>>,
					And<FinYear.endDate, GreaterEqual<Current<AccessInfo.businessDate>>>>,
				OrderBy<
					Desc<FinPeriod.finPeriodID>>>),
			takeBranchForSelectorFromQueryParams: true,
			takeOrganizationForSelectorFromQueryParams: true,
			useMasterOrganizationIDByDefault: true,
			masterPeriodBasedOnOrganizationPeriods: false)]
		public string EndYearPeriodID { get; set; }
		#endregion
	}
}
