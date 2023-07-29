namespace XspecT.Test.Subjects.Purchase;

public class PurchaseHandler
{
    private readonly IBasketRepository _basketRepository;
    private readonly ITopicExchangeV2<BasketPurchasedV1> _basketPurchased;

    public PurchaseHandler(
        IBasketRepository basketRepository,
        ITopicExchangeV2<BasketPurchasedV1> basketPurchased)
    {
        _basketRepository = basketRepository;
        _basketPurchased = basketPurchased;
    }

    public async Task<PurchaseResponseModel> Purchase(int basketId)
    {
        await _basketRepository.UpdateStatus(basketId);
        var ev = new BasketPurchasedV1();
        await _basketPurchased.Publish(ev);
        return new PurchaseResponseModel();
    }
}