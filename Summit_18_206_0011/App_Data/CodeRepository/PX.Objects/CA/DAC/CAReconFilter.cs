using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.CM;

namespace PX.Objects.CA
{
	[Serializable]
	//it is used in some reports (CA624000) 
	public partial class CAReconFilter : IBqlTable
	{
		#region ReconNbr
		public abstract class reconNbr : IBqlField
		{
		}
		[PXString(15, IsUnicode = true, InputMask = "")]
		[PXDefault]
		[PXUIField(DisplayName = "Ref. Number", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(Search<CARecon.reconNbr, Where<CARecon.cashAccountID,
									Equal<Optional<CARecon.cashAccountID>>, Or<Optional<CARecon.cashAccountID>, IsNull>>>),
					typeof(CARecon.reconNbr), typeof(CARecon.cashAccountID), typeof(CARecon.reconDate), typeof(CARecon.status))] 
		public virtual string ReconNbr
		{
			get;
			set;
		}
		#endregion
		#region CashAccountID
		public abstract class cashAccountID : IBqlField
		{
		}
		[CashAccount(null, typeof(Search<CashAccount.cashAccountID, Where<CashAccount.reconcile, Equal<boolTrue>, And<Match<Current<AccessInfo.userName>>>>>), IsKey = true, Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
		[PXDefault]
		public virtual int? CashAccountID
		{
			get;
			set;
		}
		#endregion
		#region StartDate
		public abstract class startDate : IBqlField
		{
		}
		[PXDate]
		[PXDefault]
		[PXUIField(DisplayName = "Start Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? StartDate
		{
			get;
			set;
		}
		#endregion
		#region EndDate
		public abstract class endDate : IBqlField
		{
		}
		[PXDate]
		[PXDefault]
		[PXUIField(DisplayName = "End Date", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual DateTime? EndDate
		{
			get;
			set;
		}
		#endregion
	}
}