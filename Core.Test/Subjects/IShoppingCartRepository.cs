using System.Threading.Tasks;

namespace XspecT.Test.Subjects;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> GetCart(int id);
    Task StoreCart(ShoppingCart cart);
}