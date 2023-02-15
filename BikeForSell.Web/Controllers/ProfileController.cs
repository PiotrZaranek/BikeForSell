using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BikeForSell.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public ProfileController(IProfileService profileService, IUserService userService)
        {
            _profileService = profileService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            string id = _userService.GetUserId();
            var model = _profileService.GetListPurchases(id);
            return View(model);
        }

        public IActionResult DeletePurchase(int id)
        {
            _profileService.DeletePurchase(id);
            return RedirectToAction("Purchase");
        }

        [HttpGet]
        public IActionResult Sales()
        {
            string id = _userService.GetUserId();
            var model = _profileService.GetListSales(id);
            return View(model);
        }

        public IActionResult ChangeState(int saleId, int decision)
        {
            _profileService.ChangeState(saleId, decision);
            return RedirectToAction("Sales");
        }

        public IActionResult DeleteSale(int id)
        {
            _profileService.DeleteSale(id);
            return RedirectToAction("Sales");
        }
    }
}
