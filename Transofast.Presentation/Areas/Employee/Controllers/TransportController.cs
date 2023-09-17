using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Application.Services.CompanyService;
using Transofast.Application.Services.MessageService;
using Transofast.Application.Services.TrasportService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM;

namespace Transofast.Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "employee")]
    public class TransportController : Controller
    {

        private readonly ITransportService _transportService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public TransportController(ITransportService transportService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _transportService = transportService;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var employeeIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(employeeIdClaim, out Guid employeeId);

            var listDTO = await _transportService.GetDefaults(x => x.EmployeeID == employeeId && x.Status == Domain.Enum.Status.Active);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);


            return View(listVM);
        }

        public async Task<IActionResult> Completed(int id)
        {
            try
            {
                var employeeIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Guid.TryParse(employeeIdClaim, out Guid employeeId);

                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                vm.EmployeeID = employeeId;
                var dto = _mapper.Map<TransportUpdateDTO>(vm);
                dto.Status = Domain.Enum.Status.Inactive;
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
        public async Task<IActionResult> SendMessageAsync(int id)
        {
            var transport = await _transportService.GetById(id);
            if (transport == null)
            {
                ModelState.AddModelError("", "An error occurred. Please try again.");
                return RedirectToAction("List");
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            if (transport.AppUserID == Guid.Empty)
            {
                ModelState.AddModelError("", "An error occurred. Please try again.");
                return RedirectToAction("List");
            }
            AppUser custUser = await _userManager.FindByIdAsync(transport.AppUserID.ToString());

            ViewBag.Sender = appUser?.UserName;
            ViewBag.Receiver = custUser?.UserName;
            ViewBag.UserId = appUser?.Id.ToString();
            ViewBag.ReceiverId = transport?.AppUserID.ToString();
            return View();
        }
    }
}
