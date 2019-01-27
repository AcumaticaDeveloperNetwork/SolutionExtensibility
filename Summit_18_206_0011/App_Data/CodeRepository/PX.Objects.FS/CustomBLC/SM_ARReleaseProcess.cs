using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.IN;
using PX.Objects.SO;
using System.Collections.Generic;
using System.Linq;

namespace PX.Objects.FS
{
    public class SM_ARReleaseProcess : PXGraphExtension<ARReleaseProcess>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>()
                    && PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
        }

        public bool processEquipmentAndComponents = false;

        public delegate void PersistDelegate();

        [PXOverride]
        public void Persist(PersistDelegate baseMethod)
        {
            if (SharedFunctions.isFSSetupSet(Base) == false)
            {
                baseMethod();
                return;
            }

            //// Invoice
            ARRegister arRegisterRow = (ARRegister)Base.Caches[typeof(ARRegister)].Current;
            Dictionary<int?, int?> newEquiments = new Dictionary<int?, int?>();
            SMEquipmentMaint graphSMEquipmentMaint = PXGraph.CreateInstance<SMEquipmentMaint>();

            if (processEquipmentAndComponents)
            {
                CreateEquipments(graphSMEquipmentMaint, arRegisterRow, newEquiments);
                ReplaceEquipments(graphSMEquipmentMaint, arRegisterRow);
                UpgradeEquipmentComponents(graphSMEquipmentMaint, arRegisterRow, newEquiments);
                CreateEquipmentComponents(graphSMEquipmentMaint, arRegisterRow, newEquiments);
                ReplaceComponents(graphSMEquipmentMaint, arRegisterRow);
            }

            baseMethod();
        }

        #region Methods
        private void CreateEquipments(
            SMEquipmentMaint graphSMEquipmentMaint,
            ARRegister arRegisterRow,
            Dictionary<int?, int?> newEquiments)
        {
            var inventoryItemSet = PXSelectJoin<InventoryItem,
                                   InnerJoin<ARTran,
                                            On<ARTran.inventoryID, Equal<InventoryItem.inventoryID>,
                                            And<ARTran.tranType, Equal<ARDocType.invoice>>>,
                                   InnerJoin<SOLineSplit,
                                        On<SOLineSplit.orderType, Equal<ARTran.sOOrderType>,
                                        And<SOLineSplit.orderNbr, Equal<ARTran.sOOrderNbr>,
                                        And<SOLineSplit.lineNbr, Equal<ARTran.sOOrderLineNbr>,
                                        And<SOLineSplit.qty, Greater<Zero>>>>>,
                                   InnerJoin<SOLine,
                                        On<SOLine.orderType, Equal<SOLineSplit.orderType>,
                                        And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>,
                                   LeftJoin<SOShipLineSplit,
                                        On<SOShipLineSplit.origOrderType, Equal<SOLineSplit.orderType>,
                                        And<SOShipLineSplit.origOrderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOShipLineSplit.origLineNbr, Equal<SOLineSplit.lineNbr>,
                                        And<SOShipLineSplit.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>,
                                   Where<
                                        ARTran.tranType, Equal<Required<ARInvoice.docType>>,
                                        And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>,
                                        And<FSxEquipmentModel.eQEnabled, Equal<True>,
                                        And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentAction.SellingTargetEquipment>,
                                        And<FSxSOLine.sMEquipmentID, IsNull,
                                        And<FSxSOLine.newTargetEquipmentLineNbr, IsNull,
                                        And<FSxSOLine.componentID, IsNull,
                                        And<SOLineSplit.pOCreate, Equal<False>>>>>>>>>,
                                   OrderBy<
                                        Asc<ARTran.tranType,
                                        Asc<ARTran.refNbr,
                                        Asc<ARTran.lineNbr>>>>>
                                   .Select(Base, arRegisterRow.DocType, arRegisterRow.RefNbr);

            Create_Replace_Equipments(graphSMEquipmentMaint, inventoryItemSet, arRegisterRow, newEquiments, ID.Equipment_Action.SELLING_TARGET_EQUIPMENT);
        }

        private void UpgradeEquipmentComponents(
            SMEquipmentMaint graphSMEquipmentMaint,
            ARRegister arRegisterRow,
            Dictionary<int?, int?> newEquiments)
        {
            var inventoryItemSet = PXSelectJoin<InventoryItem,
                                   InnerJoin<ARTran,
                                            On<ARTran.inventoryID, Equal<InventoryItem.inventoryID>,
                                            And<ARTran.tranType, Equal<ARDocType.invoice>>>,
                                   InnerJoin<SOLineSplit,
                                        On<SOLineSplit.orderType, Equal<ARTran.sOOrderType>,
                                        And<SOLineSplit.orderNbr, Equal<ARTran.sOOrderNbr>,
                                        And<SOLineSplit.lineNbr, Equal<ARTran.sOOrderLineNbr>,
                                        And<SOLineSplit.qty, Greater<Zero>>>>>,
                                   InnerJoin<SOLine,
                                        On<SOLine.orderType, Equal<SOLineSplit.orderType>,
                                        And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>,
                                   LeftJoin<SOShipLineSplit,
                                        On<SOShipLineSplit.origOrderType, Equal<SOLineSplit.orderType>,
                                        And<SOShipLineSplit.origOrderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOShipLineSplit.origLineNbr, Equal<SOLineSplit.lineNbr>,
                                        And<SOShipLineSplit.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>,
                                   Where<
                                        ARTran.tranType, Equal<Required<ARInvoice.docType>>,
                                        And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>,
                                        And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentAction.UpgradingComponent>,
                                        And<FSxSOLine.sMEquipmentID, IsNull,
                                        And<FSxSOLine.newTargetEquipmentLineNbr, IsNotNull,
                                        And<FSxSOLine.componentID, IsNotNull,
                                        And<FSxSOLine.equipmentLineRef, IsNull,
                                        And<SOLineSplit.pOCreate, Equal<False>>>>>>>>>,
                                   OrderBy<
                                        Asc<ARTran.tranType,
                                        Asc<ARTran.refNbr,
                                        Asc<ARTran.lineNbr>>>>>
                                   .Select(Base, arRegisterRow.DocType, arRegisterRow.RefNbr);

            foreach (PXResult<InventoryItem, ARTran, SOLineSplit, SOLine, SOShipLineSplit> bqlResult in inventoryItemSet)
            {
                ARTran arTranRow = (ARTran)bqlResult;
                InventoryItem inventoryItemRow = (InventoryItem)bqlResult;
                SOLine soLineRow = (SOLine)bqlResult;
                SOLineSplit soLineSplitRow = (SOLineSplit)bqlResult;
                SOShipLineSplit soShipLineSplitRow = (SOShipLineSplit)bqlResult;
                FSxSOLine fsxSOLineRow = PXCache<SOLine>.GetExtension<FSxSOLine>(soLineRow);
                FSxARTran fsxARTranRow = PXCache<ARTran>.GetExtension<FSxARTran>(arTranRow);

                int? smEquipmentID = -1;
                if (newEquiments.TryGetValue(fsxSOLineRow.NewTargetEquipmentLineNbr, out smEquipmentID))
                {
                    graphSMEquipmentMaint.EquipmentRecords.Current = graphSMEquipmentMaint.EquipmentRecords.Search<FSEquipment.SMequipmentID>(smEquipmentID);

                    FSEquipmentComponent fsEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Select().Where(x => ((FSEquipmentComponent)x).ComponentID == fsxSOLineRow.ComponentID).FirstOrDefault();

                    if (fsEquipmentComponentRow != null)
                    {
                        fsEquipmentComponentRow.SalesOrderNbr = soLineRow.OrderNbr;
                        fsEquipmentComponentRow.SalesOrderType = soLineRow.OrderType;
                        fsEquipmentComponentRow.LongDescr = soLineRow.TranDesc;
                        fsEquipmentComponentRow.InvoiceRefNbr = arTranRow.RefNbr;
                        fsEquipmentComponentRow.InstallationDate = arTranRow.TranDate != null ? arTranRow.TranDate : arRegisterRow.DocDate;

                        if (fsxSOLineRow != null)
                        {
                            if (fsxSOLineRow.AppointmentID != null)
                            {
                                fsEquipmentComponentRow.InstAppointmentID = fsxSOLineRow.AppointmentID;
                                fsEquipmentComponentRow.InstallationDate = fsxSOLineRow.AppointmentDate;
                            }
                            else if (fsxSOLineRow.SOID != null)
                            {
                                fsEquipmentComponentRow.InstServiceOrderID = fsxSOLineRow.SOID;
                                fsEquipmentComponentRow.InstallationDate = fsxSOLineRow.ServiceOrderDate;
                            }

                            fsEquipmentComponentRow.Comment = fsxSOLineRow.Comment;
                        }

                        if (soLineSplitRow != null)
                        {
                            fsEquipmentComponentRow.SerialNumber = soLineSplitRow.LotSerialNbr;
                        }

                        if (fsEquipmentComponentRow.SerialNumber == null
                            && soShipLineSplitRow != null)
                        {
                            fsEquipmentComponentRow.SerialNumber = soShipLineSplitRow.LotSerialNbr;
                        }

                        fsEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Update(fsEquipmentComponentRow);

                        graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.inventoryID>(fsEquipmentComponentRow, soLineRow.InventoryID);
                        graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.salesDate>(fsEquipmentComponentRow, soLineRow.OrderDate);
                        graphSMEquipmentMaint.Save.Press();
                    }
                }
            }
        }

        private void CreateEquipmentComponents(
            SMEquipmentMaint graphSMEquipmentMaint,
            ARRegister arRegisterRow,
            Dictionary<int?, int?> newEquiments)
        {
            var inventoryItemSet = PXSelectJoin<InventoryItem,
                                   InnerJoin<ARTran,
                                            On<ARTran.inventoryID, Equal<InventoryItem.inventoryID>,
                                            And<ARTran.tranType, Equal<ARDocType.invoice>>>,
                                   InnerJoin<SOLineSplit,
                                        On<SOLineSplit.orderType, Equal<ARTran.sOOrderType>,
                                        And<SOLineSplit.orderNbr, Equal<ARTran.sOOrderNbr>,
                                        And<SOLineSplit.lineNbr, Equal<ARTran.sOOrderLineNbr>,
                                        And<SOLineSplit.qty, Greater<Zero>>>>>,
                                   InnerJoin<SOLine,
                                        On<SOLine.orderType, Equal<SOLineSplit.orderType>,
                                        And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>,
                                   LeftJoin<SOShipLineSplit,
                                        On<SOShipLineSplit.origOrderType, Equal<SOLineSplit.orderType>,
                                        And<SOShipLineSplit.origOrderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOShipLineSplit.origLineNbr, Equal<SOLineSplit.lineNbr>,
                                        And<SOShipLineSplit.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>,
                                   Where<
                                        ARTran.tranType, Equal<Required<ARInvoice.docType>>,
                                        And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>,
                                        And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentAction.CreatingComponent>,
                                        And<FSxSOLine.componentID, IsNotNull,
                                        And<FSxSOLine.equipmentLineRef, IsNull,
                                        And<SOLineSplit.pOCreate, Equal<False>>>>>>>,
                                   OrderBy<
                                        Asc<ARTran.tranType,
                                        Asc<ARTran.refNbr,
                                        Asc<ARTran.lineNbr>>>>>
                                   .Select(Base, arRegisterRow.DocType, arRegisterRow.RefNbr);

            foreach (PXResult<InventoryItem, ARTran, SOLineSplit, SOLine, SOShipLineSplit> bqlResult in inventoryItemSet)
            {
                ARTran arTranRow = (ARTran)bqlResult;
                InventoryItem inventoryItemRow = (InventoryItem)bqlResult;
                SOLine soLineRow = (SOLine)bqlResult;
                SOLineSplit soLineSplitRow = (SOLineSplit)bqlResult;
                SOShipLineSplit soShipLineSplitRow = (SOShipLineSplit)bqlResult;
                FSxSOLine fsxSOLineRow = PXCache<SOLine>.GetExtension<FSxSOLine>(soLineRow);
                FSxARTran fsxARTranRow = PXCache<ARTran>.GetExtension<FSxARTran>(arTranRow);

                int? smEquipmentID = -1;
                if (fsxSOLineRow.NewTargetEquipmentLineNbr != null && fsxSOLineRow.SMEquipmentID == null)
                {
                    if (newEquiments.TryGetValue(fsxSOLineRow.NewTargetEquipmentLineNbr, out smEquipmentID))
                    {
                        graphSMEquipmentMaint.EquipmentRecords.Current = graphSMEquipmentMaint.EquipmentRecords.Search<FSEquipment.SMequipmentID>(smEquipmentID);
                    }
                }

                if (fsxSOLineRow.NewTargetEquipmentLineNbr == null && fsxSOLineRow.SMEquipmentID != null)
                {
                    graphSMEquipmentMaint.EquipmentRecords.Current = graphSMEquipmentMaint.EquipmentRecords.Search<FSEquipment.SMequipmentID>(fsxSOLineRow.SMEquipmentID);
                }

                if (graphSMEquipmentMaint.EquipmentRecords.Current != null)
                {
                    FSEquipmentComponent fsEquipmentComponentRow = new FSEquipmentComponent();
                    fsEquipmentComponentRow.ComponentID = fsxSOLineRow.ComponentID;
                    fsEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Insert(fsEquipmentComponentRow);

                    fsEquipmentComponentRow.SalesOrderNbr = soLineRow.OrderNbr;
                    fsEquipmentComponentRow.SalesOrderType = soLineRow.OrderType;
                    fsEquipmentComponentRow.InvoiceRefNbr = arTranRow.RefNbr;
                    fsEquipmentComponentRow.InstallationDate = arTranRow.TranDate != null ? arTranRow.TranDate : arRegisterRow.DocDate;

                    if (fsxSOLineRow != null)
                    {
                        if (fsxSOLineRow.AppointmentID != null)
                        {
                            fsEquipmentComponentRow.InstAppointmentID = fsxSOLineRow.AppointmentID;
                            fsEquipmentComponentRow.InstallationDate = fsxSOLineRow.AppointmentDate;
                        }
                        else if (fsxSOLineRow.SOID != null)
                        {
                            fsEquipmentComponentRow.InstServiceOrderID = fsxSOLineRow.SOID;
                            fsEquipmentComponentRow.InstallationDate = fsxSOLineRow.ServiceOrderDate;
                        }

                        fsEquipmentComponentRow.Comment = fsxSOLineRow.Comment;
                    }

                    if (soLineSplitRow != null)
                    {
                        fsEquipmentComponentRow.SerialNumber = soLineSplitRow.LotSerialNbr;
                    }

                    if (fsEquipmentComponentRow.SerialNumber == null
                        && soShipLineSplitRow != null)
                    {
                        fsEquipmentComponentRow.SerialNumber = soShipLineSplitRow.LotSerialNbr;
                    }

                    fsEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Update(fsEquipmentComponentRow);

                    graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.inventoryID>(fsEquipmentComponentRow, soLineRow.InventoryID);
                    graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.salesDate>(fsEquipmentComponentRow, soLineRow.OrderDate);
                    graphSMEquipmentMaint.Save.Press();
                }
            }
        }

        private void ReplaceEquipments(
            SMEquipmentMaint graphSMEquipmentMaint,
            ARRegister arRegisterRow)
        {
            var inventoryItemSet = PXSelectJoin<InventoryItem,
                                   InnerJoin<ARTran,
                                            On<ARTran.inventoryID, Equal<InventoryItem.inventoryID>,
                                            And<ARTran.tranType, Equal<ARDocType.invoice>>>,
                                   InnerJoin<SOLineSplit,
                                        On<SOLineSplit.orderType, Equal<ARTran.sOOrderType>,
                                        And<SOLineSplit.orderNbr, Equal<ARTran.sOOrderNbr>,
                                        And<SOLineSplit.lineNbr, Equal<ARTran.sOOrderLineNbr>,
                                        And<SOLineSplit.qty, Greater<Zero>>>>>,
                                   InnerJoin<SOLine,
                                        On<SOLine.orderType, Equal<SOLineSplit.orderType>,
                                        And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>,
                                   LeftJoin<SOShipLineSplit,
                                        On<SOShipLineSplit.origOrderType, Equal<SOLineSplit.orderType>,
                                        And<SOShipLineSplit.origOrderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOShipLineSplit.origLineNbr, Equal<SOLineSplit.lineNbr>,
                                        And<SOShipLineSplit.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>,
                                   Where<
                                        ARTran.tranType, Equal<Required<ARInvoice.docType>>,
                                        And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>,
                                        And<FSxEquipmentModel.eQEnabled, Equal<True>,
                                        And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentAction.ReplacingTargetEquipment>,
                                        And<FSxSOLine.sMEquipmentID, IsNotNull,
                                        And<FSxSOLine.newTargetEquipmentLineNbr, IsNull,
                                        And<FSxSOLine.componentID, IsNull,
                                        And<SOLineSplit.pOCreate, Equal<False>>>>>>>>>,
                                   OrderBy<
                                        Asc<ARTran.tranType,
                                        Asc<ARTran.refNbr,
                                        Asc<ARTran.lineNbr>>>>>
                                   .Select(Base, arRegisterRow.DocType, arRegisterRow.RefNbr);

            Create_Replace_Equipments(graphSMEquipmentMaint, inventoryItemSet, arRegisterRow, null, ID.Equipment_Action.REPLACING_TARGET_EQUIPMENT);
        }

        private void ReplaceComponents(
            SMEquipmentMaint graphSMEquipmentMaint,
            ARRegister arRegisterRow)
        {
            var inventoryItemSet = PXSelectJoin<InventoryItem,
                                   InnerJoin<ARTran,
                                            On<ARTran.inventoryID, Equal<InventoryItem.inventoryID>,
                                            And<ARTran.tranType, Equal<ARDocType.invoice>>>,
                                   InnerJoin<SOLineSplit,
                                        On<SOLineSplit.orderType, Equal<ARTran.sOOrderType>,
                                        And<SOLineSplit.orderNbr, Equal<ARTran.sOOrderNbr>,
                                        And<SOLineSplit.lineNbr, Equal<ARTran.sOOrderLineNbr>,
                                        And<SOLineSplit.qty, Greater<Zero>>>>>,
                                   InnerJoin<SOLine,
                                        On<SOLine.orderType, Equal<SOLineSplit.orderType>,
                                        And<SOLine.orderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOLine.lineNbr, Equal<SOLineSplit.lineNbr>>>>,
                                   LeftJoin<SOShipLineSplit,
                                        On<SOShipLineSplit.origOrderType, Equal<SOLineSplit.orderType>,
                                        And<SOShipLineSplit.origOrderNbr, Equal<SOLineSplit.orderNbr>,
                                        And<SOShipLineSplit.origLineNbr, Equal<SOLineSplit.lineNbr>,
                                        And<SOShipLineSplit.origSplitLineNbr, Equal<SOLineSplit.splitLineNbr>>>>>>>>>,
                                   Where<
                                        ARTran.tranType, Equal<Required<ARInvoice.docType>>,
                                        And<ARTran.refNbr, Equal<Required<ARInvoice.refNbr>>,
                                        And<FSxEquipmentModel.eQEnabled, Equal<True>,
                                        And<FSxSOLine.equipmentAction, Equal<ListField_EquipmentAction.ReplacingComponent>,
                                        And<FSxSOLine.sMEquipmentID, IsNotNull,
                                        And<FSxSOLine.newTargetEquipmentLineNbr, IsNull,
                                        And<FSxSOLine.equipmentLineRef, IsNotNull,
                                        And<SOLineSplit.pOCreate, Equal<False>>>>>>>>>,
                                   OrderBy<
                                        Asc<ARTran.tranType,
                                        Asc<ARTran.refNbr,
                                        Asc<ARTran.lineNbr>>>>>
                                   .Select(Base, arRegisterRow.DocType, arRegisterRow.RefNbr);

            foreach (PXResult<InventoryItem, ARTran, SOLineSplit, SOLine, SOShipLineSplit> bqlResult in inventoryItemSet)
            {
                ARTran arTranRow = (ARTran)bqlResult;
                InventoryItem inventoryItemRow = (InventoryItem)bqlResult;
                SOLine soLineRow = (SOLine)bqlResult;
                SOLineSplit soLineSplitRow = (SOLineSplit)bqlResult;
                SOShipLineSplit soShipLineSplitRow = (SOShipLineSplit)bqlResult;
                FSxSOLine fsxSOLineRow = PXCache<SOLine>.GetExtension<FSxSOLine>(soLineRow);
                FSxARTran fsxARTranRow = PXCache<ARTran>.GetExtension<FSxARTran>(arTranRow);

                graphSMEquipmentMaint.EquipmentRecords.Current = graphSMEquipmentMaint.EquipmentRecords.Search<FSEquipment.SMequipmentID>(fsxSOLineRow.SMEquipmentID);

                FSEquipmentComponent fsEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Select().Where(x => ((FSEquipmentComponent)x).LineNbr == fsxSOLineRow.EquipmentLineRef).FirstOrDefault();

                FSEquipmentComponent fsNewEquipmentComponentRow = new FSEquipmentComponent();
                fsNewEquipmentComponentRow.ComponentID = fsxSOLineRow.ComponentID;
                fsNewEquipmentComponentRow = graphSMEquipmentMaint.ApplyComponentReplacement(fsEquipmentComponentRow, fsNewEquipmentComponentRow);

                fsNewEquipmentComponentRow.SalesOrderNbr = soLineRow.OrderNbr;
                fsNewEquipmentComponentRow.SalesOrderType = soLineRow.OrderType;
                fsNewEquipmentComponentRow.InvoiceRefNbr = arTranRow.RefNbr;
                fsNewEquipmentComponentRow.InstallationDate = arTranRow.TranDate != null ? arTranRow.TranDate : arRegisterRow.DocDate;

                if (fsxSOLineRow != null)
                {
                    if (fsxSOLineRow.AppointmentID != null)
                    {
                        fsNewEquipmentComponentRow.InstAppointmentID = fsxSOLineRow.AppointmentID;
                        fsNewEquipmentComponentRow.InstallationDate = fsxSOLineRow.AppointmentDate;
                    }
                    else if (fsxSOLineRow.SOID != null)
                    {
                        fsNewEquipmentComponentRow.InstServiceOrderID = fsxSOLineRow.SOID;
                        fsNewEquipmentComponentRow.InstallationDate = fsxSOLineRow.ServiceOrderDate;
                    }

                    fsNewEquipmentComponentRow.Comment = fsxSOLineRow.Comment;
                }

                fsNewEquipmentComponentRow.LongDescr = soLineRow.TranDesc;

                if (soLineSplitRow != null)
                {
                    fsNewEquipmentComponentRow.SerialNumber = soLineSplitRow.LotSerialNbr;
                }

                if (fsNewEquipmentComponentRow.SerialNumber == null
                    && soShipLineSplitRow != null)
                {
                    fsNewEquipmentComponentRow.SerialNumber = soShipLineSplitRow.LotSerialNbr;
                }

                fsNewEquipmentComponentRow = graphSMEquipmentMaint.EquipmentWarranties.Update(fsNewEquipmentComponentRow);

                graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.inventoryID>(fsNewEquipmentComponentRow, soLineRow.InventoryID);
                graphSMEquipmentMaint.EquipmentWarranties.SetValueExt<FSEquipmentComponent.salesDate>(fsNewEquipmentComponentRow, soLineRow.OrderDate);
                graphSMEquipmentMaint.Save.Press();
            }
        }

        private void Create_Replace_Equipments(
            SMEquipmentMaint graphSMEquipmentMaint,
            PXResultset<InventoryItem> inventoryItemSet,
            ARRegister arRegisterRow,
            Dictionary<int?, int?> newEquiments,
            string action)
        {
            foreach (PXResult<InventoryItem, ARTran, SOLineSplit, SOLine, SOShipLineSplit> bqlResult in inventoryItemSet)
            {
                ARTran arTranRow = (ARTran)bqlResult;
                InventoryItem inventoryItemRow = (InventoryItem)bqlResult;
                SOLine soLineRow = (SOLine)bqlResult;
                SOLineSplit soLineSplitRow = (SOLineSplit)bqlResult;
                SOShipLineSplit soShipLineSplitRow = (SOShipLineSplit)bqlResult;
                FSEquipment fsEquipmentRow = null;
                FSxSOLine fsxSOLineRow = PXCache<SOLine>.GetExtension<FSxSOLine>(soLineRow);
                FSxARTran fsxARTranRow = PXCache<ARTran>.GetExtension<FSxARTran>(arTranRow);
                FSxEquipmentModel fsxEquipmentModelRow = PXCache<InventoryItem>.GetExtension<FSxEquipmentModel>(inventoryItemRow);
                int? iteratorMax = (int?) (soShipLineSplitRow == null || soShipLineSplitRow.Qty == null ? soLineSplitRow.Qty : soShipLineSplitRow.Qty);


                for (int i = 0; i < iteratorMax; i++)
                {
                    SoldInventoryItem soldInventoryItemRow = new SoldInventoryItem();

                    soldInventoryItemRow.CustomerID = arRegisterRow.CustomerID;
                    soldInventoryItemRow.CustomerLocationID = arRegisterRow.CustomerLocationID;
                    soldInventoryItemRow.InventoryID = inventoryItemRow.InventoryID;
                    soldInventoryItemRow.InventoryCD = inventoryItemRow.InventoryCD;
                    soldInventoryItemRow.InvoiceRefNbr = arTranRow.RefNbr;
                    soldInventoryItemRow.InvoiceLineNbr = arTranRow.LineNbr;
                    soldInventoryItemRow.DocType = arRegisterRow.DocType;
                    soldInventoryItemRow.DocDate = arTranRow.TranDate != null ? arTranRow.TranDate : arRegisterRow.DocDate;

                    if (fsxSOLineRow != null)
                    {
                        if (fsxSOLineRow.AppointmentID != null)
                        {
                            soldInventoryItemRow.DocDate = fsxSOLineRow.AppointmentDate;
                        } else if (fsxSOLineRow.SOID != null)
                        {
                            soldInventoryItemRow.DocDate = fsxSOLineRow.ServiceOrderDate;
                        }
                    }

                    soldInventoryItemRow.Descr = inventoryItemRow.Descr;
                    soldInventoryItemRow.SiteID = arTranRow.SiteID;
                    soldInventoryItemRow.ItemClassID = inventoryItemRow.ItemClassID;
                    soldInventoryItemRow.SOOrderType = soLineRow.OrderType;
                    soldInventoryItemRow.SOOrderNbr = soLineRow.OrderNbr;
                    soldInventoryItemRow.SOOrderDate = soLineRow.OrderDate;
                    soldInventoryItemRow.EquipmentTypeID = fsxEquipmentModelRow.EquipmentTypeID;


                    if (soLineSplitRow != null)
                    {
                        soldInventoryItemRow.LotSerialNumber = soLineSplitRow.LotSerialNbr;
                    }

                    if (soldInventoryItemRow.LotSerialNumber == null
                        && soShipLineSplitRow != null)
                    {
                        soldInventoryItemRow.LotSerialNumber = soShipLineSplitRow.LotSerialNbr;
                    }

                    fsEquipmentRow = SharedFunctions.CreateSoldEquipment(graphSMEquipmentMaint, soldInventoryItemRow, soLineRow, fsxSOLineRow, action, inventoryItemRow);
                }

                if (fsEquipmentRow != null)
                {
                    fsxARTranRow.SMEquipmentID = fsEquipmentRow.SMEquipmentID;
                    Base.ARTran_TranType_RefNbr.Update(arTranRow);

                    if (action == ID.Equipment_Action.SELLING_TARGET_EQUIPMENT)
                    {
                        int? smEquipmentID = -1;
                        if (newEquiments.TryGetValue(soLineRow.LineNbr, out smEquipmentID) == false)
                        {
                            newEquiments.Add(
                                soLineRow.LineNbr,
                                fsEquipmentRow.SMEquipmentID);
                        }
                    }
                    else if (action == ID.Equipment_Action.REPLACING_TARGET_EQUIPMENT)
                    {
                        if (fsxSOLineRow != null)
                        {
                            graphSMEquipmentMaint.EquipmentRecords.Current = graphSMEquipmentMaint.EquipmentRecords.Search<FSEquipment.SMequipmentID>(fsxSOLineRow.SMEquipmentID);
                            graphSMEquipmentMaint.EquipmentRecords.Current.ReplaceEquipmentID = fsEquipmentRow.SMEquipmentID;
                            graphSMEquipmentMaint.EquipmentRecords.Current.Status = ID.Equipment_Status.DISPOSED;
                            graphSMEquipmentMaint.EquipmentRecords.Current.DisposalDate = soLineRow.OrderDate;
                            graphSMEquipmentMaint.EquipmentRecords.Current.DispServiceOrderID = fsxSOLineRow.SOID;
                            graphSMEquipmentMaint.EquipmentRecords.Current.DispAppointmentID = fsxSOLineRow.AppointmentID;
                            graphSMEquipmentMaint.EquipmentRecords.Cache.SetStatus(graphSMEquipmentMaint.EquipmentRecords.Current, PXEntryStatus.Updated);
                            graphSMEquipmentMaint.Save.Press();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
