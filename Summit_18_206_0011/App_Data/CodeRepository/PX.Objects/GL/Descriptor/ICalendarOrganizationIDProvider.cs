using System;
using System.Collections.Generic;
using PX.Data;

namespace PX.Objects.GL.Descriptor
{
	public interface ICalendarOrganizationIDProvider
	{
		CalendarOrganizationIDProvider.SourcesSpecificationCollection GetSourcesSpecification(PXCache cache, object row);
		Type UseMasterCalendarSourceType { get; set; }
		bool UseMasterOrganizationIDByDefault { get; set; }
		int? GetCalendarOrganizationID(PXGraph graph, PXCache attributeCache, object extRow);

		int? GetCalendarOrganizationID(object[] pars,
			bool takeBranchForSelectorFromQueryParams,
			bool takeOrganizationForSelectorFromQueryParams);

		CalendarOrganizationIDProvider.SourceValuesCollection BuildOrganizationIDsValuesCollection(
			PXGraph graph, 
			PXCache attributeCache, 
			object extRow,
			Func<PXGraph, PXCache, object, CalendarOrganizationIDProvider.SourceSpecificationItem, CalendarOrganizationIDProvider.SourceValuesCollectionItem> buildItemDelegate);

		CalendarOrganizationIDProvider.SourceValuesCollection GetEvaluatedOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow);
		CalendarOrganizationIDProvider.SourceValuesCollection GetBasisOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow);
		CalendarOrganizationIDProvider.SourceValuesCollection GetOrganizationIDsValues(PXGraph graph, PXCache attributeCache, object extRow);
		CalendarOrganizationIDProvider.SourceValuesCollectionItem GetEvaluatedPrimaryOrganizationIDsValuesItem(PXGraph graph, PXCache attributeCache, object extRow);
		CalendarOrganizationIDProvider.SourceValuesCollectionItem GetPrimaryOrganizationIDsValuesItem(PXGraph graph, PXCache attributeCache, object extRow);
		CalendarOrganizationIDProvider.SourceValuesCollectionItem GetOrganizationIDsValueFromField(
			PXGraph graph,
			PXCache attributeCache,
			object extRow,
			CalendarOrganizationIDProvider.SourceSpecificationItem sourceSpecification);

		CalendarOrganizationIDProvider.SourceValuesCollectionItem GetBranchIDsValueFromField(
			PXGraph graph,
			PXCache attributeCache,
			object extRow,
			CalendarOrganizationIDProvider.SourceSpecificationItem sourceSpecification);

		List<CalendarOrganizationIDProvider.SourceValuesCollectionItem> GetBranchIDsValuesFromField(
			PXGraph graph,
			PXCache attributeCache,
			object extRow);
	}
}