using PX.Data;
using System;
using System.Collections.Generic;

namespace USScouts
{
	public class SomeOtherPlugin
	{

		public static void UpdateDatabase(USCustomizationPlugin plugin, ref List<Tuple<string, object[]>> errors)
		{
			string companyName = PXAccess.GetCompanyName();

			try
			{
				//Insert UpScouts here or something
			}
			catch (Exception)
			{
				errors.Add(new Tuple<string, object[]>(USMessages.ErrorInsertingUpScouts, new object[] { companyName }));
			}
		}

	}
}