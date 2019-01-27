using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace PX.Objects.PO
{
    [Serializable]
	[PXCacheName(Messages.POLandedCostReceipt)]
	public class POLandedCostReceipt : PX.Data.IBqlTable
	{
		#region LCDocType
		public abstract class lCDocType : PX.Data.IBqlField
		{
		}

		[POLandedCostDocType.List()]
		[PXDBString(1, IsKey = true, IsFixed = true)]
		[PXDBDefault(typeof(POLandedCostDoc.docType))]
		[PXUIField(DisplayName = "Landed Cost Type", Visible = false)]
		public virtual String LCDocType
		{
			get;
			set;
		}
		#endregion
		#region LCRefNbr
		public abstract class lCRefNbr : PX.Data.IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDBDefault(typeof(POLandedCostDoc.refNbr))]
		[PXUIField(DisplayName = "Landed Cost Nbr.")]
		[PXParent(typeof(Select<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Current<lCDocType>>, And<POLandedCostDoc.refNbr, Equal<Current<lCRefNbr>>>>>))]
		public virtual String LCRefNbr
		{
			get;
			set;
		}
		#endregion

		#region POReceiptType
		public abstract class pOReceiptType : PX.Data.IBqlField
		{
		}

		[PXDBString(2, IsKey = true, IsFixed = true)]
		[PXUIField(DisplayName = "PO Receipt Type", Visible = false)]
		public virtual String POReceiptType
		{
			get;
			set;
		}
		#endregion
		#region POReceiptNbr
		public abstract class pOReceiptNbr : PX.Data.IBqlField
		{
		}

		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXUIField(DisplayName = "PO Receipt Nbr.")]
		[PXSelector(typeof(Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<pOReceiptType>>>>))]
		public virtual String POReceiptNbr
		{
			get;
			set;
		}
		#endregion

		#region LineCntr
		public abstract class lineCntr : PX.Data.IBqlField
		{
		}
		[PXInt()]
		[PXDefault(0)]
		public virtual Int32? LineCntr
		{
			get;
			set;
		}
		#endregion
		
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get;
			set;
		}
		#endregion
	}
}
