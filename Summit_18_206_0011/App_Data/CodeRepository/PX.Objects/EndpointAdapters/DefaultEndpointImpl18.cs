using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.PO;
using System.Linq;

namespace PX.Objects.EndpointAdapters
{
	[PXVersion("18.200.001", "Default")]
	public class DefaultEndpointImpl18 : DefaultEndpointImpl
	{
		/// <summary>
		/// Handles creation of document details in the Bills and Adjustments (AP301000) screen
		/// for cases when po entities are specified
		/// using the <see cref="APInvoiceEntry.addPOOrder">Add PO action</see>.
		/// </summary>
		[FieldsProcessed(new[] {
			"POOrderType",
			"POOrderNbr",
			"POReceiptNbr",
			"POReceiptLine"
		})]
		protected override void BillDetail_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
		{
			EntityValueField receiptNbr = targetEntity.Fields.SingleOrDefault(f => f.Name == "POReceiptNbr") as EntityValueField;
			EntityValueField receiptLineNbr = targetEntity.Fields.SingleOrDefault(f => f.Name == "POReceiptLine") as EntityValueField;

			if (receiptNbr != null || receiptLineNbr != null)
			{
				AddPOReceiptLineToBill(graph, (APInvoiceEntry)graph, receiptNbr, receiptLineNbr);
			}
			else base.BillDetail_Insert(graph, entity, targetEntity);
		}

		private static void AddPOReceiptLineToBill(PXGraph graph, APInvoiceEntry invoiceEntry, EntityValueField receiptNbr, EntityValueField receiptLineNbr)
		{
			if (receiptNbr == null || receiptLineNbr == null) throw new PXException("Both POReceiptNbr and POReceiptLine must be provided to add a Purchase Receipt to details.");

			APInvoiceEntry.POReceiptLineAdd line = PXSelect<APInvoiceEntry.POReceiptLineAdd,
				Where<APInvoiceEntry.POReceiptLineAdd.receiptNbr, Equal<Required<POReceipt.receiptNbr>>,
					And<APInvoiceEntry.POReceiptLineAdd.lineNbr, Equal<Required<POReceiptLine.lineNbr>>>>>.Select(graph, receiptNbr.Value, receiptLineNbr.Value);

			if (line == null)
			{
				throw new PXException($"Receipt Line {receiptNbr.Value} - {receiptLineNbr.Value} not found.");
			}

			line.Selected = true;

            var receiptLineExtension = graph.GetExtension<PO.GraphExtensions.APInvoiceSmartPanel.AddPOReceiptLineExtension>();

            receiptLineExtension.poReceiptLinesSelection.Update(line);

            receiptLineExtension.addReceiptLine2.Press();
		}
	}
}
