namespace XspecT.Test.Subjects.PurchaseOrder;

public interface IBasketItemFactory
{
    Task<BasketItem[]> CreateBasketItems(int customerId, int companyId);
}
