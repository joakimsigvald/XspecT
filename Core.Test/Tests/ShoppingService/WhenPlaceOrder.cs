using XspecT.Test.Subjects;
using Moq;

namespace XspecT.Test.Tests.ShoppingService;

public abstract class WhenPlaceOrder : ShoppingServiceSpec<object>
{
    protected ShoppingCart Cart;

    protected WhenPlaceOrder() => When(_ => _.PlaceOrder(Cart));

    public abstract class GivenCart : WhenPlaceOrder
    {
        protected int ShopId;

        protected GivenCart() => Using(() => ShopId).GivenThat(() => ShopId = 123).GivenThat(() => Cart = new());

        [Fact] public void ThenOrderIsCreated() 
            => Then().Does<IOrderService>(_ => _.CreateOrder(Cart))
            .And<ILogger>(_ => _.ForContext("ShopId", ShopId));

        public class AndShopName : GivenCart
        {
            private const string _shopName = "BookShop";

            public AndShopName() => Using((_shopName, ""));

            [Fact]
            public void ThenLogOrderCreated_With_ShopName() 
                => Then().Does<ILogger>(_ => _.Information(It.Is<string>(s => s.Contains(_shopName))));
        }

        public class AndShopNameAndDivision : GivenCart
        {
            private const string _shopName = "BookShop";
            private const string _division = "Fiction";

            public AndShopNameAndDivision() => Using((_shopName, _division));

            [Fact]
            public void ThenLogOrderCreated_With_ShopNameAndDivision()
                => Then().Does<ILogger>(_ => _.Information(It.Is<string>(
                    s => s.Contains(_shopName) && s.Contains(_division))));
        }
    }
}