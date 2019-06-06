using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TripWMe.Domain.User;
using TripWMe.Models.User;

namespace TripWMe.App.Controllers
{
    //[Authorize]
    public class AppController : Controller
    {
        private readonly UserManager<TUser> _userManager;

        public AppController(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();
            await CreateNewUser();
            return View();
        }

        private async Task CreateNewUser()
        {
            var userIdentity = new UserIdentityModel();
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "sub")
                {
                    userIdentity.sub = claim.Value;
                }
                if (claim.Type == "given_name")
                {
                    userIdentity.given_name = claim.Value;
                }
                if (claim.Type == "family_name")
                {
                    userIdentity.given_name = claim.Value;
                }
            }
            var existUser = await _userManager.FindByIdAsync(userIdentity.sub);
            if(existUser == null)
            {

            }
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public async Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
    }
}
