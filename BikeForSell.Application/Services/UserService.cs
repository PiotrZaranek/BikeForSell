using BikeForSell.Application.Interfaces;
using BikeForSell.Domain.Interfaces;
using BikeForSell.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BikeForSell.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(IHttpContextAccessor context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public string GetUserId()
        {
            var id = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return id.Value;
        }

        public async void LogOutUser()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
