using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RomelSportingGoods.Models;
using RomelSportingGoods.Data;
using Microsoft.AspNetCore.Authorization;

namespace RomelSportingGoods.Pages.Products
{
	[Authorize]

	public class CreateModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageUpload { get; set; }
        IWebHostEnvironment _env;

        public CreateModel(RomelSportingGoods.Data.RomelSportingGoodsContext context, IWebHostEnvironment env)
        {
            _context = context; //database utility
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }


       
        public async Task<IActionResult> OnPostAsync()
        {
            // Make a unique image name
            string imageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-") + ImageUpload.FileName;
            Product.Image = imageName;


            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();//this will use a SQL statement to save to database


            //
            // Upload the Image to the www/photo folder
            //

            string file = _env.ContentRootPath + "\\wwwroot\\photos\\" + imageName;

            using (FileStream fileStream = new FileStream(file, FileMode.Create))
            {
                ImageUpload.CopyTo(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}
