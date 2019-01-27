using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.Common.DAC.ReportParameters;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.DAC;

namespace PX.Objects.Common.DAC
{
	public class QueryParameters: IBqlTable
	{
		#region OrganizationID
		public abstract class organizationID : IBqlField { }

		[Organization(false)]
		public int? OrganizationID { get; set; }
		#endregion

		#region BranchID
		public abstract class branchID : IBqlField { }

		[Branch(false)]
		public int? BranchID { get; set; }
		#endregion
	}
}
