using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace PX.Objects.Extensions.ContactAddress
{
	public partial class DocumentContact : PXMappedCacheExtension
	{
		#region FullName
		public abstract class fullName : IBqlField { }
		public virtual String FullName { get; set; }
		#endregion

		#region Title
		public abstract class title : IBqlField { }
		public virtual String Title { get; set; }
		#endregion

		#region FirstName
		public abstract class firstName : IBqlField { }
		public virtual String FirstName { get; set; }
		#endregion

		#region LastName
		public abstract class lastName : IBqlField { }
		public virtual String LastName { get; set; }
		#endregion

		#region Salutation
		public abstract class salutation : IBqlField { }
		public virtual String Salutation { get; set; }
		#endregion

		#region Attention
		public abstract class attention : IBqlField { }
		public virtual String Attention { get; set; }
		#endregion

		#region Email
		public abstract class email : IBqlField { }
		public virtual String EMail { get; set; }
		#endregion

		#region Phone1
		public abstract class phone1 : IBqlField { }
		public virtual String Phone1 { get; set; }
		#endregion

		#region Phone1Type
		public abstract class phone1Type : IBqlField { }
		public virtual String Phone1Type { get; set; }
		#endregion

		#region Phone2
		public abstract class phone2 : IBqlField { }
		public virtual String Phone2 { get; set; }
		#endregion

		#region Phone2Type
		public abstract class phone2Type : IBqlField { }
		public virtual String Phone2Type { get; set; }
		#endregion

		#region Phone3
		public abstract class phone3 : IBqlField { }
		public virtual String Phone3 { get; set; }
		#endregion

		#region Phone3Type
		public abstract class phone3Type : IBqlField { }
		public virtual String Phone3Type { get; set; }
		#endregion

		#region Fax
		public abstract class fax : IBqlField { }
		public virtual String Fax { get; set; }
		#endregion

		#region FaxType
		public abstract class faxType : IBqlField { }
		public virtual String FaxType { get; set; }
		#endregion

		#region OverrideContact
		public abstract class overrideContact : IBqlField { }
		public virtual Boolean? OverrideContact { get; set; }
		#endregion
	}
}
