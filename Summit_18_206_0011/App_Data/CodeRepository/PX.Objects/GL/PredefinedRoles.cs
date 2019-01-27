using PX.Common;

namespace PX.Objects.GL
{
	public static class PredefinedRoles
	{
		public static string FinancialSupervisor => WebConfig.GetString("FinancialSupervisor", "Financial Supervisor");
	}
}
