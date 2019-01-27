using Customization;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace USScouts
{
	public sealed class USCustomizationPlugin : CustomizationPlugin
	{

		//For DB Events
		public override void UpdateDatabase()
		{
			List<Tuple<string, object[]>> errors = new List<Tuple<string, object[]>>();

			SomeOtherPlugin.UpdateDatabase(this, ref errors);

			HandleExceptions(errors);
		}

		//Fires after the publish is finished. Don't make DB changes here
		public override void OnPublished()
		{
			List<Tuple<string, object[]>> errors = new List<Tuple<string, object[]>>();

			SomePlugin.OnPublished(this, ref errors);
		}

		private void HandleExceptions(List<Tuple<string, object[]>> errors)
		{
			if (errors.Count != 0)
			{
				//Normally you shouldn't localize Exceptions, but this code generates an amalgamation of errors.
				StringBuilder builder = new StringBuilder(PXLocalizer.Localize(USMessages.MainError, typeof(USMessages).FullName));

				foreach (Tuple<string, object[]> error in errors)
				{
					builder.AppendLine(PXLocalizer.LocalizeFormatWithKey(error.Item1, typeof(USMessages).FullName, error.Item2));
				}

				throw new PXException(builder.ToString());
			}
		}

	}
}