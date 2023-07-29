using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Subjects.Purchase;

public interface IBasketRepository
{
    Task<Basket> GetEditable(int basketId);
    Task<Basket> UpdateStatus(int basketId);
}