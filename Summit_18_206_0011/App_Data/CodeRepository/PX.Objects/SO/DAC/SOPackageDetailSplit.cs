using System;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;

namespace PX.Objects.SO
{
	[Serializable]
	[PXCacheName(Messages.SOPackageDetailSplit)]
	public partial class SOPackageDetailSplit : PX.Data.IBqlTable
	{
		#region ShipmentNbr
		[PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDBDefault(typeof(SOShipment.shipmentNbr))]
		[PXParent(typeof(CompositeKey<
			Field<shipmentNbr>.IsRelatedTo<SOPackageDetail.shipmentNbr>,
			Field<lineNbr>.IsRelatedTo<SOPackageDetail.lineNbr>>))]
		[PXParent(typeof(CompositeKey<
			Field<shipmentNbr>.IsRelatedTo<SOShipLine.shipmentNbr>,
			Field<shipmentLineNbr>.IsRelatedTo<SOShipLine.lineNbr>>))]
		public virtual String ShipmentNbr { get; set; }
		public abstract class shipmentNbr : PX.Data.IBqlField { }
		#endregion
		#region LineNbr
		[PXDBInt(IsKey = true)]
		[PXDefault(typeof(SOPackageDetail.lineNbr))]
		public virtual Int32? LineNbr { get; set; }
		public abstract class lineNbr : PX.Data.IBqlField { }
		#endregion
		#region SplitLineNbr
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXLineNbr(typeof(SOShipment.lineCntr), false)]
		public virtual Int32? SplitLineNbr { get; set; }
		public abstract class splitLineNbr : PX.Data.IBqlField { }
		#endregion
		#region ShipmentLineNbr
		[PXDBInt()]
		[PXDefault]
		[PXSelector(typeof(Search<SOShipLineNbrVisible.lineNbr,
			Where<SOShipLineNbrVisible.shipmentNbr, Equal<Current<SOPackageDetail.shipmentNbr>>>>),
			new[] { typeof(SOShipLineNbrVisible.lineNbr),
				typeof(SOShipLineNbrVisible.origOrderType),
				typeof(SOShipLineNbrVisible.origOrderNbr),
				typeof(SOShipLineNbrVisible.inventoryID),
				typeof(SOShipLineNbrVisible.tranDesc),
				typeof(SOShipLineNbrVisible.shippedQty),
				typeof(SOShipLineNbrVisible.packedQty),
				typeof(SOShipLineNbrVisible.uOM) }, DirtyRead = true)]
		[PXRestrictor(typeof(Where<SOShipLineNbrVisible.packedQty, Less<SOShipLineNbrVisible.shippedQty>>), Messages.QuantityPackedExceedsShippedQuantityForLine)]
		[PXUIField(DisplayName = "Shipment Line Nbr.")]
		public virtual Int32? ShipmentLineNbr { get; set; }
		public abstract class shipmentLineNbr : PX.Data.IBqlField { }
		#endregion
		#region InventoryID
		[Inventory(Enabled = false)]
		[PXDefault]
		public virtual Int32? InventoryID { get; set; }
		public abstract class inventoryID : PX.Data.IBqlField { }
		#endregion
		#region SubItemID
		[IN.SubItem(typeof(SOPackageDetailSplit.inventoryID), Enabled = false)]
		[PXDefault]
		public virtual Int32? SubItemID { get; set; }
		public abstract class subItemID : PX.Data.IBqlField { }
		#endregion
		#region UOM
		[INUnit(typeof(SOPackageDetailSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
		public virtual String UOM { get; set; }
		public abstract class uOM : PX.Data.IBqlField { }
		#endregion
		#region Qty
		[PXDBQuantity(typeof(SOPackageDetailSplit.uOM), typeof(SOPackageDetailSplit.baseQty))]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Quantity")]
		public virtual Decimal? Qty { get; set; }
		public abstract class qty : PX.Data.IBqlField { }
		#endregion
		#region BaseQty
		[PXDBDecimal(6)]
		public virtual Decimal? BaseQty { get; set; }
		public abstract class baseQty : PX.Data.IBqlField { }
		#endregion

		#region System Columns
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID { get; set; }
		public abstract class createdByID : PX.Data.IBqlField { }

		[PXDBCreatedByScreenID]
		public virtual String CreatedByScreenID { get; set; }
		public abstract class createdByScreenID : PX.Data.IBqlField { }

		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime { get; set; }
		public abstract class createdDateTime : PX.Data.IBqlField { }

		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID { get; set; }
		public abstract class lastModifiedByID : PX.Data.IBqlField { }

		[PXDBLastModifiedByScreenID]
		public virtual String LastModifiedByScreenID { get; set; }
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField { }

		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime { get; set; }
		public abstract class lastModifiedDateTime : PX.Data.IBqlField { }

		[PXDBTimestamp]
		public virtual Byte[] tstamp { get; set; }
		public abstract class Tstamp : PX.Data.IBqlField { }
		#endregion

		/// <summary>
		/// An alias descendant version of <see cref="SOShipLine"/>. Makes the LineNbr field visible in selectors by default.
		/// </summary>
		[Serializable]
		[PXHidden]
		public class SOShipLineNbrVisible : SOShipLine
		{
			public new abstract class lineNbr : PX.Data.IBqlField { }
			[PXDBInt(IsKey = true)]
			[PXLineNbr(typeof(SOShipment.lineCntr))]
			[PXUIField(DisplayName = "Line Nbr.", Visible = true)]
			public new virtual int? LineNbr { get; set; }
		}
	}
}