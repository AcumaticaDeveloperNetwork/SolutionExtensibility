using PX.SM;
using System;
using PX.Data;
using System.Collections.Generic;
using System.Collections;
using PX.Objects.GL;
using System.Diagnostics;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;

namespace PX.Objects.PM
{
	public class ProjectBalanceValidation : PXGraph<ProjectBalanceValidation>
	{
		public PXCancel<PMValidationFilter> Cancel;
		public PXFilter<PMValidationFilter> Filter;
		public PXFilteredProcessing<PMProject, PMValidationFilter, Where<PMProject.baseType, Equal<CT.CTPRType.project>,
			And<PMProject.nonProject, Equal<False>,
			And2<Match<PMProject, Current<AccessInfo.userName>>,
			And<Where<PMProject.isActive, Equal<True>, Or<PMProject.isCompleted, Equal<True>>>>>>>> Items;


		public ProjectBalanceValidation()
		{
			Items.SetSelected<PMProject.selected>();
			Items.SetProcessCaption(GL.Messages.ProcValidate);
			Items.SetProcessAllCaption(GL.Messages.ProcValidateAll);
		}

		protected virtual void PMValidationFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
		{
			PMValidationFilter filter = Filter.Current;

			Items.SetProcessDelegate<ProjectBalanceValidationProcess>(
					delegate (ProjectBalanceValidationProcess graph, PMProject item)
					{
						graph.Clear();
						graph.RunProjectBalanceVerification(item, filter);
						graph.Actions.PressSave();
					});
		}
	}

    [Serializable]
	public class ProjectBalanceValidationProcess : PXGraph<ProjectBalanceValidationProcess>
	{
		#region DAC Overrides
		[POCommitment]
		[PXDBGuid]
		protected virtual void POLine_CommitmentID_CacheAttached(PXCache sender) { }

		[PXDBString(2, IsKey = true, IsFixed = true)]
		protected virtual void POLine_OrderType_CacheAttached(PXCache sender) { }

		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXParent(typeof(Select<POOrder, Where<POOrder.orderType, Equal<Current<POLine.orderType>>, And<POOrder.orderNbr, Equal<Current<POLine.orderNbr>>>>>))]
		protected virtual void POLine_OrderNbr_CacheAttached(PXCache sender) { }

		[PXDBDate()]
		protected virtual void POLine_OrderDate_CacheAttached(PXCache sender) { }

		[PXDBInt]
		protected virtual void POLine_VendorID_CacheAttached(PXCache sender) { }

		[SOCommitment]
		[PXDBGuid]
		protected virtual void SOLine_CommitmentID_CacheAttached(PXCache sender) { }

		[PXDBString(2, IsKey = true, IsFixed = true)]
		protected virtual void SOLine_OrderType_CacheAttached(PXCache sender) { }

		[PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXParent(typeof(Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOLine.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOLine.orderNbr>>>>>))]
		protected virtual void SOLine_OrderNbr_CacheAttached(PXCache sender) { }

		[PXDBDate()]
		protected virtual void SOLine_OrderDate_CacheAttached(PXCache sender) { }

		[PXDefault]
		[PXDBInt] //NO Selector Validation
		protected virtual void PMCommitment_ProjectID_CacheAttached(PXCache sender) { }

		[PXDefault]
		[PXDBInt] //NO Selector Validation
		protected virtual void PMCommitment_ProjectTaskID_CacheAttached(PXCache sender) { } 
		#endregion


		public PXSelect<PMBudgetAccum> Budget;
		public PXSelect<PMTaskTotal> TaskTotals;
		public PXSelect<PMTaskAllocTotalAccum> AllocationTotals;
		public PXSelect<PMHistoryAccum> History;
		public PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>,
				And<PMTran.released, Equal<True>>>> Transactions;
		public PXSelectJoin<POLine,
			InnerJoin<POOrder, On<POOrder.orderType, Equal<POLine.orderType>, And<POOrder.orderNbr, Equal<POLine.orderNbr>>>>,
			Where<POLine.projectID, Equal<Required<POLine.projectID>>>> polines;
		public PXSelect<APTran, Where<APTran.released, Equal<True>, And<APTran.projectID, Equal<Required<APTran.projectID>>, And<APTran.pOLineNbr, IsNotNull>>>> aptran;
		public PXSelect<ARTran, Where<ARTran.released, Equal<True>, And<ARTran.projectID, Equal<Required<ARTran.projectID>>, And<ARTran.sOOrderLineNbr, IsNotNull>>>> artran;
		public PXSelectJoin<SOLine,
			InnerJoin<SOOrder, On<SOOrder.orderType, Equal<SOLine.orderType>, And<SOOrder.orderNbr, Equal<SOLine.orderNbr>>>>,
			Where<SOLine.projectID, Equal<Required<SOLine.projectID>>>> solines;
		public PMCommitmentSelect<PMCommitment, Where<PMCommitment.type, Equal<PMCommitmentType.externalType>, And<PMCommitment.projectID, Equal<Required<PMCommitment.projectID>>>>> ExternalCommitments;
		public Dictionary<int, PMAccountGroup> AccountGroups;

