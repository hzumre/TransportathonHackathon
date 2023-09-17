using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Transofast.Application.Services.CommentService;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.Admin.Models.ViewModels;

namespace Transofast.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;
        //private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _commentService = commentService;
            _mapper = mapper;
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

            var listDTO = await _commentService.GetDefaults(x => x.CompanyID == appUser.CompanyID);
            var listVM = _mapper.Map<List<CommentListVM>>(listDTO);
   
            return View(listVM);
        }
    }
}
