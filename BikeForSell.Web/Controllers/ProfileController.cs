using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BikeForSell.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            const int id = 1;

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
            const int id = 2;

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
