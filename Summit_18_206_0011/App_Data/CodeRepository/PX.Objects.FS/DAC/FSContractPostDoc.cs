using PX.Data;
using System;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSContractPostDoc : PX.Data.IBqlTable
    {
        #region ContractPostDocID
        public abstract class contractPostDocID : PX.Data.IBqlField
        {
        }

        [PXDBIdentity(IsKey = true)]
        public virtual int? ContractPostDocID { get; set; }
        #endregion
        #region ContractPeriodID
        public abstract class contractPeriodID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Contract Period Nbr.")]
        public virtual int? ContractPeriodID { get; set; }
        #endregion
        #region ContractPostBatchID
        public abstract class contractPostBatchID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        public virtual int? ContractPostBatchID { get; set; }
        #endregion
        #region PostDocType
        public abstract class postDocType : PX.Data.IBqlField
        {
        }

        [PXDBString(3, IsFixed = true, InputMask = ">aaa")]
        [PXUIField(DisplayName = "Document Type")]
        public virtual string PostDocType { get; set; }
        #endregion
        #region PostedTO
        public abstract class postedTO : PX.Data.IBqlField
        {
        }

        [PXDBString(2, IsFixed = true, InputMask = ">aa")]
        [PXUIField(DisplayName = "Posted to")]
        public virtual string PostedTO { get; set; }
        #endregion
        #region PostRefNbr
        public abstract class postRefNbr : PX.Data.IBqlField
        {
        }

        [PXDBString(15, IsUnicode = true)]
        [PXUIField(DisplayName = "Document Nbr.")]
        public virtual string PostRefNbr { get; set; }
        #endregion
        #region ServiceContractID
        public abstract class serviceContractID : PX.Data.IBqlField
        {
        }

        [PXDBInt]
        [PXUIField(DisplayName = "Service Contract Nbr.")]
        public virtual int? ServiceContractID { get; set; }
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
    }
}