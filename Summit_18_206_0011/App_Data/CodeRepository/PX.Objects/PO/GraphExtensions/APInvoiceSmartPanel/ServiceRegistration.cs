using Autofac;
using PX.Data.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel
{
    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.ActivateOnApplicationStart<ExtensionSorting>();
        }

        private class ExtensionSorting
        {
            private static readonly Dictionary<Type, int> _order = new Dictionary<Type, int>
            {
                {typeof(LinkLineExtension), 6 },
                {typeof(AddPOOrderExtension), 5 },
                {typeof(AddPOOrderLineExtension), 4 },
                {typeof(AddPOReceiptExtension), 3 },
                {typeof(AddPOReceiptLineExtension), 2 },
                {typeof(AddLandedCostExtension), 1 },
            };

            public ExtensionSorting()
            {
                PXBuildManager.SortExtensions += StableSort;
            }

            private static void StableSort(List<Type> list)
            {
                if (list?.Count > 1)
                {
                    var stableSortedList = list.OrderByDescending(item =>
                    _order.ContainsKey(item) ? _order[item] : 0).ToList();

                    list.Clear();
                    list.AddRange(stableSortedList);
                }
            }
        }
    }
}
