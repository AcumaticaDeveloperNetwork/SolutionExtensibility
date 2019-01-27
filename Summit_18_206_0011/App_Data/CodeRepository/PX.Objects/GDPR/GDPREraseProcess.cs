using System.Collections;
using System.Linq;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using PX.Common;

namespace PX.Objects.GDPR
{
	public class GDPREraseProcess : GDPRPseudonymizeProcess
	{
		#region ctor

		public GDPREraseProcess()
		{
			SelectedItems.SetProcessCaption(Messages.Erase);
			SelectedItems.SetProcessAllCaption(Messages.EraseAll);

			TopLevelProcessor = TopLevelProcessorImpl;
			ChildLevelProcessor = ChildLevelProcessorImpl;

			GetPseudonymizationStatus = typeof(PXPseudonymizationStatusListAttribute.notPseudonymized);
			SetPseudonymizationStatus = PXPseudonymizationStatusListAttribute.Erased;
		}

		#endregion

		#region Implementation

		private static void TopLevelProcessorImpl(string combinedKey, Guid? topParentNoteID, string info)
		{
			DeleteSearchIndex(topParentNoteID);
		}

		private static void ChildLevelProcessorImpl(PXGraph processingGraph, Type childTable, IEnumerable<PXPersonalDataFieldAttribute> fields, IEnumerable<object> childs, Guid? topParentNoteID)
		{
			PseudonymizeChilds(processingGraph, childTable, fields, childs);
			
			WipeAudit(processingGraph, childTable, fields, childs);
		}
		
		#endregion
	}
}
