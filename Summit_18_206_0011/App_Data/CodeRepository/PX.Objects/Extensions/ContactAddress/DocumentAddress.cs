using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace PX.Objects.Extensions.ContactAddress
{
	public partial class DocumentAddress : PXMappedCacheExtension
	{
		#region OverrideAddress
		public abstract class overrideAddress : IBqlField { }
		public virtual Boolean? OverrideAddress { get; set; }
		#endregion

		#region AddressLine1
		public abstract class addressLine1 : IBqlField { }
		public virtual String AddressLine1 { get; set; }
		#endregion

		#region AddressLine2
		public abstract class addressLine2 : IBqlField { }
		public virtual String AddressLine2 { get; set; }
		#endregion

		#region AddressLine3
		public abstract class addressLine3 : IBqlField { }
		public virtual String AddressLine3 { get; set; }
		#endregion

		#region City
		public abstract class city : IBqlField { }
		public virtual String City { get; set; }
		#endregion

		#region CountryID
		public abstract class countryID : IBqlField { }
		public virtual String CountryID { get; set; }
		#endregion

		#region State
		public abstract class state : IBqlField { }
		public virtual String State { get; set; }
		#endregion

		#region PostalCode
		public abstract class postalCode : IBqlField { }
		public virtual String PostalCode { get; set; }
		#endregion
	}
}
