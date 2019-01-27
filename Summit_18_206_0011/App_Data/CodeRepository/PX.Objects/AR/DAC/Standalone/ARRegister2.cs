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
	public partial class ARRegister2 : AR.ARRegister
	{
		#region DocType
		public new abstract class docType : IBqlField { }
		#endregion
		#region RefNbr
		public new abstract class refNbr : IBqlField { }
		#endregion
		#region CuryInfoID
		public new abstract class curyInfoID : IBqlField { }
		[PXDBLong]
		public override long? CuryInfoID
		{
			get;
			set;
		}
		#endregion
		#region ClosedFinPeriodID
		public new abstract class closedFinPeriodID : IBqlField { }
		#endregion
	}
}
