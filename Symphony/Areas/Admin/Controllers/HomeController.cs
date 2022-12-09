using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Symphony.Areas.Admin.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Symphony.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View(new AdminLoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginModel model)
        {
            HttpClient httpClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var res = httpClient.PostAsync(Program.ApiAddress + "api/Admin/Login", stringContent).Result;
            var data = res.Content.ReadAsStringAsync().Result;
            AdminApiResult result = JsonSerializer.Deserialize<AdminApiResult>(data);
            if (result.Status)
            {
                HttpContext.Session.SetString("token", result.Data.Token);

                // get role from token
                string role = "";
                var stream = result.Data.Token;
                var handler = new JwtSecurityTokenHandler();
                var tokenDecoded = handler.ReadJwtToken(stream);

                foreach (var item in tokenDecoded.Claims)
                {
                    switch (item.Type)
                    {
                        case "role":
                            role = item.Value;
                            break;
                    }
                }

                // save login info to cookie
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Email),
                    new Claim(ClaimTypes.Role, role)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = model.Remember
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
