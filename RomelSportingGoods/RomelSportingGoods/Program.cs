using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RomelSportingGoods.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RomelSportingGoodsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RomelSportingGoodsContext") ?? throw new InvalidOperationException("Connection string 'RomelSportingGoodsContext' not found.")));


//add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //set expiration of cookie
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        //if page is refreshed timer gets reset. 
        options.SlidingExpiration = true;

        options.Cookie.Name = "RomelSportsGoodsAuth";
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//add user authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
