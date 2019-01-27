﻿using PX.Data;

namespace PX.Objects.Common.Discount
{
	public static class BreakdownType
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(Quantity, AR.Messages.Quantity),
					Pair(Amount, AR.Messages.Amount)
				}) { }
		}
		public const string Quantity = "Q";
		public const string Amount = "A";

		public class QuantityBreakdown : Constant<string> { public QuantityBreakdown() : base(Quantity) { } }
		public class AmountBreakdown : Constant<string> { public AmountBreakdown() : base(Amount) { } }
	}
}