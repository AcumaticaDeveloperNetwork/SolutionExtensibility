using PX.Data;

namespace PX.Objects.PO
{
	public static class POAccrualType
	{
		public const string Receipt = "R";
		public const string Order = "O";

		public class receipt : Constant<string>
		{
			public receipt()
				: base(Receipt)
			{; }
		}

		public class order : Constant<string>
		{
			public order()
				: base(Order)
			{; }
		}

		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(new[]
				{
					Pair(Receipt, Messages.Receipt),
					Pair(Order, Messages.Order),
				})
			{; }
		}
	}
}
