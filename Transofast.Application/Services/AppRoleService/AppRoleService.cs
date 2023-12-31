﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.AppRoleDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.AppRoleService
{
    public class AppRoleService : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AppRoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<AppRoleListDTO>> AllRoles()
        {
            var roleList = _roleManager.Roles.ToList();
            return _mapper.Map<List<AppRoleListDTO>>(roleList);
        }

        public async Task Create(AppRoleCreateDTO appRoleCreateDTO)
        //public async Task Create(AppRoleCReateDTO appRoleCreateDTO)
        {
            AppRole appRole = _mapper.Map<AppRole>(appRoleCreateDTO);
            await _roleManager.CreateAsync(appRole);

        }

        public async Task Edit(AppRoleUpdateDTO appRoleUpdateDTO)
        {
            var result = await _roleManager.UpdateAsync(_mapper.Map<AppRole>(appRoleUpdateDTO));
        }

        public async Task<AppRoleUpdateDTO> GetById(Guid id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id.ToString());
            return _mapper.Map<AppRoleUpdateDTO>(role);
        }



        public async Task<bool> IsRoleExists(string approleName)
        {
            var result = await _roleManager.FindByNameAsync(approleName);
            if (result != null)
            {
                return true;
            }

            return false;
        }

        public async Task Remove(Guid id)
        {
            IdentityResult result = await _roleManager.DeleteAsync(await _roleManager.FindByIdAsync(id.ToString()));
        }

        public async Task RoleAssign(Guid id,string roleName)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id.ToString());
                await _userManager.AddToRoleAsync(appUser, roleName);

        }

    }
}
