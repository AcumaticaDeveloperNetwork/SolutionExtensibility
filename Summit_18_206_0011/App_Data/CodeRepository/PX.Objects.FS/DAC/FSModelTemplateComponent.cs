using System;
using PX.Data;
using PX.Objects.IN;

namespace PX.Objects.FS
{
     [System.SerializableAttribute]
     [PXCacheName(TX.TableName.MODEL_TEMPLATE_COMPONENT)]
	public class FSModelTemplateComponent : PX.Data.IBqlTable
	{
		#region ModelTemplateID
		public abstract class modelTemplateID : PX.Data.IBqlField
		{
		}

		[PXDBInt(IsKey = true)]
		[PXDBLiteDefaultAttribute(typeof(INItemClass.itemClassID))]
        [PXParent(typeof(Select<INItemClass, Where<INItemClass.itemClassID, Equal<Current<FSModelTemplateComponent.modelTemplateID>>>>))]
        public virtual int? ModelTemplateID { get; set; }
		#endregion
        #region Active
        public abstract class active : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(true)]
        [PXUIField(DisplayName = "Active")]
        public virtual bool? Active { get; set; }
        #endregion
        #region ComponentID
        public abstract class componentID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity]
        public virtual int? ComponentID { get; set; }

        #endregion
		#region ComponentCD
		public abstract class componentCD : PX.Data.IBqlField
		{
		}

        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		[PXDefault]
		[PXUIField(DisplayName = "Component ID")]
        public virtual string ComponentCD { get; set; }
		#endregion
		#region Descr
		public abstract class descr : PX.Data.IBqlField
		{
		}

		[PXDBString(250, IsUnicode = true)]
		[PXUIField(DisplayName = "Description")]
        public virtual string Descr { get; set; }
        #endregion
        #region ClassID
        public abstract class classID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Item Class ID")]
        [PXDefault]
        [PXSelector(typeof(
            Search<INItemClass.itemClassID,
            Where<
                FSxEquipmentModelTemplate.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>),
            SubstituteKey = typeof(INItemClass.itemClassCD),
            DescriptionField = typeof(INItemClass.descr))]
        public virtual int? ClassID { get; set; }
        #endregion
        #region Qty
        public abstract class qty : PX.Data.IBqlField
        {
        }

        [PXDBInt(MinValue = 1)]
        [PXDefault(TypeCode.Int32, "1")]
        [PXUIField(DisplayName = "Quantity")]
        public virtual int? Qty { get; set; }
        #endregion
        #region Optional
        public abstract class optional : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Optional", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual bool? Optional { get; set; }
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
        public virtual string CreatedByScreenID { get; set; }
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}

		[PXDBCreatedDateTime]
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
        public virtual string LastModifiedByScreenID { get; set; }
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}

		[PXDBLastModifiedDateTime]
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