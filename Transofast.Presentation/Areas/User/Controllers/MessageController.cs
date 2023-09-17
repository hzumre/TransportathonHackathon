using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Transofast.Application.DTOs.MessageDTOs;
using Transofast.Application.Services.CompanyService;
using Transofast.Application.Services.MessageService;
using Transofast.Application.Services.TrasportService;
using Transofast.Presentation.Areas.User.Models.ViewModels.MessageVM;

namespace Transofast.Presentation.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "user")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper, ICompanyService companyService)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var listDTO = await _messageService.All();
            var listVM = _mapper.Map<List<MessageListVM>>(listDTO);

            return View(listVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MessageCreateVM createVM = new MessageCreateVM();
            return View(createVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageCreateVM vm)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var dto = _mapper.Map<MessageCreateDTO>(vm);
                    await _messageService.Create(dto);
                    return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

            }
            return View(vm);
        }
    }
}
