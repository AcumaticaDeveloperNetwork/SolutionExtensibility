using System;

using PX.Data;
using PX.Objects.CS;
using PX.Objects.GL.DAC;

namespace PX.Objects.GL.Attributes
{
	public class BranchOfOrganizationAttribute : BranchBaseAttribute
	{
		public readonly Type OrganizationFieldType;

		public BranchOfOrganizationAttribute(Type organizationFieldType, bool onlyActive = true, Type sourceType = null) :
										this(organizationFieldType, onlyActive, addDefaultAttribute: true, sourceType: sourceType)
		{
		}

		public BranchOfOrganizationAttribute(Type organizationFieldType, bool onlyActive, bool addDefaultAttribute, Type sourceType = null) :
										base(sourceType ?? typeof(AccessInfo.branchID), addDefaultAttribute: addDefaultAttribute)
		{
			OrganizationFieldType = organizationFieldType;
			InitializeAttributeRestrictions(onlyActive, organizationFieldType);
		}

		public override void CacheAttached(PXCache sender)
		{
			base.CacheAttached(sender);

			if (OrganizationFieldType != null)
			{
				sender.Graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(OrganizationFieldType), OrganizationFieldType.Name, OrganizationFieldUpdated);
				sender.Graph.RowSelected.AddHandler(BqlCommand.GetItemType(OrganizationFieldType), OrganizationRowSelected);
			}
		}

		private void OrganizationFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
		{
			cache.SetValueExt(e.Row, _FieldName, null);
		}

		private void OrganizationRowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			bool enableBranchSelector = true;
			int? organizationID = (int?)sender.GetValue(e.Row, OrganizationFieldType.Name);
			Organization organization = OrganizationMaint.FindOrganizationByID(sender.Graph, organizationID);
			if (organization != null)
			{
				enableBranchSelector = organization.OrganizationType != OrganizationTypes.WithoutBranches;
			}

			PXUIFieldAttribute.SetEnabled(sender, _FieldName, enableBranchSelector);
		}

		private void InitializeAttributeRestrictions(bool onlyActive, Type organizationFieldType)
		{
			if (onlyActive)
			{
				_Attributes.Add(new PXRestrictorAttribute(typeof(Where<Branch.active, Equal<True>>), Messages.BranchInactive));
			}

			_Attributes.Add(new PXRestrictorAttribute(BqlCommand.Compose(
															typeof(Where<,,>),
															typeof(Branch.organizationID), typeof(Equal<>), typeof(Optional2<>), organizationFieldType,
															typeof(Or<,>), typeof(Optional2<>), organizationFieldType, typeof(IsNull)),
														Messages.TheSpecifiedBranchDoesNotBelongToTheSelectedCompany));
		}
	}
}