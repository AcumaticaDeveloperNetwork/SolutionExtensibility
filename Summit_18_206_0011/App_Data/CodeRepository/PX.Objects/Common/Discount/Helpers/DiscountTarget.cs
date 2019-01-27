using PX.Data;

namespace PX.Objects.Common.Discount
{
	/// <summary>
	/// Discount Targets
	/// </summary>
	public static class DiscountTarget
	{
		public const string Customer = "CU";
		public const string CustomerAndInventory = "CI";
		public const string CustomerAndInventoryPrice = "CP";
		public const string CustomerPrice = "CE";
		public const string CustomerPriceAndInventory = "PI";
		public const string CustomerPriceAndInventoryPrice = "PP";
		public const string CustomerAndBranch = "CB";
		public const string CustomerPriceAndBranch = "PB";

		public const string Warehouse = "WH";
		public const string WarehouseAndInventory = "WI";
		public const string WarehouseAndCustomer = "WC";
		public const string WarehouseAndInventoryPrice = "WP";
		public const string WarehouseAndCustomerPrice = "WE";

		public const string Branch = "BR";

		public const string Vendor = "VE";
		public const string VendorAndInventory = "VI";
		public const string VendorAndInventoryPrice = "VP";
		public const string VendorLocation = "VL";
		public const string VendorLocationAndInventory = "LI";

		public const string Inventory = "IN";
		public const string InventoryPrice = "IE";

		public const string Unconditional = "UN";

		public class customer : Constant<string> { public customer() : base(Customer) { } }
		public class customerAndInventory : Constant<string> { public customerAndInventory() : base(CustomerAndInventory) { } }
		public class customerAndInventoryPrice : Constant<string> { public customerAndInventoryPrice() : base(CustomerAndInventoryPrice) { } }
		public class customerPrice : Constant<string> { public customerPrice() : base(CustomerPrice) { } }
		public class customerPriceAndInventory : Constant<string> { public customerPriceAndInventory() : base(CustomerPriceAndInventory) { } }
		public class customerPriceAndInventoryPrice : Constant<string> { public customerPriceAndInventoryPrice() : base(CustomerPriceAndInventoryPrice) { } }
		public class customerAndBranch : Constant<string> { public customerAndBranch() : base(CustomerAndBranch) { } }
		public class customerPriceAndBranch : Constant<string> { public customerPriceAndBranch() : base(CustomerPriceAndBranch) { } }

		public class warehouse : Constant<string> { public warehouse() : base(Warehouse) { } }
		public class warehouseAndInventory : Constant<string> { public warehouseAndInventory() : base(WarehouseAndInventory) { } }
		public class warehouseAndCustomer : Constant<string> { public warehouseAndCustomer() : base(WarehouseAndCustomer) { } }
		public class warehouseAndInventoryPrice : Constant<string> { public warehouseAndInventoryPrice() : base(WarehouseAndInventoryPrice) { } }
		public class warehouseAndCustomerPrice : Constant<string> { public warehouseAndCustomerPrice() : base(WarehouseAndCustomerPrice) { } }

		public class branch : Constant<string> { public branch() : base(Branch) { } }

		public class vendor : Constant<string> { public vendor() : base(Vendor) { } }
		public class vendorAndInventory : Constant<string> { public vendorAndInventory() : base(VendorAndInventory) { } }
		public class vendorAndInventoryPrice : Constant<string> { public vendorAndInventoryPrice() : base(VendorAndInventoryPrice) { } }
		public class vendorLocation : Constant<string> { public vendorLocation() : base(VendorLocation) { } }
		public class vendorLocationAndInventory : Constant<string> { public vendorLocationAndInventory() : base(VendorLocationAndInventory) { } }

		public class inventory : Constant<string> { public inventory() : base(Inventory) { } }
		public class inventoryPrice : Constant<string> { public inventoryPrice() : base(InventoryPrice) { } }

		public class unconditional : Constant<string> { public unconditional() : base(Unconditional) { } }
	}
}