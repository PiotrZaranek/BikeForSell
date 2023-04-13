using AutoMapper;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using BikeForSell.Application.ViewModels.BikeVm;
using BikeForSell.Domain.Exceptions;
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
        private readonly IErrorService _errorService;

        public BikeController(IBikeService bikeService, IUserService userService, IProfileService profileService, IErrorService errorService)
        {
            _bikeService = bikeService;
            _userService = userService;
            _profileService = profileService;
            _errorService = errorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ListBikeForListVm bikeList;
            try
            {
                bikeList = _bikeService.GetBikeList();
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "Index", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            
            return View(bikeList);
        }

        [HttpPost]
        public IActionResult Index(ListBikeForListVm model)
        {
            ListBikeForListVm bikeList;

            try
            {
                bikeList = _bikeService.GetBikeList(model.BikeFilterParameters);
            }
            catch (Exception ex) 
            {
                _errorService.LogError(ex, "Bike", "Index", _userService.GetUserId());
                return RedirectToAction("Error");
            }

            return View(bikeList);
        }

        [HttpGet]        
        public IActionResult DetailsForIndex(int bikeId)
        {
            BikeForDetailsVm bikeDetails;

            try
            {
                bikeDetails = _bikeService.GetBikeDetails(bikeId);
                bikeDetails.CurrentUserId = _userService.GetUserId();
            }            
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "DetailsForIndex", _userService.GetUserId());
                return RedirectToAction("Error");
            }

            return View(bikeDetails);
        }

        [HttpGet]
        public IActionResult DetailsForPurchase(int bikeId)
        {
            BikeForDetailsVm bikeDetails;

            try
            {
                bikeDetails = _bikeService.GetBikeDetails(bikeId);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "DetailsForPurchase", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            
            return View(bikeDetails);
        }

        [HttpGet]
        public IActionResult YourBikes()
        {
            ListBiekForYourBikes yourBikes;
            try
            {
                string userId = _userService.GetUserId();
                yourBikes = _bikeService.GetYourBikesList(userId);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "Index", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            
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
            
            try
            {
                _bikeService.AddBike(newBikeVm, user);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "Add", _userService.GetUserId());
                return RedirectToAction("Error");
            }            

            return RedirectToAction("YourBikes");
        }

        public IActionResult ChangeStatus(int bikeId)
        {
            try 
            {
                _bikeService.ChangeStatus(bikeId);
            }            
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "ChangeStatus", _userService.GetUserId());
                return RedirectToAction("Error");
            }

            return RedirectToAction("YourBikes");
        }

        [HttpGet]
        public IActionResult Edit(int bikeId)
        {
            BikeForEditVm bikeEdit;

            try
            {
                bikeEdit = _bikeService.GetBikeForEdit(bikeId);
            }
            catch (Exception ex) 
            {
                _errorService.LogError(ex, "Bike", "Edit", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            
            return View(bikeEdit);
        }

        [HttpPost]
        public IActionResult Edit(BikeForEditVm bike)
        {
            try
            {
                _bikeService.EditBike(bike);
            }
            catch (Exception ex) 
            {
                _errorService.LogError(ex, "Bike", "Edit", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            
            return RedirectToAction("YourBikes");
        }

        public IActionResult Delete(int bikeId)
        {
            try
            {
                _bikeService.DeleteBike(bikeId);
            }            
            catch (Exception ex) 
            {
                _errorService.LogError(ex, "Bike", "Delete", _userService.GetUserId());
                return RedirectToAction("Error");
            }

            return RedirectToAction("YourBikes");
        }

        public IActionResult Buy(int bikeId)
        {
            string userId = _userService.GetUserId(); 

            try
            {
                _bikeService.BuyBike(bikeId, userId);
            }
            catch (Exception ex)
            {
                _errorService.LogError(ex, "Bike", "Buy", _userService.GetUserId());
                return RedirectToAction("Error");
            }
            

            return RedirectToAction("Purchase", "Profile");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
