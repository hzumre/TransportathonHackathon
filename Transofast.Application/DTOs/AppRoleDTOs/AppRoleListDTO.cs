using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transofast.Application.DTOs.AppRoleDTOs
{
    public class AppRoleListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasAssign { get; set; }
    }
}
