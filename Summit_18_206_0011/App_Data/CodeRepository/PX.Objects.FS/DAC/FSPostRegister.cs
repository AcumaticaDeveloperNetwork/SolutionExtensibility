using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSPostRegister : PX.Data.IBqlTable
    {
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA")]
        public virtual string SrvOrdType { get; set; }
        #endregion
        #region RefNbr
        public abstract class refNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(20, IsKey = true)]
        public virtual string RefNbr { get; set; }
        #endregion
        #region Type
        public abstract class type : PX.Data.IBqlField
        {
        }

        [PXDBString(5, IsFixed = true, IsKey = true)]
        public virtual string Type { get; set; }
        #endregion
        #region EntityType
        public abstract class entityType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        public virtual string EntityType { get; set; }
        #endregion
        #region BatchID
        public abstract class batchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? BatchID { get; set; }
        #endregion
        #region ProcessID
        public abstract class processID : IBqlField
        {
        }

        [PXDBGuid]
        public virtual Guid? ProcessID { get; set; }
        #endregion
        #region PostedTO
        public abstract class postedTO : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        public virtual string PostedTO { get; set; }
        #endregion
        #region PostDocType
        public abstract class postDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
        public virtual string PostDocType { get; set; }
        #endregion
        #region PostRefNbr
        public abstract class postRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        public virtual string PostRefNbr { get; set; }
        #endregion
    }
}