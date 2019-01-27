using PX.Data;
using PX.SM;

namespace PX.Objects.FS
{
    [System.SerializableAttribute]
    public class FSEmployeeFilter : PX.Data.IBqlTable
    {
        #region BAccountID
        public abstract class bAccountID : PX.Data.IBqlField
        {
        }

        [PXInt(IsKey = true)]
        [PXUIField(DisplayName = "Employee Name")]
        [FSSelector_Employee_All]
        public virtual int? BAccountID { get; set; }
        #endregion

        // This dummy field is for avoiding access to Staff Schedule Rules without setting Service Management Preferences
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXString(4, IsKey = true, IsFixed = true)]
        [PXDefault(typeof(FSSetup.dfltSrvOrdType))]
        [FSSelectorSrvOrdType]
        public virtual string SrvOrdType { get; set; }
        #endregion
    }
}
