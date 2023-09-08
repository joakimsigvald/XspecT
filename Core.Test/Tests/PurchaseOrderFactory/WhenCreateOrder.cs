using XspecT.Test.Subjects.Order;
using XspecT.Verification;

namespace XspecT.Test.Tests.PurchaseOrderFactory;

public class WhenCreateOrder : PurchaseOrderFactorySpec<OrderRecord>
{
    protected const int BasketId = 123;
    protected const int CompanyId = 234;
    protected Checkout Checkout = new() { Basket = new() };

    protected WhenCreateOrder() => When(_ => _.CreateOrder(Checkout));

    public class GivenBasket : WhenCreateOrder
    {
        public GivenBasket() => GivenThat(() => Checkout = new() { Basket = new() { Id = BasketId } });

        [Fact] public void ThenQuotationId_Is_BasketId() => Result.QuotationId.Is(BasketId);
        [Fact] public void ThenOrderNo_Is_BasketId() => Result.OrderNo.Is($"{BasketId}");
    }
}