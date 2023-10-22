using XspecT.Test.Subjects;
using Moq;

namespace XspecT.Test.Tests.ShoppingService;

public abstract class WhenPlaceOrder : ShoppingServiceSpec<object>
{
    protected WhenPlaceOrder() => When(_ => _.PlaceOrder(A<ShoppingCart>()));

    [Theory]
    [InlineData(2)]
    [InlineData(123)]
    public void ThenOrderIsCreated(int shopId)
        => Using(shopId)
        .Then<IOrderService>(_ => _.CreateOrder(The<ShoppingCart>()))
        .And<ILogger>(_ => _.ForContext("ShopId", shopId));

    [Fact]
    public void ThenLogOrderCreated_With_ShopNameAndDivision()
        => Using((A<string>(), ASecond<string>()))
        .Then<ILogger>(_ => _.Information(
            It.Is<string>(s => s.Contains(A<string>()) && s.Contains(ASecond<string>()))));
}