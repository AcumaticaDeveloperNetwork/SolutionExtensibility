using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public class USScoutMaint : PXGraph<USScoutMaint, USScout>
    {
        public PXSelect<USScout> Scout;

        public PXSelect<USScout, Where<USScout.scoutID, Equal<Current<USScout.scoutID>>>> CurrentScout;
    }
}
