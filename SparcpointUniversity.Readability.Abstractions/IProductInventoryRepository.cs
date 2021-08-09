namespace SparcpointUniversity.Readability.Abstractions
{
    public interface IProductInventoryRepository
    {
        ProductInventoryEntry GetInventory(int productId);
        ProductInventoryEntry AdjustOnHandQuantity(int productId, int onHandQuantityChange);
    }
}
