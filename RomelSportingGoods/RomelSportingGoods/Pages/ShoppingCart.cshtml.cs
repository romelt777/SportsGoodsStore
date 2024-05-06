using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Data;
using RomelSportingGoods.Models;

namespace RomelSportingGoods.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly RomelSportingGoodsContext _context;

        private readonly ILogger<IndexModel> _logger;
        public List<Product> Product { get; set; } = default!;
        public List<Product> ShoppingCartList { get; set; } = default!;
        public int[] QuantityProducts { get; set; }

        public bool emptyCart;
        public double subtotal { get; set; }
        public double delivery { get; } = 10.00;
        public double taxAmount { get; set; }



        public ShoppingCartModel(ILogger<IndexModel> logger, RomelSportingGoodsContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {

                Product = await _context.Product
                    .ToListAsync(); //adding all products to a list. 
            }

            this.ShoppingCartList = new List<Product>();
            this.QuantityProducts = new int[this.Product.Count];

            //check if cookie exists
            if (Request.Cookies["shoppingCookie"] == null)
            {
                emptyCart = true;
            }
            else
            {
                //reading cookie
                var cookie = Request.Cookies["shoppingCookie"];
                string? cookieValue = cookie?.ToString();

                //split string cookie
                String[]? cookieProductIds = cookieValue?.Split(',');

                for (int x = 0; x < Product.Count; x++)
                {
                    for (int i = 0; i < cookieProductIds.Length; i++)
                    {
                        if (Product[x].ProductId.ToString() == cookieProductIds[i])
                        {
                            ShoppingCartList.Add(Product[x]);
                            QuantityProducts[x] += 1;
                        }
                    }
                }

            }
            //getting subtotal
            for (int i = 0; i < Product.Count; i++)
            {
                if (QuantityProducts[i] > 0)
                {
                    subtotal += Product[i].Price;
                }
            }


        }
    }
}
