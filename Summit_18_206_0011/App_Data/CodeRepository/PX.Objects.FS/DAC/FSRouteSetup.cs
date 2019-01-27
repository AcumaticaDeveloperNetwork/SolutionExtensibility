using System;
using PX.Data;
using PX.Objects.CS;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.ROUTES_SETUP)]
    [PXPrimaryGraph(typeof(RouteSetupMaint))]
    public class FSRouteSetup : PX.Data.IBqlTable
	{
        public const string RouteManagementFieldClass = "ROUTEMANAGEMENT";

        #region RouteNumberingID
        public abstract class routeNumberingID : PX.Data.IBqlField
		{
		}

		[PXDBString(10)]
		[PXDefault]
        [PXSelector(typeof(Numbering.numberingID), DescriptionField = typeof(Numbering.descr))]
        [PXUIField(DisplayName = "Route Numbering Sequence")]
        public virtual string RouteNumberingID { get; set; }
		#endregion
        #region AutoCalculateRouteStats
        public abstract class autoCalculateRouteStats : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Calculate Route Statistics Automatically")]
        public virtual bool? AutoCalculateRouteStats { get; set; }
        #endregion 
        #region DfltSrvOrdType
        public abstract class dfltSrvOrdType : PX.Data.IBqlField
        {
        }

        [PXDBString(4, IsUnicode = true)]
        [PXUIField(DisplayName = "Default Service Order Type")]
        [FSSelectorSrvOrdTypeRoute]
        public virtual string DfltSrvOrdType { get; set; }
        #endregion        
        #region GroupINDocumentsByPostingProcess
        public abstract class groupINDocumentsByPostingProcess : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Group Inventory Documents by Posting Process")]
        public virtual bool? GroupINDocumentsByPostingProcess { get; set; }
        #endregion
        #region TrackRouteLocation
        public abstract class trackRouteLocation : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Track Start and Complete Location of Route")]
        public virtual bool? TrackRouteLocation { get; set; }
        #endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByID]
        [PXUIField(DisplayName = "CreatedByID")]
        public virtual Guid? CreatedByID { get; set; }

        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "CreatedByScreenID")]
        public virtual string CreatedByScreenID { get; set; }

        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }
        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "CreatedDateTime")]
        public virtual DateTime? CreatedDateTime { get; set; }

        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByID]
        [PXUIField(DisplayName = "LastModifiedByID")]
        public virtual Guid? LastModifiedByID { get; set; }

        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "LastModifiedByScreenID")]
        public virtual string LastModifiedByScreenID { get; set; }

        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }
        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "LastModifiedDateTime")]
        public virtual DateTime? LastModifiedDateTime { get; set; }

        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }
        [PXDBTimestamp]
        [PXUIField(DisplayName = "tstamp")]
        public virtual byte[] tstamp { get; set; }

        #endregion

        #region SetFirstManualAppointment
        public abstract class setFirstManualAppointment : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Set Appointments Created Manually as First in Route")]
        public virtual bool? SetFirstManualAppointment { get; set; }
        #endregion 
        #region EnableSeasonScheduleContract
        public abstract class enableSeasonScheduleContract : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Enable Seasons in Schedule Contracts")]
        public virtual bool? EnableSeasonScheduleContract { get; set; }
        #endregion
	}
}