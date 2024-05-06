using RomelSportingGoods.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Data;

namespace RomelSportingGoods.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RomelSportingGoodsContext _context;

        private readonly ILogger<IndexModel> _logger;
        public List<Product> Product { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, RomelSportingGoodsContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            if (_context.Product!= null)
            {

                Product = await _context.Product
                    .ToListAsync(); //adding all photos to a list. 
            }
        }
    }
}