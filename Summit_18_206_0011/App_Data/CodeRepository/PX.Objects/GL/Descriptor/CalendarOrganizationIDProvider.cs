using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods.TableDefinition;

namespace PX.Objects.GL.Descriptor
{
	public class CalendarOrganizationIDProvider : ICalendarOrganizationIDProvider
	{
		protected virtual SourcesSpecificationCollection SourcesSpecification { get; set; }

		public virtual Type UseMasterCalendarSourceType { get; set; }

		public virtual bool UseMasterOrganizationIDByDefault { get; set; }

		protected virtual SourceSpecificationItem PrimarySourceSpecification { get; set; }

        public CalendarOrganizationIDProvider(Type branchSourceType = null,
			Type branchSourceFormulaType = null,
			Type organizationSourceType = null, 
			Type useMasterCalendarSourceType = null,
			bool useMasterOrganizationIDByDefault = false,
		    Type[] sourceSpecificationTypes = null)
		{
			SourcesSpecification = new SourcesSpecificationCollection();

            if (branchSourceType != null
		        || branchSourceFormulaType != null
		        || organizationSourceType != null)
            {
	            SourceSpecificationItem sourceSpecification = new SourceSpecificationItem()
                {
                    BranchSourceType = branchSourceType,
                    BranchSourceFormulaType = branchSourceFormulaType,
                    OrganizationSourceType = organizationSourceType,
					IsMain = true
                };

                sourceSpecification.Initialize();

	            SourcesSpecification.SpecificationItems.Add(sourceSpecification);
            }

			if (sourceSpecificationTypes != null)
			{
				foreach (var sourceSpecificationType in sourceSpecificationTypes)
				{
					SourceSpecificationItem sourceSpec = (SourceSpecificationItem)Activator.CreateInstance(sourceSpecificationType);

					sourceSpec.Initialize();

					SourcesSpecification.SpecificationItems.Add(sourceSpec);
				}
			}

			SourcesSpecification.PrimarySpecificationItem = SourcesSpecification.SpecificationItems.SingleOrDefault(s => s.IsMain);

			UseMasterCalendarSourceType = useMasterCalendarSourceType;

			UseMasterOrganizationIDByDefault = useMasterOrganizationIDByDefault;
		}

		public virtual SourcesSpecificationCollection GetSourcesSpecification(PXCache cache, object row)
		{
			return SourcesSpecification;
		}

		#region Calendar Value

		public virtual int? GetCalendarOrganizationID(PXGraph graph, PXCache attributeCache, object extRow)
		{
		    if (!GetSourcesSpecification(attributeCache, extRow).SpecificationItems.Any())
		        return FinPeriod.organizationID.MasterValue;

			SourceValuesCollectionItem mainSourceValuesItem =
				GetEvaluatedPrimaryOrganizationIDsValuesItem(graph, attributeCache, extRow);

			List<int?> organizationIDs = mainSourceValuesItem != null
				? mainSourceValuesItem.OrganizationIDs
				: GetEvaluatedOrganizationIDsValues(graph, attributeCache, extRow).OrganizationIDs;

            if (IsIDsUndefined(organizationIDs)
                && (UseMasterOrganizationIDByDefault
		            || (UseMasterCalendarSourceType != null && (bool?)BqlHelper.GetCurrentValue(graph, UseMasterCalendarSourceType, extRow) == true)))

            {
		        return FinPeriod.organizationID.MasterValue;
		    }

			return organizationIDs.FirstOrDefault();
		}
        
		public virtual int? GetCalendarOrganizationID(object[] pars,
		    bool takeBranchForSelectorFromQueryParams,
		    bool takeOrganizationForSelectorFromQueryParams)
		{
			int? organizationID = null;

			SourceValuesCollectionItem sourceValuesItem = EvaluateOrganizationIDsValuesItem(
				new SourceValuesCollectionItem()
				{
					BranchIDs = takeBranchForSelectorFromQueryParams
						? ((int?) pars[0]).SingleToList()
						: null,
					OrganizationIDs = takeOrganizationForSelectorFromQueryParams
						? ((int?) pars[takeBranchForSelectorFromQueryParams ? 1 : 0]).SingleToList()
						: null
				});

		    if (IsIDsUndefined(sourceValuesItem.OrganizationIDs) && UseMasterOrganizationIDByDefault)
		    {
		        organizationID = FinPeriod.organizationID.MasterValue;
		    }

		    return organizationID;
		}

		#endregion


		#region OrganizationID & BranchID Values

	    public virtual SourceValuesCollection BuildOrganizationIDsValuesCollection(
		    PXGraph graph, 
		    PXCache attributeCache, 
		    object extRow,
		    Func<PXGraph, PXCache, object, SourceSpecificationItem, SourceValuesCollectionItem> buildItemDelegate)
	    {
		    List<SourceValuesCollectionItem> items =
			    GetSourcesSpecification(attributeCache, extRow).SpecificationItems
				    .Select(specification => buildItemDelegate(graph, attributeCache, extRow, specification))
				    .ToList();

		    return new SourceValuesCollection()
		    {
			    Items = items,
			    OrganizationIDs = items.SelectMany(item => item.OrganizationIDs).ToList()
		    };
	    }

