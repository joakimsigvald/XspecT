using XspecT.Test.Subjects.Shopping;

namespace XspecT.Test.Subjects.Purchase;

public interface IBasketItemFactory
{
    Task<BasketItem[]> CreateBasketItems(int customerId, int companyId);
}