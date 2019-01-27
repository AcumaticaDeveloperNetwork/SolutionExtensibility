using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.SQLTree;

namespace PX.Objects.Common.Bql
{
	public sealed class SameOrganizationBranch<Field, Parameter> : IBqlUnary
		where Field : IBqlOperand
		where Parameter : IBqlParameter, new()
	{
		IBqlParameter _parameter;

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			object val = null;

			if (_parameter == null)
			{
				_parameter = new Parameter();
			}

			if (_parameter.HasDefault)
			{
				Type ft = _parameter.GetReferencedType();
				if (ft.IsNested)
				{
					Type ct = BqlCommand.GetItemType(ft);
					PXCache paramcache = cache.Graph.Caches[ct];
					if (paramcache.Current != null)
					{
						val = paramcache.GetValue(paramcache.Current, ft.Name);
					}
				}
			}

			if (typeof(IBqlField).IsAssignableFrom(typeof(Field)))
			{
				if ((cache.GetItemType() == BqlCommand.GetItemType(typeof(Field)) || BqlCommand.GetItemType(typeof(Field)).IsAssignableFrom(cache.GetItemType())))
				{
					value = cache.GetValue(item, typeof(Field).Name);
				}
				else
				{
					value = null;
				}
			}
			else
			{
				throw new PXArgumentException("Operand", ErrorMessages.OperandNotClassFieldAndNotIBqlCreator);
			}

			List<int> branches = null;

			if (val != null)
			{
				branches = GetSameOrganizationBranches((int?)val);
			}
			if (branches != null && branches.Count > 0 && value != null)
			{
				result = branches.Contains((int)value);
			}
		}

		public bool AppendExpression(ref SQLExpression exp, PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection) 
		{
			bool status = true;
			if (graph == null || !info.BuildExpression) return status;

			if (_parameter == null) _parameter = new Parameter();
			object val = null;
			if (_parameter.HasDefault) {
				Type ft = _parameter.GetReferencedType();
				if (ft.IsNested) {
					Type ct = BqlCommand.GetItemType(ft);
					PXCache cache = graph.Caches[ct];
					if (cache.Current != null) {
						val = cache.GetValue(cache.Current, ft.Name);
					}
				}
			}
			SQLExpression fld = BqlCommand.GetSingleExpression(typeof(Field), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
			exp = fld.IsNull();

			List<int> branches = null;

			if (val != null) branches = GetSameOrganizationBranches((int?)val);
			if (branches != null && branches.Count > 0) {
				SQLExpression inn = null;
				foreach (int numBranch in branches) {
					if (null == inn) inn = new Data.SQLTree.Constant(numBranch);
					else inn = inn.Seq(numBranch);
				}
				exp = exp.Or(fld.In(inn)).Embrace();
			}

			return status;
		}

		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
		{
			if (graph != null && text != null)
			{
				if (_parameter == null)
				{
					_parameter = new Parameter();
				}

				object val = null;
				if (_parameter.HasDefault)
				{
					Type ft = _parameter.GetReferencedType();
					if (ft.IsNested)
					{
						Type ct = BqlCommand.GetItemType(ft);
						PXCache cache = graph.Caches[ct];
						if (cache.Current != null)
						{
							val = cache.GetValue(cache.Current, ft.Name);
						}
					}
				}
				String toAppend = BqlCommand.GetSingleField(typeof(Field), graph, tables, selection, BqlCommand.FieldPlace.Condition);
				text.Append("(").Append(toAppend).Append(" IS NULL ");

				List<int> branches = null;

				if (val != null)
					branches = GetSameOrganizationBranches((int?)val);
				if (branches != null && branches.Count > 0)
				{
					text.Append(" OR ").Append(toAppend).Append(" IN (");
					foreach (int numBranch in branches)
					{
						text.Append(numBranch).Append(",");
					}
					text.Remove(text.Length - 1, 1);
					text.Append(")");
				}
				text.Append(")");
			}
		}

		private List<int> GetSameOrganizationBranches(int? branchID)
		{
			int? organizationID = PXAccess.GetParentOrganizationID(branchID);

			return PXAccess.GetChildBranchIDs(organizationID).ToList();
		}
	}
}
