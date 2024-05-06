using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RomelSportingGoods.Models;
using System.Text;

namespace RomelSportingGoods.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly RomelSportingGoods.Data.RomelSportingGoodsContext _context;

        [BindProperty]
        public BillingInfo BillingInfo { get; set; } = default!;

        public CheckoutModel(RomelSportingGoods.Data.RomelSportingGoodsContext context)
        {
            _context = context; //database utility
        }

        public IActionResult OnGet()
        {
            System.Diagnostics.Debug.WriteLine("checkout!");
            return Page();

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Product == null || BillingInfo == null)
            {
                return Page();
            }


            //reading cookie
            var cookie = Request.Cookies["shoppingCookie"];
            string? cookieValue = cookie?.ToString();

            //setting cookie
            BillingInfo.products = cookieValue;

            //convert to json
            string json = JsonConvert.SerializeObject(BillingInfo);

            System.Diagnostics.Debug.WriteLine(json);

            //creating http client
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://nscc-inet2005-purchase-api.azurewebsites.net/");
/*            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
*/
            var content = new StringContent(json,Encoding.UTF8,"application/json");

            var response = client.PostAsync("purchase", content).Result;

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Secure = true;
            cookieOptions.Path = "/";
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(responseContent);
                Response.Cookies.Append("confirmationCodeCookie", responseContent, cookieOptions);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }


            Response.Cookies.Append("confirmationCookie", cookieValue, cookieOptions);

            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("shoppingCookie", "", cookieOptions);

            return RedirectToPage("./Confirmation");

        }


    }
}
