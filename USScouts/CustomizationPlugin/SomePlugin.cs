using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;

namespace USScouts
{
	public static class SomePlugin
	{

		public static void OnPublished(USCustomizationPlugin plugin, ref List<Tuple<string, object[]>> errors)
		{
			string companyName = PXAccess.GetCompanyName();

			plugin.WriteLog(PXLocalizer.LocalizeFormatWithKey(USMessages.SomePluginRunning, typeof(USMessages).FullName, companyName));

			//Select an Account that is Active
			PXDataRecord record = PXDatabase.SelectSingle<Account>(new PXDataFieldValue<Account.active>(PXDbType.Bit, true));

			if (record == null)
			{
				errors.Add(new Tuple<string, object[]>(USMessages.NoActiveAccounts, new object[] { companyName }));
			}
		}

	}
}