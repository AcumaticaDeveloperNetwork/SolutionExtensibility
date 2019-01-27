using System;

using PX.Data;

namespace PX.Objects.AP
{
	[Serializable]
	public class APRetainageInvoice : APRegister
	{
		public new abstract class docType : IBqlField { }
		public new abstract class refNbr : IBqlField { }
		public new abstract class origDocType : IBqlField { }
		public new abstract class origRefNbr : IBqlField { }
		public new abstract class isRetainageDocument : IBqlField { }
	}
}
