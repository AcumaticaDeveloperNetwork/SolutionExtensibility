using PX.Objects.PO;
using System;

namespace PX.Objects.AP
{
	public partial class APInvoiceEntry
	{
		[Obsolete(Common.Messages.WillBeRemovedInAcumatica2019R1)]
		public partial class POReceiptLineAdd : POReceiptLineS
		{
		}
	}
}
