using PX.Data;
using PX.Objects.CR;
using PX.Objects.GL;

namespace PX.Objects.CS
{
	public class BuildingMaint : PXGraph<BuildingMaint, Branch>
	{
		#region Selects
		public PXSetup<Company> company;
		public PXSelect<Branch> filter;
		public PXSelect<Building, Where<Building.branchID, Equal<Current<Branch.branchID>>>> building;
		#endregion

		public BuildingMaint()
		{
			filter.Cache.AllowDelete = false;
            filter.Cache.AllowInsert = false;
        }

        public virtual void Branch_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
        {
            e.Cancel = true;
        }
	}
}