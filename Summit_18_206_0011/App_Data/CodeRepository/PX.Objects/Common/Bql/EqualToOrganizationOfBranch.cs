using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common.Tools;
using PX.Objects.GL.FinPeriods;

namespace PX.Objects.Common.Bql
{
	public class EqualToOrganizationOfBranch<TOrganizationIDField, TBranchIDParameter> : IBqlUnary
		where TOrganizationIDField : IBqlOperand
		where TBranchIDParameter : IBqlParameter, new()
	{
		private IBqlParameter branchIDParameter;
		protected IBqlParameter BranchIDParameter => Cached.GetValue(ref branchIDParameter, () => new TBranchIDParameter());

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			value = BqlHelper.GetOperandValue<TOrganizationIDField>(cache, item);

			result = (int?) value == GetOrganizationID(cache.Graph);

			if (pars != null)
			{
				pars.Add(BqlHelper.GetParameterValue(cache.Graph, BranchIDParameter));
			}
		}

		public bool AppendExpression(ref SQLExpression exp, PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection)
		{
		    if (graph == null || !info.BuildExpression)
		        return true;

            SQLExpression fld = BqlCommand.GetSingleExpression(typeof(TOrganizationIDField), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);

			int? organizationID = GetOrganizationID(graph);

			exp = fld.Equal(organizationID);

			return true;
		}

	    public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text,
			BqlCommand.Selection selection)
		{
			if (graph != null && text != null)
			{
				string fieldName = BqlCommand.GetSingleField(typeof(TOrganizationIDField), graph, tables, selection, BqlCommand.FieldPlace.Condition);

				int? organizationID = GetOrganizationID(graph);

				text.Append($"{fieldName} = {organizationID}");
			}

			if (pars != null)
			{
				pars.Add(BranchIDParameter);
			}
		}

		protected int? GetOrganizationID(PXGraph graph)
		{
			int? branchID = (int?)BqlHelper.GetParameterValue(graph, BranchIDParameter);

			return PXAccess.GetParentOrganizationID(branchID);
		}
	}
}
