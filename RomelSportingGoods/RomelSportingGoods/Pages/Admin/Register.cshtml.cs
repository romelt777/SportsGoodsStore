using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RomelSportingGoods.Data;
using RomelSportingGoods.Models;

namespace RomelSportingGoods.Pages.Admin
{
	[Authorize]

	public class RegisterModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;
        [BindProperty]
        public UserRomelSportsGoods User { get; set; } = default!;

        public RegisterModel(RomelSportingGoodsContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.UserRomelSportsGoods == null || User == null)
            {
                return Page();
            }

            //encrypt password
            //using bcrypt to encrypt password
            User.Password = BCrypt.Net.BCrypt.HashPassword(User.Password);

            _context.UserRomelSportsGoods.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Login");

        }
    }
}
