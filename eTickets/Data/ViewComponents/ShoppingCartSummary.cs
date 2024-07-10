using eTickets.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            shoppingCart = _shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCardItems();
            return View(items.Count);
        }
    }
}
