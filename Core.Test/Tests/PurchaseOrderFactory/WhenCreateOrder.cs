using XspecT.Test.Subjects.PurchaseOrder;

namespace XspecT.Test.Tests.PurchaseOrderFactory;

public class WhenCreateOrder : PurchaseOrderFactorySpec<OrderRecord>
{
    protected const int BasketId = 123;
    protected const int CompanyId = 234;
    protected Checkout Checkout = new() { Basket = new() };

    protected WhenCreateOrder() => When(() => SUT.CreateOrder(Checkout));

    public class GivenBasket : WhenCreateOrder
    {
        protected override void Set() => Checkout = new() { Basket = new() { Id = BasketId } };

        [Fact] public void ThenQuotationId_Is_BasketId() => Assert.Equal(BasketId, Result.QuotationId);
        [Fact] public void ThenOrderNo_Is_BasketId() => Assert.Equal($"{BasketId}", Result.OrderNo);
    }
}