using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomelSportingGoods.Data;
using RomelSportingGoods.Models;
using System.Security.Claims;

namespace RomelSportingGoods.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;

        [BindProperty]
        public UserRomelSportsGoods User { get; set; } = default!;


        public LoginModel(RomelSportingGoodsContext context)
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

            //Get the user from the database
            //user record from database, make sure passwords match
            var dbUser = await _context.UserRomelSportsGoods.FirstOrDefaultAsync(u => u.Email == User.Email);

            //if user doesnt exist
            if (dbUser == null)
            {
                ModelState.AddModelError("User.Password", "Username not found");
                return Page();
            }

            //check the password
            //bcrypt will determine if password matched
            if (!BCrypt.Net.BCrypt.Verify(User.Password, dbUser.Password))
            {
                //if doesnt match, give error 
                ModelState.AddModelError("User.Password", "Incorrect password");

                return Page();
            }

            //Setup session (boiler plate code)
            //use built in framwork for cookie authentication
            //create session
            //create cookie
            //sign user in


            //reading on claims and roles => https://stackoverflow.com/questions/21645323/what-is-the-claims-in-asp-net-identity
            // In role-based Security, a user presents the credentials directly to the application.
            // In a claims-based model, the user presents the claims and not the credentials to the application.

            //claims=> common way of identicating users, google search "claims based authentication"/

            var claims = new List<Claim>
            {
                //creating multiple claims, this is what the user claims to be
                new Claim(ClaimTypes.Name, dbUser.UserId.ToString()),

                //role=>group authentications (aka "Users", or "Admins") 
                new Claim(ClaimTypes.Role, "Admin") //user is claiming it is of User type
            };

            //the actually claims may change
            //boilerplate code (creating claims identity, and signing in) 

            //pass in the claims and the defaultCookieAuthentication
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            //sign in method
            //create the session itself: puts the cookie in browser
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

            //setup session is done

            return RedirectToPage("/Products/Index");
        }
    }
}
