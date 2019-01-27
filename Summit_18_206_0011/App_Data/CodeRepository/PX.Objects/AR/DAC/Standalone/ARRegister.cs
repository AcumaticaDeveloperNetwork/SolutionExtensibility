using System;
using System.Diagnostics;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.CS;
using PX.Objects.SO;
using PX.Objects.Common.MigrationMode;
using PX.Objects.TX;

namespace PX.Objects.AR.Standalone
{
	[PXHidden]
	[Serializable]
	public partial class ARRegister : AR.ARRegister
	{
		#region DocType
		public new abstract class docType : IBqlField { }
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField { }
		#endregion
		#region BranchID
		public new abstract class branchID : IBqlField { }
		#endregion
		#region CuryInfoID
		public new abstract class curyInfoID : IBqlField { }

		[PXDBLong]
		public override Int64? CuryInfoID
		{
			get
			{
				return this._CuryInfoID;
			}
			set
			{
				this._CuryInfoID = value;
			}
		}
		#endregion
		#region ClosedFinPeriodID
		public new abstract class closedFinPeriodID : IBqlField { }
		#endregion

		public new abstract class docDate : IBqlField { }
		public new abstract class docDesc : IBqlField { }
		public new abstract class curyID : IBqlField { }
		public new abstract class finPeriodID : IBqlField { }
		public new abstract class status : IBqlField { }
		public new abstract class customerID : IBqlField { }
		public new abstract class released : IBqlField { }
		public new abstract class openDoc : IBqlField { }

		#region ARAccountID
		[PXDefault]
		[Account(typeof(ARRegister.branchID), typeof(Search<Account.accountID,
			Where2<Match<Current<AccessInfo.userName>>,
				And<Account.active, Equal<True>,
					And<Account.isCashAccount, Equal<False>,
						And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull,
							Or<Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account")]
		public override Int32? ARAccountID
		{
			get;
			set;
		}
		#endregion
		#region ARSubID
		public new abstract class aRSubID : PX.Data.IBqlField
		{
		}

		[PXDefault]
		[SubAccount(typeof(ARRegister.aRAccountID), DescriptionField = typeof(Sub.description), DisplayName = "AR Subaccount", Visibility = PXUIVisibility.Visible)]
		public override Int32? ARSubID
		{
			get;
			set;
		}
		#endregion
	}
}
