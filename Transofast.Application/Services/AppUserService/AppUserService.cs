using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.AppUserService
{
    public class AppUserService: IAppUserService
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IMapper _mapper;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
 

        public async Task<SignInResult> Login(LoginDTO user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO, AppUser appUser)
        {
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
            }

            return result;
        }
    }
}
