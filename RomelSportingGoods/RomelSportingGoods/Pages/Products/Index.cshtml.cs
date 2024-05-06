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

	public class IndexModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;

        public IndexModel(RomelSportingGoods.Data.RomelSportingGoodsContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Product != null)
            {
                Product = await _context.Product.ToListAsync();
            }
        }
    }
}
