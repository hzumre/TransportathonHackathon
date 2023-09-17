using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Application.Services.CommentService;
using Transofast.Application.Services.CompanyService;
using Transofast.Application.Services.MessageService;
using Transofast.Application.Services.TrasportService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM;

namespace Transofast.Presentation.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "user")]
    public class TransportController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITransportService _transportService;
        private readonly ICommentService _commentService;
        private readonly ICompanyService _companyService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public TransportController(ITransportService transportService, IMapper mapper, ICompanyService companyService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMessageService messageService, ICommentService commentService)
        {
            _transportService = transportService;
            _mapper = mapper;
            _companyService = companyService;
            _userManager = userManager;
            _signInManager = signInManager;
            _messageService = messageService;
            _commentService = commentService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim, out Guid userId);

            var listDTO = await _transportService.GetDefaults(x => x.AppUserID == userId && (x.TransportStatus == Domain.Enum.TransportStatus.Pending || x.TransportStatus == Domain.Enum.TransportStatus.Company_Confirm || x.TransportStatus == Domain.Enum.TransportStatus.Company_Reject));
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);


            return View(listVM);
        }
        public async Task<IActionResult> ActiveList()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim, out Guid userId);

            var listDTO = await _transportService.GetDefaults(x => x.AppUserID == userId && x.Status == Domain.Enum.Status.Active);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);


            return View(listVM);
        }
        public async Task<IActionResult> DeActiveList()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim, out Guid userId);

            var listDTO = await _transportService.GetDefaults(x => x.AppUserID == userId && x.Status == Domain.Enum.Status.Inactive);
            var listVM = _mapper.Map<List<TransportListVM>>(listDTO);


            return View(listVM);
        }
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                var dto = _mapper.Map<TransportUpdateDTO>(vm);
                dto.TransportStatus = Domain.Enum.TransportStatus.User_Reject;
                await _transportService.Update(dto);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("List");
            }
        }
        public async Task<IActionResult> Comfirm(int id)
        {
            try
            {
                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(await _transportService.GetById(id));
                var dto = _mapper.Map<TransportUpdateDTO>(vm);
                dto.TransportStatus = Domain.Enum.TransportStatus.User_Confirm;
                dto.Status = Domain.Enum.Status.Active;
                await _transportService.Update(dto);
                return RedirectToAction("ActiveList");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("ActiveList");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            TransportCreateVM transportCreateVM = new TransportCreateVM();
            transportCreateVM.CompanyList = await _companyService.All();


            return View(transportCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransportCreateVM vm)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdClaim, out Guid userId);
            vm.AppUserID = userId;

            vm.TransportStatus = Domain.Enum.TransportStatus.Pending;

            try
            {
                var dto = _mapper.Map<TransportCreateDTO>(vm);
                await _transportService.Create(dto);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessageAsync(int id)
        {
            var transport = await _transportService.GetById(id);
            if (transport == null)
            {
                ModelState.AddModelError("", "An error occurred. Please try again.");
                return RedirectToAction("ActiveList");
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser appUser = await _userManager.FindByIdAsync(userIdClaim);

            if (transport.EmployeeID == Guid.Empty || transport.EmployeeID == null)
            {
                ModelState.AddModelError("", "An error occurred. Please try again.");
                return RedirectToAction("ActiveList");
            }
            AppUser empUser = await _userManager.FindByIdAsync(transport.EmployeeID.ToString());

            ViewBag.Sender = appUser?.UserName;
            ViewBag.Receiver = empUser?.UserName;
            ViewBag.UserId = appUser?.Id.ToString();
            ViewBag.ReceiverId = transport.EmployeeID.ToString();
            return View();
        }

        [HttpGet]
        public IActionResult Rate(int id)
        {
            ViewBag.RelatedTransport = id;
            return View("RateAsync");
        }

        [HttpPost]
        public async Task<IActionResult> RateAsync([FromBody] RatingCommentVM model)
        {
            var transport = await _transportService.GetById(model.TransportId);
            if (transport.Rate == null)
            {
                TransportUpdateVM vm = _mapper.Map<TransportUpdateVM>(transport);
                vm.Rate = model.Rating;
                var dto = _mapper.Map<TransportUpdateDTO>(vm);
                await _transportService.Update(dto);

                var comment = new CommentCreateDTO()
                {
                    AppUserID = vm.AppUserID,
                    SendComment = model.Comment,
                    Score = model.Rating,
                    Status = Domain.Enum.Status.Inactive,
                    CompanyID = vm.CompanyID
                };
                await _commentService.Create(comment);
            }

            return Json(new { success = true });
        }

    }
}
