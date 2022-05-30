using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EatCritAndDie.Admin.Web.Controllerss;

[Route("account")]
public class AccountController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = "/")
    {
        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Discord");
    }
    
    [HttpGet("logout")]
    public async Task<IActionResult> Logout(string returnUrl = "/")
    {
        //This removes the cookie assigned to the user login.
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect(returnUrl);
    }
    
}