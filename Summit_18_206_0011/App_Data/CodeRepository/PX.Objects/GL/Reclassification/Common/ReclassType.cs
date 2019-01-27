using System;
using PX.Data;

namespace PX.Objects.GL
{
    public class ReclassType
    {
        public class List : PXStringListAttribute
        {
            public List()
                : base(
                new string[] { Common, Split },
                new string[] { Messages.CommonReclassType, Messages.Split })
            { }
        }

        public const string Common = "C";
        public const string Split = "S";
        
        
        public class common : Constant<string>
        {
            public common() : base(Common) { }
        }

        public class split : Constant<string>
        {
            public split() : base(Split) { }
        }
    }
}
