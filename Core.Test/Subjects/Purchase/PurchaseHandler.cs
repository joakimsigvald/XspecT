namespace XspecT.Test.Subjects.Purchase;

public class PurchaseHandler
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICheckoutProvider _checkoutProvider;
    private readonly ITopicExchangeV2<BasketPurchasedV1> _basketPurchased;

    public PurchaseHandler(
        IBasketRepository basketRepository,
        ICheckoutProvider checkoutProvider,
        ITopicExchangeV2<BasketPurchasedV1> basketPurchased)
    {
        _basketRepository = basketRepository;
        _checkoutProvider = checkoutProvider;
        _basketPurchased = basketPurchased;
    }

    public async Task<PurchaseResponseModel> Purchase(int basketId)
    {
        var checkout = await _checkoutProvider.GetExistingCheckout(basketId);
        checkout.IsOpen = false;
        await _basketRepository.UpdateStatus(basketId);
        var ev = new BasketPurchasedV1();
        await _basketPurchased.Publish(ev);
        return new PurchaseResponseModel();
    }
}