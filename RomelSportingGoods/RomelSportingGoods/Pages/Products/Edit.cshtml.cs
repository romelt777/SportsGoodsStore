using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Models;
using RomelSportingGoods.Data;
using Microsoft.AspNetCore.Authorization;

namespace RomelSportingGoods.Pages.Products
{
	[Authorize]

	public class EditModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;
        [BindProperty]
        public Product Product { get; set; } = default!;
        [BindProperty]
        public IFormFile? ImageUpload { get; set; }
        IWebHostEnvironment _env;
        public string previousImage { get; set; }

        public EditModel(RomelSportingGoods.Data.RomelSportingGoodsContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ModelState.Remove(nameof(Product.Image));
            ModelState.Remove("Product.Image");

            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;

            //setting previous image to string
            String ImageString = Product.Image;
            this.previousImage = ImageString;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
/*            this.ImageUpload = "";
*/
            ModelState.Remove(nameof(Product.Image));
            ModelState.Remove("Product.Image");

            //check if image is null

            if (ImageUpload!= null)
            {
                // Make a unique image name
                string imageName = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-") + ImageUpload.FileName;
                Product.Image = imageName;

                //
                // Upload the Image to the www/photo folder
                //

                string file = _env.ContentRootPath + "\\wwwroot\\photos\\" + imageName;

                using (FileStream fileStream = new FileStream(file, FileMode.Create))
                {
                    ImageUpload.CopyTo(fileStream);
                }
            }
            else
            {
            }

            

            /*            if (!ModelState.IsValid)
                        {
                            return Page();
                        }
            */
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
