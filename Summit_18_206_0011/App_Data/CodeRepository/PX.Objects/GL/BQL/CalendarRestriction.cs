using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.Common;
using PX.Objects.Common.Tools;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;

namespace PX.Objects.GL.BQL
{
	[Obsolete]
	public class CalendarRestriction<TOrganizationIDField, TBranchIDParameter, TOrganizationIDParameter, TUseMasterCalendarParameter>: IBqlUnary
		where TOrganizationIDField : IBqlOperand
		where TBranchIDParameter : IBqlParameter, new()
		where TOrganizationIDParameter : IBqlParameter, new()
		where TUseMasterCalendarParameter : IBqlParameter, new()
	{
		private IBqlParameter branchIDParameter;
		protected IBqlParameter BranchIDParameter => Cached.GetValue(ref branchIDParameter, () => new TBranchIDParameter());

		private IBqlParameter organizationIDParameter;
		protected IBqlParameter OrganizationIDParameter => Cached.GetValue(ref organizationIDParameter, () => new TOrganizationIDParameter());

		private IBqlParameter useMasterCalendarParameter;
		protected IBqlParameter UseMasterCalendarParameter => Cached.GetValue(ref useMasterCalendarParameter, () => new TUseMasterCalendarParameter());

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			value = BqlHelper.GetOperandValue<TOrganizationIDField>(cache, item);

			result = (int?)value == GetOrganizationIDRestrictinValue(cache.Graph);

			if (pars != null)
			{
				pars.Add(BqlHelper.GetParameterValue(cache.Graph, BranchIDParameter));
				pars.Add(BqlHelper.GetParameterValue(cache.Graph, OrganizationIDParameter));
				pars.Add(BqlHelper.GetParameterValue(cache.Graph, UseMasterCalendarParameter));
			}
		}

	    public bool AppendExpression(ref SQLExpression exp, PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection)
	    {
	        throw new NotImplementedException();
	    }

	    public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text,
			BqlCommand.Selection selection)
		{
			if (graph != null && text != null)
			{
				string fieldName = BqlCommand.GetSingleField(typeof(TOrganizationIDField), graph, tables, selection, BqlCommand.FieldPlace.Condition);
				int? orgIDRestrictionValue = GetOrganizationIDRestrictinValue(graph);

				text.Append($"{fieldName} = {orgIDRestrictionValue?.ToString() ?? "NULL"}");
			}

			if (pars != null)
			{
				pars.Add(BranchIDParameter);
				pars.Add(OrganizationIDParameter);
				pars.Add(UseMasterCalendarParameter);
			}
		}

		protected int? GetOrganizationIDRestrictinValue(PXGraph graph)
		{
			bool? useMasterCalendarParamValue = (bool?)BqlHelper.GetParameterValue(graph, UseMasterCalendarParameter);

			if (useMasterCalendarParamValue == true)
			{
				return FinPeriod.organizationID.MasterValue;
			}

			return (int?)BqlHelper.GetParameterValue(graph, BranchIDParameter) 
					?? (int?)BqlHelper.GetParameterValue(graph, OrganizationIDParameter);
		}
	}
}
