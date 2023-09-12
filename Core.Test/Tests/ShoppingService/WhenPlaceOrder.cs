using XspecT.Test.Subjects;
using Moq;
using XspecT.Verification;

namespace XspecT.Test.Tests.ShoppingService;

public abstract class WhenPlaceOrder : ShoppingServiceSpec<object>
{
    protected ShoppingCart Cart;

    protected WhenPlaceOrder() => When(_ => _.PlaceOrder(Cart));

    public abstract class GivenCart : WhenPlaceOrder
    {
        protected int ShopId;

        protected GivenCart() => Using(() => ShopId).Given(() => Cart = new());

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        public void ThenOrderIsCreated(int shopId)
            => Given(() => ShopId = shopId).Then<IOrderService>(_ => _.CreateOrder(Cart))
            .And<ILogger>(_ => _.ForContext("ShopId", shopId));

        public class AndShopName : GivenCart
        {
            private const string _shopName = "BookShop";

            public AndShopName() => Using((_shopName, ""));

            [Fact]
            public void ThenLogOrderCreated_With_ShopName()
                => Then<ILogger>(_ => _.Information(It.Is<string>(s => s.Contains(_shopName))));
        }

        public class AndShopNameAndDivision : GivenCart
        {
            private const string _shopName = "BookShop";
            private const string _division = "Fiction";

            public AndShopNameAndDivision() => Using((_shopName, _division));

            [Fact]
            public void ThenLogOrderCreated_With_ShopNameAndDivision()
                => Then<ILogger>(_ => _.Information(
                    It.Is<string>(s => s.Contains(_shopName) && s.Contains(_division))))
                .And().Result.Is().Null();
        }
    }
}