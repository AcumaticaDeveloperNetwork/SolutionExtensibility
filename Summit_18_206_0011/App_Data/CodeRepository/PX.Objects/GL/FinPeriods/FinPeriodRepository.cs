using System;
using System.Collections.Generic;
using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.GL.BQL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.Exceptions;

namespace PX.Objects.GL.FinPeriods
{
	public class FinPeriodRepository : IFinPeriodRepository
	{
		protected readonly PXGraph Graph;

		public FinPeriodRepository(PXGraph graph)
		{
			Graph = graph;
		}

		public int? GetCalendarOrganizationID(int? organizationID, int? branchID, bool? useMasterCalendar)
		{
			if (useMasterCalendar == true)
			{
				return FinPeriod.organizationID.MasterValue;
			}
			else
			{
				if (branchID != null)
					return PXAccess.GetParentOrganizationID(branchID);

				return organizationID;
			}
		}

		public int? GetCalendarOrganizationID(int? branchID, bool? useMasterCalendar)
		{
			if (useMasterCalendar == true)
			{
				return FinPeriod.organizationID.MasterValue;
			}
			else
			{
				return PXAccess.GetParentOrganizationID(branchID);
			}
		}

		public FinPeriod FindMaxFinPeriodWithEndDataBelongToInterval(DateTime? startDate, DateTime? endDate, int? organizationID)
		{
			return PXSelect<FinPeriod,
					Where<FinPeriod.finDate, GreaterEqual<Required<FinPeriod.finDate>>,
						And<FinPeriod.finDate, Less<Required<FinPeriod.finDate>>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>,
					OrderBy<Desc<FinPeriod.finPeriodID>>>
				.Select(Graph, startDate, endDate, organizationID);
		}

		/// <summary>
		/// Returns PeriodID from the given date.
		/// </summary>
		public string GetPeriodIDFromDate(DateTime? date, int? organizationID)
		{
			FinPeriod period = GetFinPeriodByDate(date, organizationID);

			return period.FinPeriodID;
		}

		public FinPeriod GetFinPeriodByDate(DateTime? date, int? organizationID)
		{
			FinPeriod finPeriod = FindFinPeriodByDate(date, organizationID);

			if (finPeriod == null)
			{
				throw new FinancialPeriodNotDefinedForDateException(date);
			}

			return finPeriod;
		}

		public FinPeriod FindFinPeriodByDate(DateTime? date, int? organizationID)
		{
			return PXSelect<
					FinPeriod,
					Where<
						FinPeriod.startDate, LessEqual<Required<FinPeriod.startDate>>,
						And<FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>,
							And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>
				.Select(Graph, date, date, organizationID);
		}

		public string GetOffsetPeriodId(string finPeriodID, int offset, int? organizationID)
		{
			string offsetPeriod = FindOffsetPeriodId(finPeriodID, offset, organizationID);

			if (offsetPeriod == null)
			{
				throw new FinancialPeriodOffsetNotFoundException(finPeriodID, offset);
			}

			return offsetPeriod;
		}

		public string FindOffsetPeriodId(string finPeriodID, int offset, int? organizationID)
		{
			FinYearSetup setup = PXSelect<FinYearSetup>.Select(Graph);

			//TODO: Need to refactor, duplicates the part of function FABookPeriodIDAttribute.GetBookPeriodsInYear
			FinPeriodSetup periodsInYear = PXSelectGroupBy<FinPeriodSetup, Where<FinPeriodSetup.endDate, Greater<FinPeriodSetup.startDate>>,
				Aggregate<Max<FinPeriodSetup.periodNbr>>>.Select(Graph);
			if (setup != null && FiscalPeriodSetupCreator.IsFixedLengthPeriod(setup.FPType) &&
			    periodsInYear != null && periodsInYear.PeriodNbr != null)
			{
				return FinPeriodUtils.OffsetPeriod(finPeriodID, offset, Convert.ToInt32(periodsInYear.PeriodNbr));
			}
			else if (offset > 0)
			{
				PXResultset<FinPeriod> res = PXSelect<
						FinPeriod,
						Where<
							FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>,
							And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
							And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>,
						OrderBy<
							Asc<FinPeriod.finPeriodID>>>
					.SelectWindowed(Graph, 0, offset, finPeriodID, organizationID);

				if (res.Count < offset)
				{
					return null;
				}

				return ((FinPeriod)res[res.Count - 1]).FinPeriodID;
			}
			else if (offset < 0)
			{
				PXResultset<FinPeriod> res = PXSelect<
						FinPeriod,
						Where<
							FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>,
							And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
							And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>,
						OrderBy<
							Desc<FinPeriod.finPeriodID>>>
					.SelectWindowed(Graph, 0, -offset, finPeriodID, organizationID);

				if (res.Count < -offset)
				{
					return null;
				}

				return ((FinPeriod)res[res.Count - 1]).FinPeriodID;
			}
			else
			{
				return finPeriodID;
			}
		}

