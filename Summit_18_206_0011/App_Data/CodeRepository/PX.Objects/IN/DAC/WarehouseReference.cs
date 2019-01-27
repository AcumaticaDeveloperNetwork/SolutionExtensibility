using System;
using PX.Data;

namespace PX.Objects.IN.DAC
{
	[Serializable]
	public class WarehouseReference : IBqlTable
	{
		#region SiteID
		public abstract class siteID : IBqlField { }

		[PXDBInt(IsKey = true)]
		[PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
		public virtual int? SiteID { get; set; }
		#endregion

		#region PortalSetupID
		public abstract class portalSetupID : IBqlField { }

		[PXDBString(32, IsKey = true)]
		[PXUIField(Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string PortalSetupID { get; set; }
		#endregion
	}
}
