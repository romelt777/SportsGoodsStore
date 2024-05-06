using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RomelSportingGoods.Pages.Admin
{
    public class LogoutModel : PageModel
    {
		public async Task<IActionResult> OnGet()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToPage("../Index");
		}
    }
}
