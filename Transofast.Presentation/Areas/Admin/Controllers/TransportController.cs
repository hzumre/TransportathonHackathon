using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Application.Services.CompanyService;
using Transofast.Application.Services.TrasportService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.Admin.Models.ViewModels;
using Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM;

namespace Transofast.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class TransportController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransportService _transportService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public TransportController(ITransportService transportService, IMapper mapper, ICompanyService companyService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _transportService = transportService;
            _mapper = mapper;
            _companyService = companyService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            var listDTO = await _transportService.GetDefaults(x => x.CompanyID == appUser.CompanyID && (x.TransportStatus == Domain.Enum.TransportStatus.Pending||x.TransportStatus== Domain.Enum.TransportStatus.Company_Confirm));
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);


            return View(listVM);
        }
        public async Task<IActionResult> ActiveList()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            var listDTO = await _transportService.GetDefaults(x => x.CompanyID == appUser.CompanyID && x.Status== Domain.Enum.Status.Active);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);

            return View(listVM);
        }

        public async Task<IActionResult> DeActiveList()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            var listDTO = await _transportService.GetDefaults(x => x.CompanyID == appUser.CompanyID && x.Status == Domain.Enum.Status.Inactive);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);

            return View(listVM);
        }


        public async Task<IActionResult> ConfirmTransportList()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            var listDTO = await _transportService.GetDefaults(x => x.CompanyID == appUser.CompanyID && x.TransportStatus != Domain.Enum.TransportStatus.Company_Reject);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);

            return View(listVM);
        }
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                var dto = _mapper.Map<TransportUpdateDTO>(vm);
                dto.TransportStatus = Domain.Enum.TransportStatus.Company_Reject;
                await _transportService.Update(dto);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                vm.CompanyList = await _companyService.All();


                return View(vm);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransportUpdateVM vm)
        {
            
            vm.TransportStatus = Domain.Enum.TransportStatus.Company_Confirm;
            var dto = _mapper.Map<TransportUpdateDTO>(vm);
            await _transportService.Update(dto);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                var usersInRole = await _userManager.GetUsersInRoleAsync("employee");
      
                    vm.EmployeeList = usersInRole.Where(x=>x.CompanyID==appUser.CompanyID).ToList();


                return View(vm);

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(TransportUpdateVM vm)
        {
            vm.TransportStatus = Domain.Enum.TransportStatus.Company_Confirm;
            vm.Status = Domain.Enum.Status.Active;
            var dto = _mapper.Map<TransportUpdateDTO>(vm);
            await _transportService.Update(dto);
            return RedirectToAction("ActiveList");
        }

    }
}
