using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;

namespace PX.Objects.FS
{
    [PXTable(typeof(InventoryItem.inventoryID), IsOptional = true)]
    public class FSxService : PXCacheExtension<InventoryItem>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
        }

        #region AutoShowNote
        public abstract class autoShowNote : PX.Data.IBqlField
		{
		}		

		[PXDBBool]
		[PXDefault(false)]
		[PXUIField(DisplayName = "Open Note When Service Is Selected")]
		public virtual bool? AutoShowNote { get; set; }
		#endregion
        #region BillingRule
        public abstract class billingRule : ListField_BillingRule
        {
        }

        [PXDBString(4, IsFixed = true)]
        [billingRule.List]
        [PXDefault(ID.BillingRule.TIME)]
        [PXUIField(DisplayName = "Default Billing Rule")]
        public virtual string BillingRule { get; set; }
        #endregion
        #region EstimatedDuration
        public abstract class estimatedDuration : PX.Data.IBqlField
		{
		}			

	    [PXDefault(1)]
        [PXUIField(DisplayName = "Estimated Duration")]        
        [PXDBTimeSpanLong(Format = TimeSpanFormatType.LongHoursMinutes)]
        public virtual int? EstimatedDuration { get; set; }
        #endregion
        #region ActionType
        public abstract class actionType : ListField_Service_Action_Type
        {
        }

        [PXDBString(1, IsFixed = true)]
        [PXDefault(ID.Service_Action_Type.NO_ITEMS_RELATED)]
        [actionType.List]
        [PXUIField(DisplayName = "Pickup/Delivery Action", Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
        public virtual string ActionType { get; set; }
        #endregion
        #region PendingBasePrice
        public abstract class pendingBasePrice : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Pending Base Price", Enabled = false, Visible = false)]
        public virtual int? PendingBasePrice { get; set; }
        #endregion
        #region PendingBasePriceDate
        public abstract class pendingBasePriceDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXUIField(DisplayName = "Pending Base Price Date", Enabled = false, Visible = false)]
        public virtual int? PendingBasePriceDate { get; set; }
        #endregion
        #region BasePriceDate
        public abstract class basePriceDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXUIField(DisplayName = "Base Price Date", Enabled = false, Visible = false)]
        public virtual int? BasePriceDate { get; set; }
        #endregion
        #region LastBasePrice
        public abstract class lastBasePrice : PX.Data.IBqlField
        {
        }

        [PXInt]
        [PXUIField(DisplayName = "Last Base Price", Enabled = false, Visible = false)]
        public virtual int? LastBasePrice { get; set; }
        #endregion
        #region DfltEarningType
        public abstract class dfltEarningType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, IsUnicode = false, InputMask = ">LL")]
        [PXDefault(typeof(Search<EPSetup.regularHoursType>), PersistingCheck = PXPersistingCheck.Nothing)]
        [PXSelector(typeof(EPEarningType.typeCD))]
        [PXUIField(DisplayName = "Default Earning Type")]
        public virtual string DfltEarningType { get; set; }
        #endregion
        #region ChkServiceManagement
        public abstract class ChkServiceManagement : PX.Data.IBqlField
        {
        }

        [PXBool]
        [PXUIField(Visible = false)]
        public virtual bool? chkServiceManagement
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}
