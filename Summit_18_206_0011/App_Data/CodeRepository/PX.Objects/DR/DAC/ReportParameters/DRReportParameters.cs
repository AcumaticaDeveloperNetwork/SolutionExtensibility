using PX.Data;

namespace PX.Objects.DR.DAC.ReportParameters
{
	public class DRReportParameters: IBqlTable
	{
		#region pendingRevenueValidate
		public abstract class pendingRevenueValidate : IBqlField { }

		[PXDBBool]
		[PXDefault(typeof(Search<DRSetup.pendingRevenueValidate>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? PendingRevenueValidate { get; set; }
		#endregion
		#region pendingExpenseValidate
		public abstract class pendingExpenseValidate : IBqlField { }

		[PXDBBool]
		[PXDefault(typeof(Search<DRSetup.pendingExpenseValidate>), PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? PendingExpenseValidate { get; set; }
		#endregion
	}
}
