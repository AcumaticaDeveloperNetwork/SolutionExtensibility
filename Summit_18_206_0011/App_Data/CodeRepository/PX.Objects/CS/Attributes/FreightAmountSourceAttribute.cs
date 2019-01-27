using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;

namespace PX.Objects.CS
{
	public class FreightAmountSourceAttribute : PXStringListAttribute
	{
		public const string ShipmentBased = "S";
		public const string OrderBased = "O";

		public class shipmentBased : Constant<string>
		{
			public shipmentBased() : base(ShipmentBased) {; }
		}
		public class orderBased : Constant<string>
		{
			public orderBased() : base(OrderBased) {; }
		}

		public FreightAmountSourceAttribute() : base(
			new[]
			{
				Pair(ShipmentBased, Messages.ShipmentBased),
				Pair(OrderBased, Messages.OrderBased),
			})
		{
		}
	}
}
