using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Application.DTOs.UserDTOs;
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
    public class AdminController : Controller
    {
        readonly private IAppUserService _appUserService;
        readonly private IAppRoleService _appRoleService;
        readonly private IMapper _mapper;
        readonly private ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public AdminController(IAppUserService appUserService, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICompanyService companyService, IAppRoleService appRoleService)

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


        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> SignUp()
        {
            AdminRegisterVM adminRegisterVM = new AdminRegisterVM();
            adminRegisterVM.CompanyList = await _companyService.All();
            if (User.Identity.IsAuthenticated)//Kullanıcı giriş yapmışsa anasayfaya yönlendirilsin.
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(adminRegisterVM);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignUp(AdminRegisterVM vm)
        {

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterDTO>(vm);
                AppUser appUser = _mapper.Map<AppUser>(user);
                appUser.CompanyID = vm.CompanyID;
                IdentityResult result = await _userManager.CreateAsync(appUser, vm.Password);
    
                await _appRoleService.RoleAssign(appUser.Id, "admin");

                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn", "Login", new { area = "" });
                }
                foreach (var item in result.Errors)
                {

                    ModelState.AddModelError(item.Code, item.Description);
                    TempData["error"] = "Kayıt Oluşturulurken Bir Hata Meydana Geldi";
                }
            }

            return View(vm);
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

                List<CompanyListDTO> company = await _companyService.GetDefaults(x => x.ID== vm.CompanyID);

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
