using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    [PXDBInt]
    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "US Scout")]
    public class USScoutTranAttribute : AcctSubAttribute
    {
        public USScoutTranAttribute() : base()
        {
            PXSelectorAttribute attr = new PXSelectorAttribute(typeof(Search<USScout.scoutID>));
            attr.SubstituteKey = typeof(USScout.scoutCD);
            attr.DescriptionField = typeof(USScout.description);
            _Attributes.Add(attr);
            _SelAttrIndex = _Attributes.Count - 1;
        }
    }
}
