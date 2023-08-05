using XspecT.Test.Subjects.Order;

namespace XspecT.Test.Subjects.Purchase;

public interface ICheckoutProvider
{
    Task<Checkout> GetExistingCheckout(int basketId);
}