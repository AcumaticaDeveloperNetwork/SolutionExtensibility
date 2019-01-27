using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.GL.DAC;

namespace PX.Objects.CS.DAC
{
	[PXCacheName(Messages.Company)]
	[Serializable]
	public partial class OrganizationBAccount : BAccount
	{
		public new abstract class bAccountID : IBqlField { }
		public new abstract class defContactID : IBqlField { }
		public new abstract class defAddressID : IBqlField { }
		public new abstract class defLocationID : IBqlField { }

		#region AcctCD
		public new abstract class acctCD : PX.Data.IBqlField { }

		[PXDimensionSelector("COMPANY", typeof(Search2<BAccount.acctCD, InnerJoin<Organization, On<Organization.bAccountID, Equal<BAccount.bAccountID>>>>), typeof(BAccount.acctCD),
			typeof(BAccount.acctCD), typeof(BAccount.acctName))]
		[PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDefault()]
		[PXUIField(DisplayName = "Company ID", Visibility = PXUIVisibility.SelectorVisible)]
		public override String AcctCD
		{
			get
			{
				return base._AcctCD;
			}
			set
			{
				base._AcctCD = value;
			}
		}
		#endregion
		#region AcctName
		public new abstract class acctName : PX.Data.IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Company Name", Visibility = PXUIVisibility.SelectorVisible)]
		public override String AcctName
		{
			get
			{
				return this._AcctName;
			}
			set
			{
				this._AcctName = value;
			}
		}
		#endregion
	}
}
