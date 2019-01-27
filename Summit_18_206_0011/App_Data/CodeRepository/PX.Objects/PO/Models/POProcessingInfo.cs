using System;

namespace PX.Objects.AP
{
	public partial class APInvoiceEntry
	{
		[Obsolete(Common.Messages.WillBeRemovedInAcumatica2019R1)]
		public class POProcessingInfo : Tuple<int, int, APTran, int>
		{
			public POProcessingInfo(int serviceLinesTotal, int processedServiceLines, APTran aptran, int insertedLinesTotal) : base(serviceLinesTotal, processedServiceLines, aptran, insertedLinesTotal) { }

			public int ServiceLinesTotal => Item1;
			public int ProcessedServiceLines => Item2;
			public APTran ConflictingAPTran => Item3;
			public int InsertedLinesTotal => Item4;
		}
	}
}
