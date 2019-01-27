using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace PX.Objects.PO.LandedCosts.Attributes
{
	public class POLandedCostDocStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute() : base(
				new[]
				{
					Pair(Hold, Messages.Hold),
					Pair(Balanced, Messages.Balanced),
					Pair(Released, Messages.Released),
				})
			{ }
		}

		public const string Hold = "H";
		public const string Balanced = "B";
		public const string Released = "R";

		public class hold : Constant<string>
		{
			public hold() : base(Hold) {; }
		}

		public class balanced : Constant<string>
		{
			public balanced() : base(Balanced) { }
		}

		public class released : Constant<string>
		{
			public released() : base(Released) { }
		}
	}
}
