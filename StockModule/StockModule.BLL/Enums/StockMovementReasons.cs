using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModule.BLL
{
    public enum StockMovementReasons
    {
        NoReason = 0,
        MaterialToProduction,
        ProductionToMaterial,
        MaterialToShipping,
        ShippingToMaterial,
        IguMaterialToShipping,
        LotRemnantsFreeToReserved,
        LotRemnantsReservedToFree,
        ScreenMaterialToProduction,
        ScreenMaterialToShipping,
        InventoryMaterial,
        FreeRemnantToReserved,
        ReservedRemnantToFree,
        RestoreReservedRemnant,
        DeliveryCorrection,
        DeliveryLineNew,
        ScreenShippingToMaterial,
        ScreenProductionToMaterial,
        SpoilageMaterialToQuality,
        SpoilageProductionToQuality,
        NewRemnantToFree,
        MissingReservedRemnant,
        ReservedRemnantToProduction,
        FreeRemnantToProduction,
        ReturnProductionRemnantToFree,
        SpoilageRemoveMaterial,
        DeliveryQualityInvoice,
        RemoveFreeRemnant,
        ReturnProductionRemnantToReserved,
        IguSpoilageProduction,
        IguSpoilageCancelProduction,
        IguSpoilageSupplier,
        IguSpoilageCancelSupplier,
        IguQualityFinish,
        IguQualityUnfinish,
        ShippingToClient,
        InventoryAddMaterial,
        InventoryRemoveMaterial,
        DeliveryPurchaseLineNew,
        CorrectionFromProductionToMaterial,
        CorrectionFromMaterialToProduction,
        DeliveryPurchaseInvoice,
        SpoilageProductionToMaterial,
        ProdSpoilageMaterialToQuality,
        PurchaseToMaterial,
        MaterialSpoilageCancel,
        ProductionSpoilageCancel
    }
}
