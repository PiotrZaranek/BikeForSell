using AutoMapper.Configuration.Conventions;
using BikeForSell.Domain.Enums;
using BikeForSell.Application.Interfaces;
using BikeForSell.Application.Services;
using BikeForSell.Application.ViewModels.ProfileVm;
using BikeForSell.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BikeForSell.Web.Controllers
{
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
        [Authorize(Roles = "Allowed")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Allowed")]
        public IActionResult Purchase()
        {
            string id = _userService.GetUserId();
            var model = _profileService.GetListPurchases(id);
            return View(model);
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult DeletePurchase(int id)
        {
            _profileService.DeletePurchase(id);
            return RedirectToAction("Purchase");
        }

        [HttpGet]
        [Authorize(Roles = "Allowed")]
        public IActionResult Sales()
        {
            string id = _userService.GetUserId();
            var model = _profileService.GetListSales(id);
            return View(model);
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult ChangeState(int saleId, Decision decision)
        {
            _profileService.ChangeState(saleId, decision);
            return RedirectToAction("Sales");
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult DeleteSale(int id)
        {
            _profileService.DeleteSale(id);
            return RedirectToAction("Sales");
        }

        [Authorize(Roles = "NotAllowed, Allowed")]        
        public IActionResult CheckUserDetalInformation()
        {
            string id = _userService.GetUserId();

            if (_profileService.UserDetalInformation(id))
            {
                return RedirectToAction("Index", "Bike");
            }
            else
            {
                return RedirectToAction("AddDetalInformation");
            }            
        }

        [HttpGet]
        [Authorize( Roles = "NotAllowed")]
        public IActionResult AddDetalInformation()
        {
            var model = new DetalInformationVm();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDetalInformation(DetalInformationVm model)
        {
            if(ModelState.IsValid)
            {
                model.Id = _userService.GetUserId();
                _profileService.AddDetalInformation(model);             
                return RedirectToAction("SuccessfullyAddedInformation");
            }
            else
            {
                return View(model);
            }            
        }

        [Authorize(Roles = "NotAllowed")]
        public IActionResult SuccessfullyAddedInformation()
        {
            _userService.LogOutUser();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Allowed")]
        public IActionResult EditDetalInformation()
        {
            var model = new EditDetalInformationVm();
            model.Id = _userService.GetUserId();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Allowed")]
        public IActionResult EditDetalInformation(EditDetalInformationVm model)
        {
            if (ModelState.IsValid)
            {
                _profileService.EditDetalInformation(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }            
        }
    }
}
