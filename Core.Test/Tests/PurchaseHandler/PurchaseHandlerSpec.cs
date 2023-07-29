using XspecT.Fixture;
using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Tests.PurchaseHandler;

public abstract class PurchaseHandlerSpec<TResult> : SubjectSpecAsync<Subjects.Purchase.PurchaseHandler, TResult>
{
    protected PurchaseHandlerSpec() => Using(new Basket());
}