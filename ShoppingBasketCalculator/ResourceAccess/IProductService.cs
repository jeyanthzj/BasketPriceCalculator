using ShoppingBasketCalculator.Models;

namespace ShoppingBasketCalculator.ResourceAccess
{
    public interface IProductService
    {
        Product GetProduct(string productName);
    }
}