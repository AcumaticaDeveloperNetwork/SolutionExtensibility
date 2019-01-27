using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USSimpleInvoiceStatus
    {
        public const string Pending = "P";
        public const string Created = "C";

        public class pending : Constant<string>
        {
            public pending() : base(Pending)
            {
            }
        }

        public class created : Constant<string>
        {
            public created() : base(Created)
            {
            }
        }

        public class ListAttribute : PXStringListAttribute
        {
            public ListAttribute() : base(new string[] { Pending, Created }, new string[] { "Pending", "Created" })
            {
            }
        }
    }
}
