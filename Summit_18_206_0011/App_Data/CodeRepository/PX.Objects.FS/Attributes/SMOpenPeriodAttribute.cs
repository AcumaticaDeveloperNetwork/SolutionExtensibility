using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

namespace PX.Objects.FS
{
    public class SMOpenPeriodAttribute : OpenPeriodAttribute
    {
        private Type _tableSourceType;
        private Type _DependenceFieldType;

        public SMOpenPeriodAttribute(Type sourceType, Type dependenceFieldType,
                Type branchSourceType = null,
                Type branchSourceFormulaType = null,
                Type organizationSourceType = null,
                Type useMasterCalendarSourceType = null,
                Type defaultType = null,
                bool redefaultOrRevalidateOnOrganizationSourceUpdated = true,
                bool checkFinPeriodExistenceForDate = true,
                bool useMasterOrganizationIDByDefault = true)
                : base(typeof(Search<FinPeriod.finPeriodID>),
                            sourceType,
                            branchSourceType: branchSourceType,
                            branchSourceFormulaType: branchSourceFormulaType,
                            organizationSourceType: organizationSourceType,
                            useMasterCalendarSourceType: useMasterCalendarSourceType,
                            defaultType: defaultType,
                            redefaultOrRevalidateOnOrganizationSourceUpdated: redefaultOrRevalidateOnOrganizationSourceUpdated,
                            checkFinPeriodExistenceForDate: checkFinPeriodExistenceForDate,
                            useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault)
        {
            _tableSourceType = BqlCommand.GetItemType(sourceType);
            _DependenceFieldType = dependenceFieldType;
        }

        public override void CacheAttached(PXCache sender)
        {
            base.CacheAttached(sender);
            if (_tableSourceType != null)
            {
               sender.Graph.FieldUpdated.AddHandler(_tableSourceType, _DependenceFieldType.Name, SourceFieldUpdated);
            }
        }

        protected void SourceFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
        {
            string postTo = (string)cache.GetValue(e.Row, _DependenceFieldType.Name);
            CheckFinPeriod(cache, e.Row, postTo, true);
        }

        public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
        {
            string postTo = (string)sender.GetValue(e.Row, _DependenceFieldType.Name);
            CheckFinPeriod(sender, e.Row, postTo);
            base.FieldVerifying(sender, e);
        }

        private void CheckFinPeriod(PXCache sender, object row, string postTo, bool isAltered = false)
        {
            if (postTo != null)
            {
                if (postTo == ID.Batch_PostTo.SO)
                {
                    OpenPeriodAttribute.SetValidatePeriod(sender, isAltered ? null : row, _FieldName, GL.PeriodValidation.Nothing);
                }
                else
                {
                    OpenPeriodAttribute.SetValidatePeriod(sender, isAltered ? null : row, _FieldName, GL.PeriodValidation.DefaultSelectUpdate);
                }
            }
        }
    }
}
