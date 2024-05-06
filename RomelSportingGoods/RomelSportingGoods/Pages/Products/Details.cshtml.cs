using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Models;
using RomelSportingGoods.Data;
using Microsoft.AspNetCore.Authorization;

namespace RomelSportingGoods.Pages.Products
{
    [Authorize]

    public class DetailsModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;

        public DetailsModel(RomelSportingGoods.Data.RomelSportingGoodsContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

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
    }
}
