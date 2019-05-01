using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoGoogleAuth.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public IActionResult OnGetAsync(string returnUrl = null)
        {
            string provider = "Google";
            
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Page(
                    "./Login",
                    pageHandler: "Callback",
                    values: new { returnUrl }),
            };

            return new ChallengeResult(provider, authenticationProperties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(
            string returnUrl = null, string remoteError = null)
        {
            var GoogleUser = this.User.Identities.FirstOrDefault();

            if (GoogleUser.IsAuthenticated)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };

                await HttpContext.SignInAsync(

                CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(GoogleUser),
                    authProperties);
            }

            return LocalRedirect("/");
        }

    }
}