using System.Collections.Generic;
using PX.Data;
using PX.Data.SQLTree;

namespace PX.Objects.CR
{
	class EntryStatus: IBqlOperand, IBqlCreator
	{
		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			value = cache.InternalCurrent != null ? cache.GetStatus(cache.InternalCurrent) : PXEntryStatus.Notchanged;
		}

		public bool AppendExpression(ref SQLExpression exp, PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection)
		{
			return true;
		}

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<System.Type> tables, List<System.Type> fields, List<IBqlSortColumn> sortColumns, System.Text.StringBuilder text, BqlCommand.Selection selection) {}

		public sealed class inserted : Constant<PXEntryStatus>
		{
			public inserted() : base(PXEntryStatus.Inserted) { }
		}
		public sealed class updated : Constant<PXEntryStatus>
		{
			public updated() : base(PXEntryStatus.Updated) { }
		}
		public sealed class deleted : Constant<PXEntryStatus>
		{
			public deleted() : base(PXEntryStatus.Deleted) { }
		}
		public sealed class notchanged : Constant<PXEntryStatus>
		{
			public notchanged() : base(PXEntryStatus.Notchanged) { }
		}
	}
}
