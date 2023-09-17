using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.AppRoleDTOs;

namespace Transofast.Application.Services.AppRoleService
{
    public interface IAppRoleService
    {
        Task Create(AppRoleCreateDTO appRoleCreateDTO);
        // Task Create(AppRoleCReateDTO appRoleCreateDTO);
        Task Edit(AppRoleUpdateDTO appRoleUpdateDTO);
        Task Remove(Guid id);
        Task<List<AppRoleListDTO>> AllRoles();
        Task<AppRoleUpdateDTO> GetById(Guid id);
        Task<bool> IsRoleExists(string approleName);

        Task RoleAssign(Guid id, string roleName);
    }
}
