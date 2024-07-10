using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCardItem> ShoppingCardItems { get; set; }


        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCardItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1,

                };
                _context.ShoppingCardItems.Add(shoppingCartItem);

            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCardItems.Add(shoppingCartItem);
                }
                _context.SaveChanges();
            }
        }

        public List<ShoppingCardItem> GetShoppingCardItems()
            {
                return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());
            }

            public double GetShoppingCartTotal()
            {
                var total = _context.ShoppingCardItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
                return total;
            }
        }
    } 
