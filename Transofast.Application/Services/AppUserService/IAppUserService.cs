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
    public interface IAppUserService
    {
        Task<SignInResult> Login(LoginDTO user);
        Task LogOut();
        Task<IdentityResult> Register(RegisterDTO registerDTO, AppUser appUser);
        
    }
}
