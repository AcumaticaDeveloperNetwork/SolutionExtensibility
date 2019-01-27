using System;
using PX.Data;

namespace PX.Objects.FS
{
    [Serializable]
    public class FSActivationContractFilter : PX.Data.IBqlTable
    {
        #region ActivationDate
        public abstract class activationDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Activation Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? ActivationDate { get; set; }
        #endregion
    }

    [Serializable]
    public class FSTerminateContractFilter : PX.Data.IBqlTable
    {
        #region CancelationDate
        public abstract class cancelationDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Cancelation Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? CancelationDate { get; set; }
        #endregion
    }

    [Serializable]
    public class FSSuspendContractFilter : PX.Data.IBqlTable
    {
        #region SuspensionDate
        public abstract class suspensionDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXDefault(typeof(AccessInfo.businessDate))]
        [PXUIField(DisplayName = "Suspension Date", Visibility = PXUIVisibility.SelectorVisible)]
        public virtual DateTime? SuspensionDate { get; set; }
        #endregion
    }

    [Serializable]
    public class ActiveSchedule : FSSchedule
    {
        #region RefNbr
        [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Ref. Nbr.", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        [PXSelector(typeof(Search<FSSchedule.refNbr>))]
        public override string RefNbr { get; set; }
        #endregion   
        #region RecurrenceDescription
        [PXDBString(int.MaxValue, IsUnicode = true)]
        [PXUIField(DisplayName = "Recurrence Description", Enabled = false)]
        public override string RecurrenceDescription { get; set; }
        #endregion
        #region ChangeRecurrence
        public abstract class changeRecurrence : IBqlField
        {
        }

        [PXBool]
        [PXUIField(DisplayName = "Change Recurrence")]
        public virtual bool? ChangeRecurrence { get; set; }
        #endregion
        #region EffectiveRecurrenceStartDate
        public abstract class effectiveRecurrenceStartDate : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXUIField(DisplayName = "Effective Recurrence Start Date", Visibility = PXUIVisibility.SelectorVisible, Required = true)]
        [PXUIEnabled(typeof(Where<changeRecurrence, Equal<True>>))]
        public virtual DateTime? EffectiveRecurrenceStartDate { get; set; }
        #endregion
        #region NextExecution
        public abstract class nextExecution : PX.Data.IBqlField
        {
        }

        [PXDate]
        [PXUIField(DisplayName = "Next Execution", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual DateTime? NextExecution { get; set; }
        #endregion

        public ActiveSchedule()
        {
        }
    }
}
