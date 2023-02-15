using BikeForSell.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BikeForSell.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        public UserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserId()
        {
            var id = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return id.Value;
        }
    }
}
