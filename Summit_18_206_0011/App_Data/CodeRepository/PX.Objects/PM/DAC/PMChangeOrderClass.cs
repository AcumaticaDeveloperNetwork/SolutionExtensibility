using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;

namespace PX.Objects.PM
{
	[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
	[PXCacheName(Messages.ChangeOrderClass)]
	[PXPrimaryGraph(typeof(ChangeOrderClassMaint))]
	[Serializable]
	public class PMChangeOrderClass : PX.Data.IBqlTable
	{
		#region ClassID
		public abstract class classID : PX.Data.IBqlField
		{
			public const int Length = 15;
		}

		[PXReferentialIntegrityCheck]
		[PXDBString(classID.Length, IsUnicode = true, IsKey = true, InputMask = "")]
		[PXDefault()]
		[PXUIField(DisplayName = "Class ID", Visibility = PXUIVisibility.SelectorVisible)]
		[PXSelector(typeof(PMChangeOrderClass.classID), DescriptionField = typeof(PMChangeOrderClass.description))]
		public virtual String ClassID
		{
			get;
			set;
		}
		#endregion
				
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		
		[PXDBString(256, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
		[PXFieldDescription]
		public virtual String Description
		{
			get;
			set;
		}
		#endregion
		#region IsCostBudgetEnabled
		public abstract class isCostBudgetEnabled : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Cost Budget")]
		public virtual bool? IsCostBudgetEnabled
		{
			get;
			set;
		}
		#endregion
		#region IsRevenueBudgetEnabled
		public abstract class isRevenueBudgetEnabled : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Revenue Budget")]
		public virtual bool? IsRevenueBudgetEnabled
		{
			get;
			set;
		}
		#endregion
		#region IsPurchaseOrderEnabled
		public abstract class isPurchaseOrderEnabled : IBqlField
		{
		}

		[PXDBBool]
		[PXDefault(true)]
		[PXUIField(DisplayName ="Commitments")]
		public virtual bool? IsPurchaseOrderEnabled
		{
			get;
			set;
		}
		#endregion
		#region IsActive
		public abstract class isActive : IBqlField
		{
		}
		[PXUIField(DisplayName ="Active")]
		[PXDBBool]
		[PXDefault(true)]
		public virtual bool? IsActive
		{
			get;
			set;
		}
		#endregion

		#region IncrementsProjectNumber
		public abstract class incrementsProjectNumber : IBqlField
		{
		}
		[PXBool]
		public virtual bool? IncrementsProjectNumber
		{
			get { return IsRevenueBudgetEnabled == true;  }
		}
		#endregion

		#region System Columns
		#region NoteID
		public abstract class noteID : PX.Data.IBqlField
		{
		}
		protected Guid? _NoteID;
		[PXNote(DescriptionField = typeof(PMChangeOrderClass.description))]
		public virtual Guid? NoteID
		{
			get
			{
				return this._NoteID;
			}
			set
			{
				this._NoteID = value;
			}
		}
		#endregion
		#region tstamp
		public abstract class Tstamp : PX.Data.IBqlField
		{
		}
		protected Byte[] _tstamp;
		[PXDBTimestamp()]
		public virtual Byte[] tstamp
		{
			get
			{
				return this._tstamp;
			}
			set
			{
				this._tstamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected String _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual String CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.CreatedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBCreatedDateTime]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected String _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual String LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXUIField(DisplayName = PXDBLastModifiedByIDAttribute.DisplayFieldNames.LastModifiedDateTime, Enabled = false, IsReadOnly = true)]
		[PXDBLastModifiedDateTime]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
		#endregion

	}
}
