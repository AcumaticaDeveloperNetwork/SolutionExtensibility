using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USAPTranExtension : PXCacheExtension<APTran>
    {
        #region ScoutID
        public abstract class usrUSScoutID : PX.Data.IBqlField
        {
        }
        [USScoutTran]
        public virtual int? UsrUSScoutID { get; set; }
        #endregion

        #region IsActive
        public static bool IsActive() { return PXDatabaseUSProvider.GetFeatureActive(); }
        #endregion
    }
}
