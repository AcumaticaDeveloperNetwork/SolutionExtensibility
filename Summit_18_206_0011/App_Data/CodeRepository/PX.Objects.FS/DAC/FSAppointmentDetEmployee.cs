using PX.Data;
using PX.Objects.IN;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSAppointmentDetEmployee : FSAppointmentDet
    {
        public new abstract class appointmentID : PX.Data.IBqlField
        {
        }

        public new abstract class sODetID : PX.Data.IBqlField
        {
        }

        public new abstract class lineType : PX.Data.IBqlField
        {
        }


        #region LineRef
        public new abstract class lineRef : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true)]
        [PXUIField(DisplayName = "Line Ref.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public override string LineRef { get; set; }
        #endregion

        #region InventoryID
        public new abstract class inventoryID : PX.Data.IBqlField
        {            
        }
        
        #endregion
    }
}