		public virtual SourceValuesCollection GetEvaluatedOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow)
		{
			return BuildOrganizationIDsValuesCollection(graph, attributeCache, extRow, EvaluateOrganizationIDsValuesItem);
		}

		public virtual SourceValuesCollection GetBasisOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow)
        {
	        SourceValuesCollection sourceValues = GetEvaluatedOrganizationIDsValues(graph, attributeCache, extRow);

            var availableOrganizationIDs = PXAccess.GetAvailableOrganizationIDs();

	        if ((sourceValues.OrganizationIDs == null
	             || sourceValues.OrganizationIDs.All(id => id == null)
	             || !sourceValues.OrganizationIDs.Any())
				&& GetCalendarOrganizationID(graph, attributeCache, extRow) == FinPeriod.organizationID.MasterValue)
	        {
		        sourceValues.OrganizationIDs = availableOrganizationIDs.ToList();
	        }
	        else
            {
	            sourceValues.OrganizationIDs = sourceValues.OrganizationIDs.Intersect(availableOrganizationIDs).ToList();
            }

            return sourceValues;
        }

	    public virtual SourceValuesCollection GetOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow)
	    {
		    return BuildOrganizationIDsValuesCollection(graph, attributeCache, extRow, GetOrganizationIDsValueFromField);
		}

		#endregion


		#region Main Source Field

		public virtual SourceValuesCollectionItem GetEvaluatedPrimaryOrganizationIDsValuesItem(PXGraph graph, PXCache attributeCache, object extRow)
        {
            return EvaluateOrganizationIDsValuesItem(graph, attributeCache, extRow, PrimarySourceSpecification);
        }

        public virtual SourceValuesCollectionItem GetPrimaryOrganizationIDsValuesItem(PXGraph graph, PXCache attributeCache, object extRow)
        {
            return GetOrganizationIDsValueFromField(graph, attributeCache, extRow, PrimarySourceSpecification);
        }

		#endregion


		#region OrganizationID Values Service

		protected virtual SourceValuesCollectionItem EvaluateOrganizationIDsValuesItem(PXGraph graph, PXCache attributeCache, object extRow, SourceSpecificationItem specificationItem)
		{
			if (specificationItem == null)
				return null;

			return EvaluateOrganizationIDsValuesItem(new SourceValuesCollectionItem()
			{
				OrganizationIDs = GetOrganizationIDsValueFromField(graph, attributeCache, extRow, specificationItem).OrganizationIDs,
				BranchIDs = GetBranchIDsValueFromField(graph, attributeCache, extRow, specificationItem).BranchIDs,
				SpecificationItem = specificationItem
			});
		}

        protected virtual SourceValuesCollectionItem EvaluateOrganizationIDsValuesItem(SourceValuesCollectionItem calendarOrganizationIdSourceValuesItem)
		{
			if (calendarOrganizationIdSourceValuesItem == null)
				return null;

			if (IsIDsUndefined(calendarOrganizationIdSourceValuesItem.OrganizationIDs))
			{
			    if (calendarOrganizationIdSourceValuesItem.BranchIDs != null)
			    {
				    calendarOrganizationIdSourceValuesItem.OrganizationIDs =
					    calendarOrganizationIdSourceValuesItem.BranchIDs
						    .Select(branchID => PXAccess.GetParentOrganizationID(branchID))
						    .ToList();
			    }
			}

			return calendarOrganizationIdSourceValuesItem;
		}

		#endregion


		#region Values Extraction

		public virtual SourceValuesCollectionItem GetOrganizationIDsValueFromField(
		    PXGraph graph, 
		    PXCache attributeCache, 
		    object extRow,
		    SourceSpecificationItem sourceSpecification)
		{
			int? organizationID = null;

			if (sourceSpecification.OrganizationSourceType != null)
			{
				PXCache cache = GetSourceCache(graph, attributeCache, sourceSpecification.OrganizationSourceType);

				object row = GetSourceRow(cache, extRow);

				organizationID = (int?)BqlHelper.GetOperandValue(cache, row, sourceSpecification.OrganizationSourceType);
			}

			return new SourceValuesCollectionItem()
			{
				SpecificationItem = sourceSpecification,
				OrganizationIDs = organizationID.SingleToList()
			};
		}

        public virtual SourceValuesCollectionItem GetBranchIDsValueFromField(
		    PXGraph graph, 
		    PXCache attributeCache,
		    object extRow,
		    SourceSpecificationItem calendarOrganizationIdSourceSpec)
		{
			bool? result = null;
			object branchID = null;

			if (calendarOrganizationIdSourceSpec.BranchSourceType != null || calendarOrganizationIdSourceSpec.BranchSourceFormula != null)
			{
				PXCache cache = GetSourceCache(graph, attributeCache, calendarOrganizationIdSourceSpec.BranchSourceType);

				object row = GetSourceRow(cache, extRow);

				if (calendarOrganizationIdSourceSpec.BranchSourceFormula != null)
				{
					BqlFormula.Verify(cache, row, calendarOrganizationIdSourceSpec.BranchSourceFormula, ref result, ref branchID);
				}
				else
				{
					branchID = BqlHelper.GetOperandValue(cache, row, calendarOrganizationIdSourceSpec.BranchSourceType);
				}
			}

			return new SourceValuesCollectionItem()
			{
				SpecificationItem = calendarOrganizationIdSourceSpec,
				BranchIDs = ((int?)branchID).SingleToList()
			};
		}

		public virtual List<SourceValuesCollectionItem> GetBranchIDsValuesFromField(
			PXGraph graph,
			PXCache attributeCache,
			object extRow)
		{
			return GetSourcesSpecification(attributeCache, extRow).SpecificationItems
						.Select(specification => GetBranchIDsValueFromField(graph, attributeCache, extRow, specification))
						.ToList();
		}

		#endregion


		#region PXCache & CurrentRow Service

		protected virtual PXCache GetSourceCache(PXGraph graph, PXCache attributeCache, Type sourceType)
		{
			if (typeof(IBqlField).IsAssignableFrom(sourceType) && !BqlCommand.GetItemType(sourceType).IsAssignableFrom(attributeCache.GetItemType()))
			{
				return graph.Caches[BqlCommand.GetItemType(sourceType)];
			}

			return attributeCache;
		}

		protected virtual object GetSourceRow(PXCache sourceCache, object extRow)
		{
			if (extRow == null || sourceCache.GetItemType() != extRow.GetType())
			{
				return sourceCache.Current;
			}

			return extRow;
		}

		#endregion


	    protected bool IsIDsUndefined(List<int?> values)
	    {
		    return values == null
		           || values.All(id => id == null)
		           || !values.Any();
	    }

		#region Types

		public class SourcesSpecificationCollection
		{
			public List<SourceSpecificationItem> SpecificationItems { get; set; }

			public List<Type> DependsOnFields { get; set; }

			public SourceSpecificationItem PrimarySpecificationItem { get; set; }

			public SourcesSpecificationCollection()
			{
				SpecificationItems = new List<SourceSpecificationItem>();
				DependsOnFields = new List<Type>();
			}
		}

		public class SourceSpecificationItem
		{
			public virtual Type BranchSourceType { get; set; }

			public virtual Type BranchSourceFormulaType { get; set; }

			public virtual IBqlCreator BranchSourceFormula { get; protected set; }

			public virtual Type OrganizationSourceType { get; set; }

			public virtual bool IsMain { get; set; }

			public virtual SourceSpecificationItem Initialize()
			{
				if (BranchSourceFormulaType != null && BranchSourceFormula == null)
				{
					BranchSourceFormula = PXFormulaAttribute.InitFormula(BranchSourceFormulaType);
				}

				return this;
			}
		}

		public class SourceValuesCollectionItem
		{
			public SourceSpecificationItem SpecificationItem { get; set; }

			public virtual List<int?> OrganizationIDs { get; set; }

			public virtual List<int?> BranchIDs { get; set; }
		}

		public class SourceValuesCollection
		{
			public List<SourceValuesCollectionItem> Items { get; set; }

			public List<int?> OrganizationIDs { get; set; }

			public SourceValuesCollection()
			{
				Items = new List<SourceValuesCollectionItem>();
			}
		}

		public class SourceSpecification<TBranchSource, TIsMain> :
			SourceSpecification<TBranchSource, BqlNone, BqlHelper.fieldStub, TIsMain>
			where TBranchSource : IBqlField
			where TIsMain : BoolConstant
		{
		}

		public class SourceSpecification<TBranchSource, TBranchFormula, TIsMain> :
			SourceSpecification<TBranchSource, TBranchFormula, BqlHelper.fieldStub, TIsMain>
			where TBranchSource : IBqlField
			where TBranchFormula : IBqlCreator
			where TIsMain : BoolConstant
		{
		}

		public class SourceSpecification<TBranchSource, TBranchFormula, TOrganizationSource, TIsMain> : SourceSpecificationItem
			where TBranchSource : IBqlField
			where TBranchFormula : IBqlCreator
			where TOrganizationSource : IBqlField
			where TIsMain : BoolConstant
		{
			public override Type BranchSourceType => BqlHelper.GetTypeNotStub<TBranchSource>();

			public override Type BranchSourceFormulaType => BqlHelper.GetTypeNotStub<TBranchFormula>();

			public override Type OrganizationSourceType => BqlHelper.GetTypeNotStub<TOrganizationSource>();

			public override SourceSpecificationItem Initialize()
			{
				base.Initialize();

				BoolConstant isMainConst = Activator.CreateInstance<TIsMain>();
				IsMain = (bool) isMainConst.Value;

				return this;
			}
		}

		#endregion

	}
}
