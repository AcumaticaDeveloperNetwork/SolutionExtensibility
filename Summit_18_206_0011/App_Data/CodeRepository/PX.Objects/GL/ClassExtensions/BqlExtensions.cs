using PX.Data;
using System.Collections.Generic;

namespace PX.Objects.GL.ClassExtensions
{
	public static class BqlExtensions
	{
		public static TTable SelectSingle<TTable>(this BqlCommand command, PXGraph graph, bool isReadonly, params object[] parameters)
			where TTable : IBqlTable
		{
			object data = (new PXView(graph, isReadonly, command).SelectSingle(parameters));
			PXResult result = data as PXResult;

			return result != null
				? (TTable)result[typeof(TTable)]
				: (TTable)data;
		}

		public static TTable SelectSingle<TTable>(this BqlCommand command, PXGraph graph, bool isReadonly, IBqlTable[] currents, params object[] parameters)
			where TTable : IBqlTable
		{
			object data = (new PXView(graph, isReadonly, command).SelectSingleBound(currents, parameters));
			PXResult result = data as PXResult;

			return result != null
				? (TTable)result[typeof(TTable)]
				: (TTable)data;
		}

		public static TTable SelectSingle<TTable>(this BqlCommand command, PXGraph graph, params object[] parameters)
			where TTable : IBqlTable
		{
			return command.SelectSingle<TTable>(graph, false, parameters);
		}

		public static TTable SelectSingleReadonly<TTable>(this BqlCommand command, PXGraph graph, params object[] parameters)
			where TTable : IBqlTable
		{
			return command.SelectSingle<TTable>(graph, true, parameters);
		}

		public static TTable SelectSingleReadonly<TTable>(this BqlCommand command, PXGraph graph, IBqlTable[] currents, params object[] parameters)
			where TTable : IBqlTable
		{
			return command.SelectSingle<TTable>(graph, true, currents, parameters);
		}

		public static IEnumerable<TTable> Select<TTable>(this BqlCommand command, PXGraph graph, bool isReadonly, params object[] parameters)
			where TTable : IBqlTable
		{
			return new PXView(graph, isReadonly, command).SelectMulti(parameters).RowCast<TTable>();
		}

		public static IEnumerable<TTable> Select<TTable>(this BqlCommand command, PXGraph graph, params object[] parameters)
			where TTable : IBqlTable
		{
			return command.Select<TTable>(graph, false, parameters);
		}

		public static IEnumerable<TTable> SelectReadonly<TTable>(this BqlCommand command, PXGraph graph, params object[] parameters)
			where TTable : IBqlTable
		{
			return command.Select<TTable>(graph, true, parameters);
		}

		public static bool Any(this BqlCommand command, PXGraph graph, bool isReadonly, params object[] parameters)
		{
			return (new PXView(graph, isReadonly, command).SelectSingle(parameters)) != null;
		}

		public static bool Any(this BqlCommand command, PXGraph graph, bool isReadonly, IBqlTable[] currents, params object[] parameters)
		{
			return (new PXView(graph, isReadonly, command).SelectSingleBound(currents, parameters)) != null;
		}

		public static bool Any(this BqlCommand command, PXGraph graph, params object[] parameters)
		{
			return command.Any(graph, false, parameters);
		}

		public static bool Any(this BqlCommand command, PXGraph graph, IBqlTable[] currents, params object[] parameters)
		{
			return command.Any(graph, false, currents, parameters);
		}

		public static bool AnyReadonly(this BqlCommand command, PXGraph graph, params object[] parameters)
		{
			return command.Any(graph, true, parameters);
		}

		public static bool AnyReadonly(this BqlCommand command, PXGraph graph, IBqlTable[] currents, params object[] parameters)
		{
			return command.Any(graph, true, currents, parameters);
		}
	}
}