using PX.Common;

namespace USScouts
{
	[PXLocalizable(Prefix)]
	public static class USMessages
	{

		public const string Prefix = "UpScouts Messages";

		public const string MainError = "Please contact your System Administrator, the following issues have been found:";

		public const string SomePluginRunning = "SomePlugin running on Company {0}";

		public const string NoActiveAccounts = "There are no Active Accounts in Company {0}, Package cannot be published.";

		public const string ErrorInsertingUpScouts = "An exception occurred while trying to insert UpScout records on Company {0}.";

	}
}