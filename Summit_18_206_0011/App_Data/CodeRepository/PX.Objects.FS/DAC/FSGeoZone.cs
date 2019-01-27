using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    [PXCacheName(TX.TableName.GEOGRAPHIC_ZONE)]
    [PXPrimaryGraph(typeof(GeoZoneMaint))]
	public class FSGeoZone : PX.Data.IBqlTable
	{
		#region GeoZoneID
		public abstract class geoZoneID : PX.Data.IBqlField
		{
		}

		[PXDBIdentity]
		[PXUIField(DisplayName = "Service Area", Enabled = false)]
        public virtual int? GeoZoneID { get; set; }
		#endregion
		#region GeoZoneCD
		public abstract class geoZoneCD : PX.Data.IBqlField
		{
		}

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
		[PXUIField(DisplayName = "Service Area ID", Visibility = PXUIVisibility.SelectorVisible)]
        [PXSelector(typeof(FSGeoZone.geoZoneCD))]
        [PXDefault]
        [NormalizeWhiteSpace]
		public virtual string GeoZoneCD { get; set; }
        #endregion
        #region CountryID
        public abstract class countryID : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsUnicode = true)]
        [PXUIField(DisplayName = "Country")]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [Country]
        public virtual string CountryID { get; set; }
        #endregion
        #region Descr
        public abstract class descr : PX.Data.IBqlField
		{
		}

		[PXDBString(60, IsUnicode = true)]
        [PXDefault]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		public virtual string Descr { get; set; }
		#endregion
        #region CreatedByID
        public abstract class createdByID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
        #endregion
        #region CreatedByScreenID
        public abstract class createdByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBCreatedByScreenID]
        [PXUIField(DisplayName = "Created By Screen ID")]
        public virtual string CreatedByScreenID { get; set; }
        #endregion
        #region CreatedDateTime
        public abstract class createdDateTime : PX.Data.IBqlField
        {
        }

        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? CreatedDateTime { get; set; }
        #endregion
        #region LastModifiedByID
        public abstract class lastModifiedByID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
        #endregion
        #region LastModifiedByScreenID
        public abstract class lastModifiedByScreenID : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedByScreenID]
        [PXUIField(DisplayName = "Last Modified By Screen ID")]
        public virtual string LastModifiedByScreenID { get; set; }
        #endregion
        #region LastModifiedDateTime
        public abstract class lastModifiedDateTime : PX.Data.IBqlField
        {
        }

        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        #endregion
        #region tstamp
        public abstract class Tstamp : PX.Data.IBqlField
        {
        }

        [PXDBTimestamp]
        public virtual byte[] tstamp { get; set; }
        #endregion
	}
}
