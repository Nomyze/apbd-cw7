namespace Repositories;

public interface IWarehouseRepository {
    public Task<bool> CheckProductExists(int IdProduct);
    public Task<bool> CheckWarehouseExists(int IdWarehouse);
    public Task<int?> GetOrder(int IdProduct, int Amount, DateTime CreatedAt);
    public Task<bool> OrderRealized(int IdProduct, int IdWarehouse, int IdOrder);
}
