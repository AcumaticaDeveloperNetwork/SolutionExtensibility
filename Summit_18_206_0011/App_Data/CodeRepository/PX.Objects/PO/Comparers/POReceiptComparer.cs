using System;
using System.Collections.Generic;

namespace PX.Objects.AP
{
	public partial class APInvoiceEntry
	{
		[Obsolete(Common.Messages.WillBeRemovedInAcumatica2019R1)]
		public class POReceiptComparer : IEqualityComparer<APTran>
		{
			public POReceiptComparer()
			{
			}

			#region IEqualityComparer<APTran> Members

			public bool Equals(APTran x, APTran y)
			{
				return x.ReceiptNbr == y.ReceiptNbr;
			}

			public int GetHashCode(APTran obj)
			{
				return obj.ReceiptNbr.GetHashCode();
			}

			#endregion
		}
	}
}
