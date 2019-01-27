using System;
using System.Collections.Generic;
using System.Text;
using PX.Data;
using PX.Data.SQLTree;
using PX.TM;

namespace PX.Objects.EP
{
	public class Approver<UserID> : IBqlComparison
		where UserID : IBqlOperand, new()
	{
		private IBqlCreator _operand;

		public void Verify(PXCache cache, object item, List<object> pars, ref bool? result, ref object value)
		{
			result = null;
			value = null;
		}

		public bool AppendExpression(ref SQLExpression exp, PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection)
		{
			bool status = true;

			SQLExpression userID1 = null;
			status &= BqlCommand.AppendExpression<UserID>(ref userID1, graph, info, selection, ref _operand);
			SQLExpression userID2 = null;
			status &= BqlCommand.AppendExpression<UserID>(ref userID2, graph, info, selection, ref _operand);

			if (graph == null) return status;

			exp.VerifyComparisonExpression();

			exp = exp.LExpr()
				.In(new Query()
					.Select<EPApproval.refNoteID>()
					.From<EPApproval>()
					.Where(typeof(EPApproval.ownerID).Equal(userID1)
						.Or(typeof(EPApproval.workgroupID)
							.In(new Query()
								.Select<EPCompanyTreeH.workGroupID>()
								.From<EPCompanyTreeH>()
								.Join<EPCompanyTreeMember>()
									.On(typeof(EPCompanyTreeH.parentWGID).Equal(typeof(EPCompanyTreeMember.workGroupID))
										.And(typeof(EPCompanyTreeH.parentWGID).NotEqual(typeof(EPCompanyTreeH.workGroupID)))
										.And(typeof(EPCompanyTreeMember.active).Equal(true))
										.And(typeof(EPCompanyTreeMember.userID).Equal(userID2)))
									.Where(PX.Data.SQLTree.Constant.SQLConstant(1)
										.Equal(PX.Data.SQLTree.Constant.SQLConstant(1)))))));
			return status;
		}


		public void Parse(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, BqlCommand.Selection selection)
		{
			if (graph != null && text != null)
			{
				text.Append(" IN ").Append(BqlCommand.SubSelect);
				text.Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPApproval).Name, typeof(EPApproval.refNoteID).Name));
				//text.Append(typeof (EPApproval).Name).Append('.').Append(typeof (EPApproval.refNoteID).Name);
				text.Append(" FROM ").Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPApproval).Name)).Append(' ').Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPApproval).Name));
				text.Append(" WHERE (").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPApproval).Name, typeof(EPApproval.ownerID).Name));
					//.Append(typeof (EPApproval).Name).Append('.').Append(typeof (EPApproval.ownerID).Name);
				text.Append('=');
				ParseOperand(graph, pars, tables, fields, sortColumns, text, selection);
				text.Append(" OR ");
				text.Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPApproval).Name, typeof(EPApproval.workgroupID).Name));
				//text.Append(typeof (EPApproval).Name).Append('.').Append(typeof (EPApproval.workgroupID).Name);
				text.Append(" IN ").Append(BqlCommand.SubSelect);
				text.Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeH).Name, typeof(EPCompanyTreeH.workGroupID).Name));
				//text.Append(typeof (EPCompanyTreeH).Name).Append('.').Append(typeof (EPCompanyTreeH.workGroupID).Name);
				text.Append(" FROM ").Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPCompanyTreeH).Name)).Append(' ').Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPCompanyTreeH).Name));
				text.Append(" INNER JOIN ").Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPCompanyTreeMember).Name)).Append(' ').Append(graph.SqlDialect.quoteDbIdentifier(typeof (EPCompanyTreeMember).Name));
				text.Append(" ON ").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeH).Name, typeof(EPCompanyTreeH.parentWGID).Name));
					//.Append(typeof (EPCompanyTreeH).Name).Append('.').Append(typeof (EPCompanyTreeH.parentWGID).Name);
				text.Append('=').Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeMember).Name, typeof(EPCompanyTreeMember.workGroupID).Name));
					//.Append(typeof (EPCompanyTreeMember).Name).Append('.').Append(typeof (EPCompanyTreeMember.workGroupID).Name);
				text.Append(" AND ").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeH).Name, typeof(EPCompanyTreeH.parentWGID).Name));
					//.Append(typeof (EPCompanyTreeH).Name).Append('.').Append(typeof (EPCompanyTreeH.parentWGID).Name);
				text.Append("<>").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeH).Name, typeof(EPCompanyTreeH.workGroupID).Name));
					//.Append(typeof (EPCompanyTreeH).Name).Append('.').Append(typeof (EPCompanyTreeH.workGroupID).Name);
				text.Append(" AND ").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeMember).Name, typeof(EPCompanyTreeMember.active).Name));
					//.Append(typeof(EPCompanyTreeMember).Name).Append('.').Append(typeof(EPCompanyTreeMember.active).Name);
				text.Append("= CONVERT (BIT, 1)");
				text.Append(" AND ").Append(graph.SqlDialect.quoteTableAndColumn(typeof(EPCompanyTreeMember).Name, typeof(EPCompanyTreeMember.userID).Name));
					//.Append(typeof (EPCompanyTreeMember).Name).Append('.').Append(typeof (EPCompanyTreeMember.userID).Name);
				text.Append('=');
				ParseOperand(graph, pars, tables, fields, sortColumns, text, selection);
				text.Append(" WHERE 1=1");
				text.Append(')').Append(')').Append(')');
			}
			else
			{
				ParseOperand(graph, pars, tables, fields, sortColumns, text, selection);
				ParseOperand(graph, pars, tables, fields, sortColumns, text, selection);
			}
		}

		private void ParseOperand(PXGraph graph, List<IBqlParameter> pars, List<Type> tables, List<Type> fields, List<IBqlSortColumn> sortColumns, StringBuilder text, PX.Data.BqlCommand.Selection selection)
		{
			BqlCommand.EqualityList list = fields as BqlCommand.EqualityList;
			if (list != null) {
				list.NonStrict = true;
			}
			if (!typeof(IBqlCreator).IsAssignableFrom(typeof(UserID)))
			{
				if (graph != null && text != null)
					text.Append(" ").Append(BqlCommand.GetSingleField(typeof(UserID), graph, tables, selection, BqlCommand.FieldPlace.Condition));

				if (fields != null)
					fields.Add(typeof(UserID));
			}
			else
			{
				if (_operand == null)
					_operand = _operand.createOperand<UserID>();
				_operand.Parse(graph, pars, tables, fields, sortColumns, text, selection);
			}
		}
	}

}
