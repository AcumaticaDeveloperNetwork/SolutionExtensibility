using PX.Data;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USScouts
{
    public abstract class USProvider : ProviderBase
    {

    }

    public class PXDatabaseUSProvider : USProvider
    {
        private class USFeatureDefinition : IPrefetchable<PXDatabaseUSProvider>
        {

            private bool _featureActive;

            public void Prefetch(PXDatabaseUSProvider provider)
            {
                PXDataRecord record = PXDatabase.SelectSingle<USSetup>(new PXDataField<USSetup.enableInAP>(), new PXDataField<USSetup.simpleNumberingID>());
                if (record == null) _featureActive = false;
                else
                {
                    if (record.GetBoolean(0) ?? false) _featureActive = true;
                    else _featureActive = false;
                }
            }

            public bool IsFeatureActive()
            {
                return _featureActive;
            }
        }

        private static USFeatureDefinition GetSlot()
        {
            return PXDatabase.GetSlot<USFeatureDefinition, PXDatabaseUSProvider>("USFEATUREACTIVE", new PXDatabaseUSProvider(), typeof(USSetup));
        }

        public static bool GetFeatureActive()
        {
            USFeatureDefinition def = GetSlot();
            if (def.IsFeatureActive()) return true;
            else return false;
        }
    }
}
