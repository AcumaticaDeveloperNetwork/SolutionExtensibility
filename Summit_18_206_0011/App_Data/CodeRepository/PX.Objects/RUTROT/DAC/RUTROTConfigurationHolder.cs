using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.CS;

namespace PX.Objects.RUTROT.DAC
{
	public class RUTROTConfigurationHolder: PXMappedCacheExtension, IRUTROTConfigurationHolder
	{
		public static bool IsActive()
		{
			return PXAccess.FeatureInstalled<CS.FeaturesSet.rutRotDeduction>();
		}
		#region AllowsRUTROT
		public abstract class allowsRUTROT : IBqlField { }

		public virtual bool? AllowsRUTROT
		{
			get;
			set;
		}
		#endregion
		#region RUTDeductionPct
		public abstract class rUTDeductionPct : IBqlField { }

		public decimal? RUTDeductionPct
		{
			get;
			set;
		}
		#endregion
		#region RUTPersonalAllowanceLimit
		public abstract class rUTPersonalAllowanceLimit : IBqlField { }

		public virtual decimal? RUTPersonalAllowanceLimit
		{
			get;
			set;
		}
		#endregion
		#region RUTExtraAllowanceLimit
		public abstract class rUTExtraAllowanceLimit : IBqlField { }

		public virtual decimal? RUTExtraAllowanceLimit
		{
			get;
			set;
		}
		#endregion
		#region ROTDeductionPct
		public abstract class rOTDeductionPct : IBqlField { }

		public decimal? ROTDeductionPct
		{
			get;
			set;
		}
		#endregion
		#region ROTPersonalAllowanceLimit
		public abstract class rOTPersonalAllowanceLimit : IBqlField { }

		public virtual decimal? ROTPersonalAllowanceLimit
		{
			get;
			set;
		}
		#endregion
		#region ROTExtraAllowanceLimit
		public abstract class rOTExtraAllowanceLimit : IBqlField { }

		public virtual decimal? ROTExtraAllowanceLimit
		{
			get;
			set;
		}
		#endregion
		#region RUTROTCuryID
		public abstract class rUTROTCuryID : PX.Data.IBqlField
		{
		}

		public virtual string RUTROTCuryID
		{
			get;
			set;
		}
		#endregion
		#region RUTROTClaimNextRefNbr
		public abstract class rUTROTClaimNextRefNbr : PX.Data.IBqlField { }

		public virtual int? RUTROTClaimNextRefNbr
		{
			get;
			set;
		}
		#endregion
		#region RUTROTOrgNbrValidRegEx
		public abstract class rUTROTOrgNbrValidRegEx : PX.Data.IBqlField
		{
		}

		public virtual string RUTROTOrgNbrValidRegEx
		{
			get;
			set;
		}
		#endregion
		#region Default Type
		public abstract class defaultRUTROTType : PX.Data.IBqlField
		{
		}

		public virtual string DefaultRUTROTType
		{
			get;
			set;
		}

		#endregion
		#region TaxAgencyAccountID
		public abstract class taxAgencyAccountID : PX.Data.IBqlField { }

		public virtual int? TaxAgencyAccountID
		{
			get;
			set;
		}
		#endregion
		#region BalanceOnProcess
		public abstract class balanceOnProcess : PX.Data.IBqlField { }

		public virtual string BalanceOnProcess { get; set; }
		#endregion
	}
}
