using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USSetupMaint : PXGraph<USSetupMaint>
    {
        #region Views
        public PXSave<USSetup> Save;

        public PXCancel<USSetup> Cancel;

        public PXSelect<USSetup> Setup;
        #endregion

        public override void Persist()
        {
            WebDialogResult result = Setup.Ask(ActionsMessages.Warning, PXMessages.LocalizeFormatNoPrefix("Saving will modify instance behavior"), MessageButtons.OKCancel, MessageIcon.Warning, true);
            //        //checking answer	
            if (result == WebDialogResult.OK)
            {
                base.Persist();
                PXDatabase.ResetSlots();
                PXPageCacheUtils.InvalidateCachedPages();
                this.Clear();
                throw new PXRefreshException();
            }
        }
    }
}
