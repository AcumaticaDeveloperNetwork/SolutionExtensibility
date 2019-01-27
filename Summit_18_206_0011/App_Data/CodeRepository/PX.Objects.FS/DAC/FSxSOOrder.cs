using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

namespace PX.Objects.FS
{
    [PXTable(IsOptional = true)]
    public class FSxSOOrder : PXCacheExtension<SOOrder>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region AssignedEmpID
        public abstract class assignedEmpID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [FSSelector_StaffMember_All]
        [PXUIField(DisplayName = "Assigned to", Visible = false)]
        public virtual int? AssignedEmpID { get; set; }
        #endregion        
        #region SDEnabled
        public abstract class sDEnabled : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Create Service Order", Enabled = false, Visible = false)]
        public virtual bool? SDEnabled { get; set; }
        #endregion
        #region SrvOrdType
        public abstract class srvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName = "Service Order Type", Enabled = false, Required = false, Visible = false)]
        [FSSelectorSalesOrderSrvOrdType]
        public virtual string SrvOrdType { get; set; }
        #endregion
        #region Installed
        public abstract class installed : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Installed", Enabled = false, Visible = false)]
        public virtual bool? Installed { get; set; }
        #endregion
        #region SLAETA
        public abstract class sLAETA : PX.Data.IBqlField
        {
        }

        protected DateTime? _SLAETA;
        [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
        [PXUIField(DisplayName = "Deadline - SLA", Enabled = false, Visible = false)]
        public virtual DateTime? SLAETA
        {
            get
            {
                return this._SLAETA;
            }

            set
            {
                this.SLAETAUTC = value;
                this._SLAETA = value;
            }
        }
        #endregion

        #region SORefNbr
        public abstract class soRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Service Order Nbr.", Visibility = PXUIVisibility.SelectorVisible, Visible = false, Enabled = false)]
        [PXSelector(typeof(Search<FSServiceOrder.refNbr,
            Where<
            FSServiceOrder.srvOrdType, Equal<Current<FSxSOOrder.srvOrdType>>>>))]

        public virtual string SORefNbr { get; set; }
        #endregion

        #region UTC Fields
        #region SLAETAUTC
        public abstract class sLAETAUTC : PX.Data.IBqlField
        {
        }

        [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
        [PXUIField(DisplayName = "Deadline - SLA", Enabled = false, Visible = false)]
        public virtual DateTime? SLAETAUTC { get; set; }
        #endregion
        #endregion

        #region IsFSIntegrated
        public abstract class isFSIntegrated : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(Enabled = false, Visible = false)]
        [SkipSetExtensionVisibleInvisible]
        public bool? IsFSIntegrated { get; set; }
        #endregion
    }
}