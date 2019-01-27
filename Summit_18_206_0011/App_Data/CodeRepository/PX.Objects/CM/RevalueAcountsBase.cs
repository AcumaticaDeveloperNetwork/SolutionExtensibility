using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Tools;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.FinPeriods;

namespace PX.Objects.CM
{
	public class RevalueAcountsBase<THistory> : PXGraph<RevalueAcountsBase<THistory>> 
		where THistory : class, IBqlTable, new()
	{
		private IFinPeriodRepository _finPeriodRepository;
		public IFinPeriodRepository FinPeriodRepository => Cached.GetValue(ref _finPeriodRepository, GetService<IFinPeriodRepository>);

		private IFinPeriodUtils _finPeriodUtils;
		public IFinPeriodUtils FinPeriodUtils => Cached.GetValue(ref _finPeriodUtils, GetService<IFinPeriodUtils>);

		public virtual ProcessingResult CheckFinPeriod(string finPeriodID, int? branchID)
		{
			ProcessingResult result = new ProcessingResult();
			int? organizationID = PXAccess.GetParentOrganizationID(branchID);
			FinPeriod period = FinPeriodRepository.FindByID(organizationID, finPeriodID);

			if (period == null)
			{
				result.AddErrorMessage(GL.Messages.FinPeriodDoesNotExistForCompany,
						FinPeriodIDFormattingAttribute.FormatForError(finPeriodID),
						PXAccess.GetOrganizationCD(PXAccess.GetParentOrganizationID(branchID)));
			}
			else
			{
				result = FinPeriodUtils.CanPostToPeriod(period);
			}

			if (!result.IsSuccess)
			{
				PXProcessing<THistory>.SetError(new PXException(result.GetGeneralMessage()));
			}

			return result;
		}
	}
}
