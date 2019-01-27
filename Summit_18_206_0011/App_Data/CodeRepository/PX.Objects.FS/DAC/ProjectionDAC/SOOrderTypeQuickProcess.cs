using System;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO;

namespace PX.Objects.FS
{
    #region PXProjection
    [Serializable]
    [PXBreakInheritance]
    [PXProjection(typeof(Select<SOOrderType>))]
    #endregion
    public class SOOrderTypeQuickProcess : SOOrderType
    {
        #region OrderType
        public new abstract class orderType : IBqlField { }
        #endregion
        #region Behavior
        public new abstract class behavior : IBqlField { }
        #endregion
        #region AllowQuickProcess
        public new abstract class allowQuickProcess : IBqlField { }
        #endregion
    }
}