		public virtual void RunProjectBalanceVerification(PMProject project, PMValidationFilter options)
		{
			InitAccountGroup();

			PXDatabase.Delete<PMTaskTotal>(new PXDataFieldRestrict(typeof(PMTaskTotal.projectID).Name, PXDbType.Int, 4, project.ContractID, PXComp.EQ));
			PXDatabase.Delete<PMTaskAllocTotal>(new PXDataFieldRestrict(typeof(PMTaskAllocTotal.projectID).Name, PXDbType.Int, 4, project.ContractID, PXComp.EQ));
			PXDatabase.Delete<PMHistory>(new PXDataFieldRestrict(typeof(PMHistory.projectID).Name, PXDbType.Int, 4, project.ContractID, PXComp.EQ));
			PXDatabase.Delete<PMCommitment>(new PXDataFieldRestrict(typeof(PMCommitment.projectID).Name, PXDbType.Int, 4, project.ContractID, PXComp.EQ),
				new PXDataFieldRestrict(typeof(PMCommitment.type).Name, PXDbType.Char, 1, PMCommitmentType.Internal, PXComp.EQ));

			if (options.RecalculateUnbilledSummary == true)
			{
				PXDatabase.Delete<PMUnbilledDailySummary>(new PXDataFieldRestrict(typeof(PMUnbilledDailySummary.projectID).Name, PXDbType.Int, 4, project.ContractID, PXComp.EQ));
			}

			ProjectBalance pb = CreateProjectBalance();
					
			Dictionary<BudgetKeyTuple, PMBudgetEx> existingBudgetRecords = new Dictionary<BudgetKeyTuple, PMBudgetEx>();
			
			if (options.RecalculateChangeOrders == true && project.ChangeOrderWorkflow == true)
			{
				var selectBudget = new PXSelect<PMBudgetEx, Where<PMBudgetEx.projectID, Equal<Required<PMBudgetEx.projectID>>>>(this);
				using (new PXFieldScope(selectBudget.View, typeof(PMBudgetEx.projectID), typeof(PMBudgetEx.projectTaskID)
					, typeof(PMBudgetEx.accountGroupID), typeof(PMBudgetEx.inventoryID), typeof(PMBudgetEx.costCodeID)
					, typeof(PMBudgetEx.uOM), typeof(PMBudgetEx.qty), typeof(PMBudgetEx.amount)))
				{
					foreach (PMBudgetEx record in selectBudget.Select(project.ContractID))
					{
						existingBudgetRecords.Add(GetKey(record), record);

						List<PXDataFieldParam> list = BuildBudgetClearCommandWithChangeOrders(options, record);
						PXDatabase.Update<PMBudget>(list.ToArray());
					}
				}
			}
			else
			{
				HashSet<int> clearedAccountGroups = new HashSet<int>();
				var selectBudget = new PXSelect<PMBudgetEx, Where<PMBudgetEx.projectID, Equal<Required<PMBudgetEx.projectID>>>>(this);
				using (new PXFieldScope(selectBudget.View, typeof(PMBudgetEx.projectID), typeof(PMBudgetEx.projectTaskID)
					, typeof(PMBudgetEx.accountGroupID), typeof(PMBudgetEx.inventoryID), typeof(PMBudgetEx.costCodeID)
					, typeof(PMBudgetEx.uOM)))
				{
					foreach (PMBudgetEx record in selectBudget.Select(project.ContractID))
					{
						existingBudgetRecords.Add(GetKey(record), record);

						if (!clearedAccountGroups.Contains(record.AccountGroupID.Value))
						{
							List<PXDataFieldParam> list = BuildBudgetClearCommand(options, record);
							PXDatabase.Update<PMBudget>(list.ToArray());
							clearedAccountGroups.Add(record.AccountGroupID.Value);
						}
					}
				}
			}

			PXSelectBase <PMTran> select = null;

			if (options.RecalculateUnbilledSummary == true)
			{
				select = new PXSelectJoinGroupBy<PMTran,
				LeftJoin<Account, On<PMTran.accountID, Equal<Account.accountID>>,
				LeftJoin<OffsetAccount, On<PMTran.offsetAccountID, Equal<OffsetAccount.accountID>>,
				LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<Account.accountGroupID>>,
				LeftJoin<OffsetPMAccountGroup, On<OffsetPMAccountGroup.groupID, Equal<OffsetAccount.accountGroupID>>>>>>,
				Where<PMTran.projectID, Equal<Required<PMTran.projectID>>,
				And<PMTran.released, Equal<True>>>,
				Aggregate<GroupBy<PMTran.tranType,
				GroupBy<PMTran.finPeriodID,
				GroupBy<PMTran.tranPeriodID,
				GroupBy<PMTran.projectID,
				GroupBy<PMTran.taskID,
				GroupBy<PMTran.inventoryID,
				GroupBy<PMTran.costCodeID,
				GroupBy<PMTran.date,
				GroupBy<PMTran.accountID,
				GroupBy<PMTran.accountGroupID,
				GroupBy<PMTran.offsetAccountID,
				GroupBy<PMTran.offsetAccountGroupID,
				GroupBy<PMTran.uOM,
				GroupBy<PMTran.released,
				GroupBy<PMTran.remainderOfTranID,
				GroupBy<PMTran.duplicateOfTranID,
				Sum<PMTran.qty,
				Sum<PMTran.amount,
				Max<PMTran.billable,
				GroupBy<PMTran.billed,
				GroupBy<PMTran.reversed>>>>>>>>>>>>>>>>>>>>>>>(this);
			}
			else
			{
				select = new PXSelectJoinGroupBy<PMTran,
				LeftJoin<Account, On<PMTran.accountID, Equal<Account.accountID>>,
				LeftJoin<OffsetAccount, On<PMTran.offsetAccountID, Equal<OffsetAccount.accountID>>,
				LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<Account.accountGroupID>>,
				LeftJoin<OffsetPMAccountGroup, On<OffsetPMAccountGroup.groupID, Equal<OffsetAccount.accountGroupID>>>>>>,
				Where<PMTran.projectID, Equal<Required<PMTran.projectID>>,
				And<PMTran.released, Equal<True>>>,
				Aggregate<GroupBy<PMTran.tranType,
				GroupBy<PMTran.finPeriodID,
				GroupBy<PMTran.tranPeriodID,
				GroupBy<PMTran.projectID,
				GroupBy<PMTran.taskID,
				GroupBy<PMTran.inventoryID,
				GroupBy<PMTran.costCodeID,
				GroupBy<PMTran.accountID,
				GroupBy<PMTran.accountGroupID,
				GroupBy<PMTran.offsetAccountID,
				GroupBy<PMTran.offsetAccountGroupID,
				GroupBy<PMTran.uOM,
				GroupBy<PMTran.released,
				GroupBy<PMTran.remainderOfTranID,
				GroupBy<PMTran.duplicateOfTranID,
				Sum<PMTran.qty,
				Sum<PMTran.amount>>>>>>>>>>>>>>>>>>>(this);
			}

			foreach (PXResult<PMTran, Account, OffsetAccount, PMAccountGroup, OffsetPMAccountGroup> res in select.Select(project.ContractID))
			{
				PMTran tran = (PMTran)res;

				RegisterReleaseProcess.AddToUnbilledSummary(this, tran);

				//suppose we have allocated unbilled 100 - unearned 100
				//during billing we reduced the amount to 80.
				//as a result of this. only 80 will be reversed. leaving 20 on the unbilled.
				//plus a remainder transaction will be generated. (if we allow this remainder to update balance it will add additional 20 to the unbilled.)
				if (tran.RemainderOfTranID != null)
					continue; //skip remainder transactions. 

				ProcessTransaction(project, res, pb);
			}
			
			RebuildAllocationTotals(project);

			if (options.RebuildCommitments == true)
			{
				ProcessPOCommitments(project);
				ProcessSOCommitments(project);
				ProcessExternalCommitments(project);
			}

			if(options.RecalculateDraftInvoicesAmount == true)
			{
				RecalculateDraftInvoicesAmount(project, pb);
			}

			if (options.RecalculateChangeOrders == true && project.ChangeOrderWorkflow == true)
			{
				RecalculateChangeOrders(project, pb, existingBudgetRecords);
			}

			InitCostCodeOnModifiedEntities();
		}

