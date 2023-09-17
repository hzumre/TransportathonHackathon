using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Transofast.Application.DTOs.UserDTOs;
using Transofast.Application.Services.AppUserService;
using Transofast.Application.Services.CompanyService;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM;
using Transofast.Presentation.Areas.User.Models.ViewModels.UserVM;

namespace Transofast.Presentation.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        IWebHostEnvironment _webHostEnvironment;
        readonly private ICompanyService _companyService;

        public UserController(IAppUserService appUserService, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment, ICompanyService companyService)

        {
            _appUserService = appUserService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            TransportCreateVM transportCreateVM = new TransportCreateVM();
            transportCreateVM.CompanyList = await _companyService.All();


            return View(transportCreateVM);
        }

        [HttpGet]
        public async Task<IActionResult> ProfileDetails()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {

                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);
                var dto = _mapper.Map<UserUpdateDTO>(appUser);
                var vm = _mapper.Map<UserUpdateVM>(dto);
                vm.AppUserID = appUser.Id;


                return View(vm);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(UserUpdateVM vm)
        {

            var eDTO = _mapper.Map<UserUpdateDTO>(vm);
            AppUser appUser = _mapper.Map<AppUser>(eDTO);
            appUser.Id = vm.AppUserID;
            appUser.SecurityStamp = Guid.NewGuid().ToString();
            await _userManager.UpdateAsync(appUser);         
            return RedirectToAction("Index", "User");

        }
    }
}
