using AutoMapper;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.ViewModels.BikeVm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BikeForSell.Web.Controllers
{
    [Authorize(Roles = "Allowed")]
    public class BikeController : Controller
    {
        private readonly IBikeService _bikeService;
        private readonly IProfileService _profileService;
        private readonly IUserService _userService;

        public BikeController(IBikeService bikeService, IUserService userService, IProfileService profileService)
        {
            _bikeService = bikeService;
            _userService = userService;
            _profileService = profileService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bikeList = _bikeService.GetBikeList();

            return View(bikeList);
        }

        [HttpPost]
        public IActionResult Index(ListBikeForListVm model)
        {            
            var bikeList = _bikeService.GetBikeList(model.BikeFilterParameters);

            return View(bikeList);
        }

        [HttpGet]        
        public IActionResult DetailsForIndex(int bikeId)
        {
            var bikeDetails = _bikeService.GetBikeDetails(bikeId);
            bikeDetails.CurrentUserId = _userService.GetUserId();            
            return View(bikeDetails);
        }

        [HttpGet]
        public IActionResult DetailsForPurchase(int bikeId)
        {
            var bikeDetails = _bikeService.GetBikeDetails(bikeId);
            return View(bikeDetails);
        }

        [HttpGet]
        public IActionResult YourBikes()
        {
            string userId = _userService.GetUserId();
            var yourBikes = _bikeService.GetYourBikesList(userId);
            return View(yourBikes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var newBike = new NewBikeVm();
            return View(newBike);
        }

        [HttpPost]
        public IActionResult Add(NewBikeVm newBikeVm)
        {
            var user = _profileService.GetUser(_userService.GetUserId());        
            _bikeService.AddBike(newBikeVm, user);

            return RedirectToAction("YourBikes");
        }

        public IActionResult ChangeStatus(int bikeId)
        {
            _bikeService.ChangeStatus(bikeId);
            return RedirectToAction("YourBikes");
        }

        [HttpGet]
        public IActionResult Edit(int bikeId)
        {
            var bikeEdit = _bikeService.GetBikeForEdit(bikeId);
            return View(bikeEdit);
        }

        [HttpPost]
        public IActionResult Edit(BikeForEditVm bike)
        {
            _bikeService.EditBike(bike);
            return RedirectToAction("YourBikes");
        }

        public IActionResult Delete(int bikeId)
        {
            _bikeService.DeleteBike(bikeId);
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