		public virtual void ProcessTransaction(PMProject project, PXResult<PMTran, Account, OffsetAccount, PMAccountGroup, OffsetPMAccountGroup> res, ProjectBalance pb)
		{
			PMTran tran = (PMTran)res;
			Account acc = (Account)res;
			PMAccountGroup ag = (PMAccountGroup)res;
			OffsetAccount offsetAcc = (OffsetAccount)res;
			OffsetPMAccountGroup offsetAg = (OffsetPMAccountGroup)res;
			
			IList<ProjectBalance.Result> balances = pb.Calculate(project, tran, acc, ag, offsetAcc, offsetAg);
			
			foreach (ProjectBalance.Result balance in balances)
			{
				if (balance.Status != null)
				{
					PMBudgetAccum ps = new PMBudgetAccum();
					ps.ProjectID = balance.Status.ProjectID;
					ps.ProjectTaskID = balance.Status.ProjectTaskID;
					ps.AccountGroupID = balance.Status.AccountGroupID;
					ps.InventoryID = balance.Status.InventoryID;
					ps.CostCodeID = balance.Status.CostCodeID;
					ps.UOM = balance.Status.UOM;
					ps.IsProduction = balance.Status.IsProduction;
					ps.Type = balance.Status.Type;
					ps.Description = balance.Status.Description;

					ps = Budget.Insert(ps);

					ps.ActualQty += balance.Status.ActualQty.GetValueOrDefault();
					ps.ActualAmount += balance.Status.ActualAmount.GetValueOrDefault();
				}

				if (balance.TaskTotal != null)
				{
					PMTaskTotal ta = new PMTaskTotal();
					ta.ProjectID = balance.TaskTotal.ProjectID;
					ta.TaskID = balance.TaskTotal.TaskID;

					ta = TaskTotals.Insert(ta);
					ta.Asset += balance.TaskTotal.Asset.GetValueOrDefault();
					ta.Liability += balance.TaskTotal.Liability.GetValueOrDefault();
					ta.Income += balance.TaskTotal.Income.GetValueOrDefault();
					ta.Expense += balance.TaskTotal.Expense.GetValueOrDefault();
				}


				foreach (PMHistory item in balance.History)
				{
					PMHistoryAccum hist = new PMHistoryAccum();
					hist.ProjectID = item.ProjectID;
					hist.ProjectTaskID = item.ProjectTaskID;
					hist.AccountGroupID = item.AccountGroupID;
					hist.InventoryID = item.InventoryID;
					hist.CostCodeID = item.CostCodeID;
					hist.PeriodID = item.PeriodID;

					hist = History.Insert(hist);
					hist.FinPTDAmount += item.FinPTDAmount.GetValueOrDefault();
					hist.FinYTDAmount += item.FinYTDAmount.GetValueOrDefault();
					hist.FinPTDQty += item.FinPTDQty.GetValueOrDefault();
					hist.FinYTDQty += item.FinYTDQty.GetValueOrDefault();
					hist.TranPTDAmount += item.TranPTDAmount.GetValueOrDefault();
					hist.TranYTDAmount += item.TranYTDAmount.GetValueOrDefault();
					hist.TranPTDQty += item.TranPTDQty.GetValueOrDefault();
					hist.TranYTDQty += item.TranYTDQty.GetValueOrDefault();
				}
			}
		}

