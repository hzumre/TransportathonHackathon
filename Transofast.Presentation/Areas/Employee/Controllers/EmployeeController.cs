using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Application.DTOs.UserDTOs;
using Transofast.Application.Services.AppRoleService;
using Transofast.Application.Services.AppUserService;
using Transofast.Application.Services.CompanyService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.User.Models.ViewModels.UserVM;

namespace Transofast.Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "employee")]
    public class EmployeeController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private IAppRoleService _appRoleService;
        readonly private IMapper _mapper;
        readonly private ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public EmployeeController(IAppUserService appUserService, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICompanyService companyService, IAppRoleService appRoleService)

        {
            _appUserService = appUserService;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _companyService = companyService;
            _appRoleService = appRoleService;
        }
        public IActionResult Index()
        {
            return View();
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

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.ID == vm.CompanyID);

                TempData["companyName"] = company.FirstOrDefault().Name;


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
