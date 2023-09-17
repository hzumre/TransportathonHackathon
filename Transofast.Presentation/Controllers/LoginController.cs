using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.UserDTOs;
using Transofast.Application.Services.AppRoleService;
using Transofast.Application.Services.AppUserService;
using Transofast.Application.Services.CompanyService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Models;

namespace Transofast.Presentation.Controllers
{
    public class LoginController : Controller
    {

        readonly private IAppUserService _appUserService;
        readonly private IAppRoleService _appRoleService;
        readonly private IMapper _mapper;
        readonly private ICompanyService _companyService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;


        public LoginController(IAppUserService appUserService, IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICompanyService companyService, IAppRoleService appRoleService)

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
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignIn(UserSignInVM vm)
        {
            if (ModelState.IsValid)
            {
                var _loginDto = _mapper.Map<LoginDTO>(vm);
                var result = await _appUserService.Login(_loginDto);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(vm.username);
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("admin"))
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });

                    }
                    else if (roles.Contains("employee"))
                    {
                        return RedirectToAction("Index", "Employee", new { area = "Employee" });
                        //return RedirectToRoute(new { controller = "Home", action = "Index", area="CompanyManager" });



                    }
                    else if (roles.Contains("user"))
                    {
                        return RedirectToAction("Index", "User", new { area = "User" });
                    }
                    else
                    {
                        TempData["error"] = "Login failed.";
                        return View(vm);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Add a model error for display
                    return View(vm);
                }
            }

            TempData["error"] = "Login failed.";
            return View(vm);
        }


        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet, AllowAnonymous]//Bu attribute sayesinde ilgili action methodun Authorize kapsamından çıkmasını istiyoruz. Neden? Çünkü kullanıcı herhangi bir kimlik doğrulamasından yani authentication işleminden geçmeden Register sayfasını görebilmeili ve sisteme register olabilmelidir.
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)//Kullanıcı giriş yapmışsa anasayfaya yönlendirilsin.
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SignUp(UserRegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterDTO>(vm);
                AppUser appUser = _mapper.Map<AppUser>(user);
                IdentityResult result = await _userManager.CreateAsync(appUser, vm.Password);
                //var result = await _appUserService.Register(user, appUser);

                await _appRoleService.RoleAssign(appUser.Id, "user");

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

    }

}
