using PX.Data;

namespace PX.Objects.Common.Discount
{
	public static class DiscountOption
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(Percent, AR.Messages.Percent),
					Pair(Amount, AR.Messages.Amount),
					Pair(FreeItem, AR.Messages.FreeItem),
				}) { }
		}

		public const string Percent = "P";
		public const string Amount = "A";
		public const string FreeItem = "F";

		public class PercentDiscount : Constant<string> { public PercentDiscount() : base(Percent) { } }
		public class AmountDiscount : Constant<string> { public AmountDiscount() : base(Amount) { } }
		public class FreeItemDiscount : Constant<string> { public FreeItemDiscount() : base(FreeItem) { } }
	}
}