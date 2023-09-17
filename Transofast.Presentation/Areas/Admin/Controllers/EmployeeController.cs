using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Application.Services.AppRoleService;
using Transofast.Application.Services.AppUserService;
using Transofast.Application.Services.CompanyService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.Admin.Models.ViewModels;
using Transofast.Presentation.Areas.User.Models.ViewModels.UserVM;

namespace Transofast.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class EmployeeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        readonly private IAppRoleService _appRoleService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public EmployeeController(UserManager<AppUser> userManager, IAppUserService appUserService, SignInManager<AppUser> signInManager, IMapper mapper, IAppRoleService appRoleService, ICompanyService companyService)
        {
            _userManager = userManager;
            _appUserService = appUserService;
            _signInManager = signInManager;
            _mapper = mapper;
            _appRoleService = appRoleService;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAlEmployees()
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync("employee");
            var listEmployee = _mapper.Map<List<UserListVM>>(usersInRole);
            return View(listEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);


                TempData["companyID"] = appUser.CompanyID;

                    List<CompanyListDTO> company = await _companyService.GetDefaults(x=>x.ID==(int)(appUser.CompanyID));
  
                

                TempData["companyName"] = company.FirstOrDefault().Name;

                return View();
            }

            else
            {
                return View();
            }


        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM vm)
        {
            
                try
                {
                    var user = _mapper.Map<RegisterDTO>(vm);
                    AppUser appUser = _mapper.Map<AppUser>(user);
                    //appUser.CompanyID = vm.CompanyID;
                    IdentityResult result = await _userManager.CreateAsync(appUser, vm.Password);

                    await _appRoleService.RoleAssign(appUser.Id, "employee");

                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAlEmployees");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            

            return View(vm);
        }

    }
}
