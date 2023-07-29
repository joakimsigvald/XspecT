using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Subjects.Purchase;

public interface ICheckoutProvider
{
    Task<Checkout> GetExistingCheckout(int basketId);
}