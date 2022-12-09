using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        // Admin authentication
        option.LoginPath = "/admin/home/login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // add authentication
app.UseAuthorization();
app.UseSession(); // use session

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Student}/{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program
{
    public static string ApiAddress = "https://localhost:7119/";
}