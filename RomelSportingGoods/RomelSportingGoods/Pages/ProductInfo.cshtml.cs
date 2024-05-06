using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Models;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace RomelSportingGoods.Pages
{
    public class ProductInfoModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;
        public Product Product { get; set; } = default!;


        public ProductInfoModel(RomelSportingGoods.Data.RomelSportingGoodsContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);

            UserRomelSportsGoods? user = _context.UserRomelSportsGoods.Where(u => u.UserId == id).SingleOrDefault();

            if (user != null)
            {

            }

            //cookie

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Secure = true;
            cookieOptions.Path = "/";

            //check if cookie is null
            if (Request.Cookies["shoppingCookie"] == null)
            {

                Response.Cookies.Append("shoppingCookie", product.ProductId.ToString(), cookieOptions);
            }
            else
            {
                var cookie = Request.Cookies["shoppingCookie"];
                string? cookieValue = cookie?.ToString();


                Response.Cookies.Append("shoppingCookie", cookieValue +"," + product.ProductId.ToString(), cookieOptions);
            }


            return RedirectToPage("./Index");
        }

    }
}
