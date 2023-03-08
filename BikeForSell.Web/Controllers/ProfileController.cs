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
            string userId = _userService.GetUserId();
            var purchase = _profileService.GetListPurchases(userId);
            return View(purchase);
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult DeletePurchase(int purchaseId)
        {
            _profileService.DeletePurchase(purchaseId);
            return RedirectToAction("Purchase");
        }

        [HttpGet]
        [Authorize(Roles = "Allowed")]
        public IActionResult Sales()
        {
            string userId = _userService.GetUserId();
            var sales = _profileService.GetListSales(userId);
            return View(sales);
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult ChangeState(int saleId, Decision salesmanDecision)
        {
            _profileService.ChangeState(saleId, salesmanDecision);
            return RedirectToAction("Sales");
        }

        [Authorize(Roles = "Allowed")]
        public IActionResult DeleteSale(int saleId)
        {
            _profileService.DeleteSale(saleId);
            return RedirectToAction("Sales");
        }

        [Authorize(Roles = "NotAllowed, Allowed")]        
        public IActionResult CheckUserDetalInformation()
        {
            string userId = _userService.GetUserId();

            if (_profileService.UserDetalInformation(userId))
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
            var detalInformation = new DetalInformationVm();
            return View(detalInformation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDetalInformation(DetalInformationVm userDetalInformation)
        {
            if(ModelState.IsValid)
            {
                userDetalInformation.Id = _userService.GetUserId();
                _profileService.AddDetalInformation(userDetalInformation);             
                return RedirectToAction("SuccessfullyAddedInformation");
            }
            else
            {
                return View(userDetalInformation);
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
        public IActionResult EditDetalInformation(EditDetalInformationVm userDetalInformation)
        {
            if (ModelState.IsValid)
            {
                _profileService.EditDetalInformation(userDetalInformation);
                return RedirectToAction("Index");
            }
            else
            {
                return View(userDetalInformation);
            }            
        }
    }
}