		/// <summary>
		/// Returns Next Period from the given.
		/// </summary>
		public string NextPeriod(string finPeriodID, int? organizationID)
		{
			return GetOffsetPeriodId(finPeriodID, 1, organizationID);
		}

		/// <summary>
		/// Returns Start date for the given Period
		/// </summary>
		public DateTime PeriodStartDate(string finPeriodID, int? organizationID)
		{
			FinPeriod financialPeriod = PXSelect<
					FinPeriod,
					Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>
				.Select(Graph, finPeriodID, organizationID);

			if (financialPeriod == null)
			{
				throw new FinancialPeriodWithIdNotFoundException(finPeriodID);
			}

			return (DateTime)financialPeriod.StartDate;
		}

		/// <summary>
		/// Returns End date for the given period
		/// </summary>
		public DateTime PeriodEndDate(string finPeriodID, int? organizationID)
		{
			FinPeriod financialPeriod = PXSelect<
					FinPeriod,
					Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>
				.Select(Graph, finPeriodID, organizationID);

			if (financialPeriod == null)
			{
				throw new FinancialPeriodWithIdNotFoundException(finPeriodID);
			}

			return financialPeriod.EndDate.Value.AddDays(-1);
		}

		public IEnumerable<FinPeriod> GetFinPeriodsInInterval(DateTime? fromDate, DateTime? tillDate, int? organizationID)
		{
			return PXSelect<FinPeriod,
							Where<FinPeriod.startDate, GreaterEqual<Required<FinPeriod.startDate>>,
								And<FinPeriod.endDate, LessEqual<Required<FinPeriod.endDate>>,
								And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
								And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>>
							.Select(Graph, fromDate, tillDate, organizationID)
							.RowCast<FinPeriod>();
		}

		public IEnumerable<FinPeriod> GetAdjustmentFinPeriods(string finYear, int? organizationID)
		{
			return PXSelect<FinPeriod,
							Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>,
								And<FinPeriod.startDate, Equal<FinPeriod.endDate>,
								And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>
							.Select(Graph, finYear)
							.RowCast<FinPeriod>();
		}

		public FinPeriod FindLastYearNotAdjustmentPeriod(string finYear, int? organizationID)
		{
			return (FinPeriod)PXSelect<FinPeriod,
					Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>,
						And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>,
					OrderBy<Desc<FinPeriod.finPeriodID>>>
				.SelectWindowed(Graph, 0, 1, finYear, organizationID);
		}

		public FinPeriod FindLastFinancialPeriodOfYear(string finYear, int? organizationID)
			=> PXSelect<
					FinPeriod,
					Where<FinPeriod.finYear, Equal<Required<FinPeriod.finYear>>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>,
					OrderBy<Desc<FinPeriod.finPeriodID>>>
					.SelectWindowed(Graph, 0, 1, finYear, organizationID);

		/// <summary>
		/// Returns a minimal set of financial periods that contain a given date interval
		/// within them, excluding any adjustment periods.
		/// </summary>
		/// <param name="Graph">The Graph which will be used when performing a select DB query.</param>
		/// <param name="startDate">The starting date of the date interval.</param>
		/// <param name="endDate">The ending date of the date interval.</param>
		public IEnumerable<FinPeriod> PeriodsBetweenInclusive(DateTime startDate, DateTime endDate, int? organizationID)
		{
			if (startDate > endDate)
			{
				throw new PXArgumentException(nameof(startDate));
			}

			return PXSelect<
					FinPeriod,
					Where<
						FinPeriod.endDate, Greater<Required<FinPeriod.endDate>>,
						And<FinPeriod.startDate, LessEqual<Required<FinPeriod.startDate>>,
						And<FinPeriod.startDate, NotEqual<FinPeriod.endDate>,
						And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>>>
						.Select(Graph, startDate, endDate, organizationID)
						.RowCast<FinPeriod>();
		}

		public void CheckIsDateWithinPeriod(string finPeriodID, int? organizationID, DateTime date, string errorMessage)
		{
			if (!IsDateWithinPeriod(finPeriodID, date, organizationID))
			{
				throw new PXSetPropertyException(errorMessage);
			}
		}

		public bool IsDateWithinPeriod(string finPeriodID, DateTime date, int? organizationID)
		{
			FinPeriod finPeriod = GetByID(finPeriodID, organizationID);

			return date >= finPeriod.StartDate && date < finPeriod.EndDate;
		}

		public bool PeriodExists(string finPeriodID, int? organizationID)
		{
			FinPeriod finPeriod = FindByID(organizationID, finPeriodID);

			return finPeriod != null;
		}

