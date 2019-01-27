using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
	public class FSPostDoc : PX.Data.IBqlTable
	{
        #region ProcessID
        public abstract class processID : IBqlField
        {
        }

		[PXDBGuid]
        public virtual Guid? ProcessID { get; set; }
        #endregion
        #region RecordID
        public abstract class recordID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? RecordID { get; set; }
        #endregion

        #region BillingCycleID
        public abstract class billingCycleID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? BillingCycleID { get; set; }
        #endregion
        #region GroupKey
        public abstract class groupKey : PX.Data.IBqlField
        {
        }

        [PXDBString]
        public virtual string GroupKey { get; set; }
        #endregion

        #region AppointmentID
        public abstract class appointmentID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Appointment Nbr.")]
        [PXSelector(typeof(Search<FSAppointment.appointmentID>), SubstituteKey = typeof(FSAppointment.refNbr))]
        public virtual int? AppointmentID { get; set; }
        #endregion
        #region SOID
        public abstract class sOID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Service Order Nbr.")]
        [PXSelector(typeof(Search<FSServiceOrder.sOID>), SubstituteKey = typeof(FSServiceOrder.refNbr))]
        public virtual int? SOID { get; set; }
        #endregion

        #region RowIndex
        public abstract class rowIndex : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? RowIndex { get; set; }
        #endregion
        
        #region PostNegBalanceToAP
        public abstract class postNegBalanceToAP : PX.Data.IBqlField
        {
        }

        [PXDBBool]
        public virtual bool? PostNegBalanceToAP { get; set; }
        #endregion
        #region PostOrderType
        public abstract class postOrderType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        public virtual string PostOrderType { get; set; }
        #endregion
        #region PostOrderTypeNegativeBalance
        public abstract class postOrderTypeNegativeBalance : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        public virtual string PostOrderTypeNegativeBalance { get; set; }
        #endregion

        #region InvtMult
        public abstract class invtMult : PX.Data.IBqlField
        {
        }

        [PXShort]
        public virtual short? InvtMult { get; set; }
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

        #region BatchID
        public abstract class batchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? BatchID { get; set; }
        #endregion
        #region EntityType
        public abstract class entityType : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        public virtual string EntityType { get; set; }
        #endregion
        #region PostedTO
        public abstract class postedTO : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXUIField(DisplayName = "Posted to")]
        public virtual string PostedTO { get; set; }
        #endregion
        #region PostDocType
        public abstract class postDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
        [PXUIField(DisplayName = "Document Type")]
        public virtual string PostDocType { get; set; }
        #endregion
        #region PostRefNbr
        public abstract class postRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Document Nbr.")]
        public virtual string PostRefNbr { get; set; }
        #endregion

        public virtual object DocLineRef { get; set; }
	}
}
