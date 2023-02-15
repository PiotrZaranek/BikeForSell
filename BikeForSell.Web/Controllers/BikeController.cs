using AutoMapper;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.BikeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BikeForSell.Web.Controllers
{
    [Authorize]
    public class BikeController : Controller
    {
        private readonly IBikeService _bikeService;
        private readonly IUserService _userService;

        public BikeController(IBikeService bikeService, IUserService userService)
        {
            _bikeService = bikeService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = _bikeService.GetBikeList();

            return View(models);
        }

        [HttpPost]
        public IActionResult Index(string searchString, int prizeFrom, int prizeTo, string type, int filter)
        {
            var models = _bikeService.GetBikeList(searchString, prizeFrom, prizeTo, type, filter);

            return View(models);
        }

        [HttpGet]        
        public IActionResult DetailsForIndex(int id)
        {
            var model = _bikeService.GetBikeDetails(id);
            model.CurrentUserId = _userService.GetUserId();
            return View(model);
        }

        [HttpGet]
        public IActionResult DetailsForPurchase(int id)
        {
            var model = _bikeService.GetBikeDetails(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult YourBikes()
        {
            string id = _userService.GetUserId();
            var model = _bikeService.GetYourBikesList(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new NewBikeVm();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(NewBikeVm newBike)
        {
            string id = _userService.GetUserId();            
            _bikeService.Add(newBike, id);

            return RedirectToAction("YourBikes");
        }

        public IActionResult ChangeStatus(int id)
        {
            _bikeService.ChangeStatus(id);
            return RedirectToAction("YourBikes");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _bikeService.GetBikeForEdit(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(BikeForEditVm bike)
        {
            _bikeService.EditBike(bike);
            return RedirectToAction("YourBikes");
        }

        public IActionResult Delete(int id)
        {
            _bikeService.DeleteBike(id);
            return RedirectToAction("YourBikes");
        }

        public IActionResult Buy(int id)
        {
            string userId = _userService.GetUserId(); 
            _bikeService.BuyBike(id, userId);

            return RedirectToAction("Purchase", "Profile");
        }
    }
}