		/// <summary>
		/// Gets the ID of the financial period with the same <see cref="PX.Objects.GL.Obsolete.FinPeriod.PeriodNbr"/> 
		/// as the one specified, but residing in the previous financial year. If no such financial 
		/// period exists, an exception is thrown.
		/// </summary>
		public string GetSamePeriodInPreviousYear(string finPeriodID, int? organizationID)
		{
			string yearPart = finPeriodID.Substring(0, 4);
			string periodNumber = finPeriodID.Substring(4, 2);

			string previousYear = (int.Parse(yearPart) - 1).ToString();
			string suggestedPeriodID = string.Concat(previousYear, periodNumber);

			if (!PeriodExists(suggestedPeriodID, organizationID))
			{
				throw new FinancialPeriodWithIdNotFoundException(suggestedPeriodID);
			}

			return suggestedPeriodID;
		}

		public FinPeriod GetByID(string finPeriodID, int? organizationID)
		{
			FinPeriod finPeriod = FindByID(organizationID, finPeriodID);

			if (finPeriod == null)
			{
				//TODO 222 Pank EntityHelper.GetFriendlyEntityName(typeof(FinPeriod))
				throw new PXException(Common.Messages.EntityWithIDDoesNotExist, 
										EntityHelper.GetFriendlyEntityName(typeof(FinPeriod)),
										finPeriodID);
			}

			return finPeriod;
		}

		public FinPeriod FindByID(int? organizationID, string finPeriodID)
		{
			return PXSelect<FinPeriod,
						Where<FinPeriod.finPeriodID, Equal<Required<FinPeriod.finPeriodID>>,
							And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>>
						.Select(Graph, finPeriodID, organizationID);
		}

		public FinPeriod FindPrevPeriod(int? organizationID, string finPeriodID, bool looped = false)
		{
			FinPeriod nextperiod = null;
			if (!string.IsNullOrEmpty(finPeriodID))
			{
				nextperiod =
						PXSelect<FinPeriod,
							Where<FinPeriod.finPeriodID, Less<Required<FinPeriod.finPeriodID>>,
								And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>,
							OrderBy<Desc<FinPeriod.finPeriodID>>>
							.SelectWindowed(Graph, 0, 1, finPeriodID, organizationID);
			}
			if (looped && nextperiod == null)
			{
				nextperiod = FindFirstPeriod(organizationID);

			}
			return nextperiod;
		}

		public FinPeriod FindNextPeriod(int? organizationID, string finPeriodID, bool looped = false)
		{
			FinPeriod nextperiod = null;
			if (!string.IsNullOrEmpty(finPeriodID))
			{
				nextperiod =
						PXSelect<FinPeriod,
							Where<FinPeriod.finPeriodID, Greater<Required<FinPeriod.finPeriodID>>,
								And<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>>,
							OrderBy<Asc<FinPeriod.finPeriodID>>>
							.SelectWindowed(Graph, 0, 1, finPeriodID, organizationID);
			}
			if (looped && nextperiod == null)
			{
				nextperiod = FindLastPeriod(organizationID);
			}
			return nextperiod;
		}

		public FinPeriod FindFirstPeriod(int? organizationID)
		{
			return PXSelect<FinPeriod,
					Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>,
					OrderBy<Asc<FinPeriod.finPeriodID>>>
				.SelectWindowed(Graph, 0, 1, organizationID);
		}

		public FinPeriod FindLastPeriod(int? organizationID)
		{
			return PXSelect<FinPeriod,
							Where<FinPeriod.organizationID, Equal<Required<FinPeriod.organizationID>>>,
							OrderBy<Desc<FinPeriod.finPeriodID>>>
							.SelectWindowed(Graph, 0, 1, organizationID);
		}

		public OrganizationFinPeriod FindFirstOpenFinPeriod(string fromFinPeriodID, int? organizationID, Type fieldModuleClosed = null)
		{
			BqlCommand select =
				BqlCommand.CreateInstance(typeof(Select<OrganizationFinPeriod,
					Where<OrganizationFinPeriod.status, Equal<FinPeriod.status.open>,
						And<OrganizationFinPeriod.finPeriodID, GreaterEqual<Required<OrganizationFinPeriod.finPeriodID>>,
						And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>,
					OrderBy<Asc<OrganizationFinPeriod.finPeriodID>>>));

			if (fieldModuleClosed != null)
			{
				select = select.WhereAnd(BqlCommand.Compose(typeof(Where<,>), fieldModuleClosed, typeof(NotEqual<True>)));
			}

			return (OrganizationFinPeriod) (new PXView(Graph, false, select).SelectSingle(fromFinPeriodID, organizationID));
		}
	}
}
