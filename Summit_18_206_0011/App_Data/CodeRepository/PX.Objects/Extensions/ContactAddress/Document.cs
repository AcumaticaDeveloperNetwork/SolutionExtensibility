using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.CR;

namespace PX.Objects.Extensions.ContactAddress
{
	public partial class Document : PXMappedCacheExtension
	{
		#region ContactID
		public abstract class contactID : IBqlField { }
		public virtual Int32? ContactID { get; set; }
		#endregion

		#region DocumentContactID
		public abstract class documentContactID : IBqlField { }
		public virtual Int32? DocumentContactID { get; set; }
		#endregion

		#region DocumentAddressID
		public abstract class documentAddressID : IBqlField { }
		public virtual Int32? DocumentAddressID { get; set; }
		#endregion

		#region LocationID
		public abstract class locationID : IBqlField { }
		public virtual Int32? LocationID { get; set; }
		#endregion

		#region BAccountID
		public abstract class bAccountID : IBqlField { }
		public virtual Int32? BAccountID { get; set; }
		#endregion

		#region AllowOverrideContactAddress
		public abstract class allowOverrideContactAddress : IBqlField { }
		public virtual bool? AllowOverrideContactAddress { get; set; }
		#endregion
	}
}