		public virtual void RebuildAllocationTotals(PMProject project)
		{
			PXSelectBase<PMTran> select2 = new PXSelect<PMTran,
				Where<PMTran.origProjectID, Equal<Required<PMTran.origProjectID>>,
				And<PMTran.origTaskID, IsNotNull,
				And<PMTran.origAccountGroupID, IsNotNull>>>>(this);

			foreach (PMTran tran in select2.Select(project.ContractID))
			{
				PMTaskAllocTotalAccum tat = new PMTaskAllocTotalAccum();
				tat.ProjectID = tran.OrigProjectID;
				tat.TaskID = tran.OrigTaskID;
				tat.AccountGroupID = tran.OrigAccountGroupID;
				tat.InventoryID = tran.InventoryID.GetValueOrDefault(PMInventorySelectorAttribute.EmptyInventoryID);

				tat = AllocationTotals.Insert(tat);
				tat.Amount += tran.Amount.GetValueOrDefault();
				tat.Quantity += tran.Qty.GetValueOrDefault();
			}
		}

		public virtual void ProcessPOCommitments(PMProject project)
		{
			foreach (PXResult<POLine, POOrder> res in polines.Select(project.ContractID))
			{
				POLine poline = (POLine)res;
				POOrder order = (POOrder)res;
				PXParentAttribute.SetParent(polines.Cache, poline, typeof(POOrder), order);

				PMCommitmentAttribute.Sync(polines.Cache, poline);
			}

			foreach (APTran tran in aptran.Select(project.ContractID))
			{
				POLine poline = new POLine();
				poline.OrderType = tran.POOrderType;
				poline.OrderNbr = tran.PONbr;
				poline.LineNbr = tran.POLineNbr;

				poline = polines.Locate(poline);

				if (poline != null && poline.CommitmentID != null)
				{
					decimal sign = (tran.DrCr == DrCr.Debit) ? Decimal.One : Decimal.MinusOne;

					PMCommitment container = new PMCommitment();
					container.CommitmentID = poline.CommitmentID;
					container.UOM = tran.UOM;
					container.InventoryID = tran.InventoryID;
					container.CostCodeID = tran.CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode());
					container.InvoicedAmount = sign * tran.LineAmt.GetValueOrDefault();
					container.InvoicedQty = sign * tran.Qty.GetValueOrDefault();

					PMCommitmentAttribute.AddToInvoiced(polines.Cache, container);
				}
			}
		}

		public virtual void ProcessSOCommitments(PMProject project)
		{
			foreach (PXResult<SOLine, SOOrder> res in solines.Select(project.ContractID))
			{
				SOLine soline = (SOLine)res;
				SOOrder order = (SOOrder)res;
				PXParentAttribute.SetParent(solines.Cache, soline, typeof(SOOrder), order);

				PMCommitmentAttribute.Sync(solines.Cache, soline);
			}

			foreach (ARTran tran in artran.Select(project.ContractID))
			{
				SOLine soline = new SOLine();
				soline.OrderType = tran.SOOrderType;
				soline.OrderNbr = tran.SOOrderNbr;
				soline.LineNbr = tran.SOOrderLineNbr;

				soline = solines.Locate(soline);

				if (soline != null && soline.CommitmentID != null)
				{
					decimal sign = (tran.DrCr == DrCr.Credit) ? Decimal.One : Decimal.MinusOne;

					PMCommitment container = new PMCommitment();
					container.CommitmentID = soline.CommitmentID;
					container.UOM = tran.UOM;
					container.InventoryID = tran.InventoryID;
					container.CostCodeID = tran.CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode());
					container.InvoicedAmount = sign * tran.TranAmt.GetValueOrDefault();
					container.InvoicedQty = sign * tran.Qty.GetValueOrDefault();

					PMCommitmentAttribute.AddToInvoiced(solines.Cache, container);
				}
			}
		}

		public virtual void ProcessExternalCommitments(PMProject project)
		{
			foreach (PMCommitment item in ExternalCommitments.Select(project.ContractID))
			{
				ExternalCommitments.RollUpCommitmentBalance(polines.Cache, item, 1);
			}
		}

		public virtual void RecalculateDraftInvoicesAmount(PMProject project, ProjectBalance pb)
		{
			var selectProforma = new PXSelectJoinGroupBy<PMProformaLine,
				InnerJoin<Account, On<PMProformaLine.accountID, Equal<Account.accountID>>>,
				Where<PMProformaLine.projectID, Equal<Required<PMProformaLine.projectID>>,
				And<PMProformaLine.released, Equal<False>>>,
				Aggregate<GroupBy<PMProformaLine.projectID,
				GroupBy<PMProformaLine.taskID,
				GroupBy<PMProformaLine.accountID,
				GroupBy<PMProformaLine.inventoryID,
				GroupBy<PMProformaLine.costCodeID,
				Sum<PMProformaLine.lineTotal>>>>>>>>(this);

			var selectInvoice = new PXSelectJoinGroupBy<ARTran,
				InnerJoin<Account, On<ARTran.accountID, Equal<Account.accountID>>>,
				Where<ARTran.projectID, Equal<Required<ARTran.projectID>>,
				And<ARTran.released, Equal<False>>>,
				Aggregate<GroupBy<ARTran.tranType,
				GroupBy<ARTran.projectID, 
				GroupBy<ARTran.taskID,
				GroupBy<ARTran.accountID,
				GroupBy<ARTran.inventoryID,
				GroupBy<ARTran.costCodeID,
				Sum<ARTran.tranAmt>>>>>>>>>(this);


			var selectRevenueBudget = new PXSelect<PMRevenueBudget,
									Where<PMRevenueBudget.projectID, Equal<Required<PMRevenueBudget.projectID>>,
									And<PMRevenueBudget.type, Equal<GL.AccountType.income>>>>(this);
			var revenueBudget = selectRevenueBudget.Select(project.ContractID);

			foreach (PXResult<PMProformaLine, Account> res in selectProforma.Select(project.ContractID))
			{
				PMProformaLine line = (PMProformaLine)res;
				Account account = (Account)res;

				int? inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;

				foreach (PMRevenueBudget rev in revenueBudget)
				{
					foreach (PMRevenueBudget budget in revenueBudget)
					{
						if (budget.TaskID == line.TaskID && line.InventoryID == budget.InventoryID)
						{
							inventoryID = line.InventoryID;
						}
					}
				}

				PMBudgetAccum invoiced = new PMBudgetAccum();
				invoiced.Type = GL.AccountType.Income;
				invoiced.ProjectID = line.ProjectID;
				invoiced.ProjectTaskID = line.TaskID;
				invoiced.AccountGroupID = account.AccountGroupID;
				invoiced.InventoryID = inventoryID;
				invoiced.CostCodeID = line.CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode());

				invoiced = Budget.Insert(invoiced);
				invoiced.InvoicedAmount += line.LineTotal.GetValueOrDefault();

				if (line.IsPrepayment == true)
				{
					invoiced.PrepaymentInvoiced += line.LineTotal.GetValueOrDefault();
				}
			}

			foreach (PXResult<ARTran, Account> res in selectInvoice.Select(project.ContractID))
			{
				ARTran line = (ARTran)res;
				Account account = (Account)res;

				int? inventoryID = PMInventorySelectorAttribute.EmptyInventoryID;
				
				foreach (PMRevenueBudget rev in revenueBudget)
				{
					foreach (PMRevenueBudget budget in revenueBudget)
					{
						if (budget.TaskID == line.TaskID && line.InventoryID == budget.InventoryID)
						{
							inventoryID = line.InventoryID;
						}
					}
				}

				PMBudgetAccum invoiced = new PMBudgetAccum();
				invoiced.Type = GL.AccountType.Income;
				invoiced.ProjectID = line.ProjectID;
				invoiced.ProjectTaskID = line.TaskID;
				invoiced.AccountGroupID = account.AccountGroupID;
				invoiced.InventoryID = inventoryID;
				invoiced.CostCodeID = line.CostCodeID.GetValueOrDefault(CostCodeAttribute.GetDefaultCostCode());

				invoiced = Budget.Insert(invoiced);
				invoiced.InvoicedAmount += line.TranAmt.GetValueOrDefault() * ARDocType.SignAmount(line.TranType);
			}
		}

		public virtual void RecalculateChangeOrders(PMProject project, ProjectBalance projectBalance, Dictionary<BudgetKeyTuple, PMBudgetEx> existingBudgetRecords)
		{
			var select = new PXSelect<PMChangeOrderBudget,
				Where<PMChangeOrderBudget.projectID, Equal<Required<PMChangeOrderBudget.projectID>>,
				And<PMChangeOrderBudget.released, Equal<True>>>>(this);

			foreach (PMChangeOrderBudget change in select.Select(project.ContractID))
			{
				BudgetKeyTuple key = GetKey(change);

				PMBudgetEx existing = null;
				if (existingBudgetRecords.TryGetValue(key, out existing))
				{
					PMBudgetAccum budget = new PMBudgetAccum();
					budget.ProjectID = key.ProjectID;
					budget.ProjectTaskID = key.ProjectTaskID;
					budget.AccountGroupID = key.AccountGroupID;
					budget.InventoryID = key.InventoryID;
					budget.CostCodeID = key.CostCodeID;

					budget = Budget.Insert(budget);
					budget.ChangeOrderAmount += change.Amount.GetValueOrDefault();
					budget.RevisedAmount += change.Amount.GetValueOrDefault();

					var rollup = projectBalance.CalculateRollupQty<PMChangeOrderBudget>(change, existing);
					if (rollup.Qty.GetValueOrDefault() != 0)
					{
						budget.ChangeOrderQty += change.Qty.GetValueOrDefault();
						budget.RevisedQty += change.Qty.GetValueOrDefault();
					}
				}
				else
				{
					PMAccountGroup accountGroup = null;
					if (AccountGroups.TryGetValue(key.AccountGroupID, out accountGroup))
					{
						PMBudgetAccum budget = new PMBudgetAccum();
						budget.ProjectID = key.ProjectID;
						budget.ProjectTaskID = key.ProjectTaskID;
						budget.AccountGroupID = key.AccountGroupID;
						budget.InventoryID = key.InventoryID;
						budget.CostCodeID = key.CostCodeID;
						budget.Type = accountGroup.IsExpense == true ? GL.AccountType.Expense : accountGroup.Type;

						budget = Budget.Insert(budget);
						budget.ChangeOrderAmount += change.Amount.GetValueOrDefault();
						budget.RevisedAmount += change.Amount.GetValueOrDefault();
					}
				}
			}


		}

		public virtual void InitAccountGroup()
		{
			if (AccountGroups == null)
			{
				AccountGroups = new Dictionary<int, PMAccountGroup>();
				foreach(PMAccountGroup ag in PXSelect<PMAccountGroup>.Select(this))
				{
					AccountGroups.Add(ag.GroupID.Value, ag);
				}
			}
		} 

		public virtual void InitCostCodeOnModifiedEntities()
		{
			if (CostCodeAttribute.UseCostCode())
			{
				int? defaultCostCodeID = CostCodeAttribute.DefaultCostCode;
				foreach (POLine line in polines.Cache.Updated)
				{
					if ( line.CostCodeID == null )
					{
						polines.Cache.SetValue<POLine.costCodeID>(line, defaultCostCodeID);
					}
				}

				foreach (SOLine line in solines.Cache.Updated)
				{
					if (line.CostCodeID == null)
					{
						solines.Cache.SetValue<POLine.costCodeID>(line, defaultCostCodeID);
					}
				}
			}
		}

		public virtual string GetAccountGroupType(int? accountGroup)
		{
			PMAccountGroup ag = AccountGroups[accountGroup.Value];

			if (ag.Type == PMAccountType.OffBalance)
				return ag.IsExpense == true ? GL.AccountType.Expense : ag.Type;
			else
				return ag.Type;
		}

		public virtual ProjectBalance CreateProjectBalance()
		{
			return new ProjectBalance(this);
		}

		public List<PXDataFieldParam> BuildBudgetClearCommand(PMValidationFilter options, PMBudgetEx status)
		{
			List<PXDataFieldParam> list = new List<PXDataFieldParam>();
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.projectID).Name, PXDbType.Int, 4, status.ProjectID, PXComp.EQ));
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.accountGroupID).Name, PXDbType.Int, 4, status.AccountGroupID, PXComp.EQ));

			list.Add(new PXDataFieldAssign(typeof(PMBudget.type).Name, PXDbType.Char, 1, GetAccountGroupType(status.AccountGroupID)));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.actualAmount).Name, PXDbType.Decimal, 0m));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.actualQty).Name, PXDbType.Decimal, 0m));

			if (options.RebuildCommitments == true)
			{
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOpenAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOpenQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedReceivedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedInvoicedAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedInvoicedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOrigQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOrigAmount).Name, PXDbType.Decimal, 0m));
			}

			if (options.RecalculateDraftInvoicesAmount == true)
			{
				list.Add(new PXDataFieldAssign(typeof(PMBudget.invoicedAmount).Name, PXDbType.Decimal, 0m));
			}

			return list;
		}

		public List<PXDataFieldParam> BuildBudgetClearCommandWithChangeOrders(PMValidationFilter options, PMBudgetEx status)
		{
			List<PXDataFieldParam> list = new List<PXDataFieldParam>();
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.projectID).Name, PXDbType.Int, 4, status.ProjectID, PXComp.EQ));
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.accountGroupID).Name, PXDbType.Int, 4, status.AccountGroupID, PXComp.EQ));
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.projectTaskID).Name, PXDbType.Int, 4, status.ProjectTaskID, PXComp.EQ));
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.inventoryID).Name, PXDbType.Int, 4, status.InventoryID, PXComp.EQ));
			list.Add(new PXDataFieldRestrict(typeof(PMBudget.costCodeID).Name, PXDbType.Int, 4, status.CostCodeID, PXComp.EQ));

			list.Add(new PXDataFieldAssign(typeof(PMBudget.type).Name, PXDbType.Char, 1, GetAccountGroupType(status.AccountGroupID)));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.actualAmount).Name, PXDbType.Decimal, 0m));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.actualQty).Name, PXDbType.Decimal, 0m));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.revisedQty).Name, PXDbType.Decimal, status.Qty));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.revisedAmount).Name, PXDbType.Decimal, status.Amount));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.changeOrderQty).Name, PXDbType.Decimal, 0m));
			list.Add(new PXDataFieldAssign(typeof(PMBudget.changeOrderAmount).Name, PXDbType.Decimal, 0m));

			if (options.RebuildCommitments == true)
			{
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOpenAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOpenQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedReceivedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedInvoicedAmount).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedInvoicedQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOrigQty).Name, PXDbType.Decimal, 0m));
				list.Add(new PXDataFieldAssign(typeof(PMBudget.committedOrigAmount).Name, PXDbType.Decimal, 0m));
			}

			if (options.RecalculateDraftInvoicesAmount == true)
			{
				list.Add(new PXDataFieldAssign(typeof(PMBudget.invoicedAmount).Name, PXDbType.Decimal, 0m));
			}

			return list;
		}

		private BudgetKeyTuple GetKey(PMBudget record)
		{
			return new BudgetKeyTuple(record.ProjectID.Value, record.ProjectTaskID.Value, record.AccountGroupID.Value, record.InventoryID.Value, record.CostCodeID.Value);
		}

		private BudgetKeyTuple GetKey(PMChangeOrderBudget record)
		{
			return new BudgetKeyTuple(record.ProjectID.Value, record.ProjectTaskID.Value, record.AccountGroupID.Value, record.InventoryID.Value, record.CostCodeID.Value);
		}

		[PXHidden]
        [Serializable]
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public partial class OffsetAccount : Account
		{
			#region AccountID
			public new abstract class accountID : PX.Data.IBqlField
			{
			}
			#endregion
			#region AccountCD
			public new abstract class accountCD : PX.Data.IBqlField
			{
			}
			#endregion
			#region AccountGroupID
			public new abstract class accountGroupID : PX.Data.IBqlField
			{
			}
			#endregion
		}

		[PXHidden]
        [Serializable]
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public partial class OffsetPMAccountGroup : PMAccountGroup
		{
			#region GroupID
			public new abstract class groupID : PX.Data.IBqlField
			{
			}

			#endregion
			#region GroupCD
			public new abstract class groupCD : PX.Data.IBqlField
			{
			}
			#endregion
			#region Type
			public new abstract class type : PX.Data.IBqlField
			{
			}
			#endregion
		}

		[PXHidden]
        [Serializable]
		[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
		public partial class PMBudgetEx : PMBudget
		{
			#region AccountGroupID
			public new abstract class accountGroupID : PX.Data.IBqlField
			{
			}
			#endregion
			#region ProjectID
			public new abstract class projectID : PX.Data.IBqlField
			{
			}
			#endregion
			#region ProjectTaskID
			public new abstract class projectTaskID : PX.Data.IBqlField
			{
			}
			
			#endregion
			#region InventoryID
			public new abstract class inventoryID : PX.Data.IBqlField
			{
			}
			
			#endregion
		}
	}

	[Serializable]
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	public partial class PMValidationFilter : IBqlTable
	{
		#region RecalculateUnbilledSummary
		public abstract class recalculateUnbilledSummary : PX.Data.IBqlField
		{
		}
		protected Boolean? _RecalculateUnbilledSummary;
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Recalculate Unbilled Summary")]
		public virtual Boolean? RecalculateUnbilledSummary
		{
			get
			{
				return this._RecalculateUnbilledSummary;
			}
			set
			{
				this._RecalculateUnbilledSummary = value;
			}
		}
		#endregion
		#region RecalculateDraftInvoicesAmount
		public abstract class recalculateDraftInvoicesAmount : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Recalculate Draft Invoices Amount")]
		public virtual Boolean? RecalculateDraftInvoicesAmount
		{
			get;
			set;
		}
		#endregion
		#region RebuildCommitments
		public abstract class rebuildCommitments : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Rebuild Commitments")]
		public virtual Boolean? RebuildCommitments
		{
			get;
			set;
		}
		#endregion
		#region RecalculateChangeOrders
		public abstract class recalculateChangeOrders : PX.Data.IBqlField
		{
		}
		[PXDBBool()]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Recalculate Change Orders", FieldClass = PMChangeOrder.FieldClass)]
		public virtual Boolean? RecalculateChangeOrders
		{
			get;
			set;
		}
		#endregion
	}
}
