﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Transofast.Application.DTOs.AppRoleDTOs
{
    public class AppRoleCreateDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Rolün belirtilmesi zorunludur.")]
        [Display(Name = "Rol:")]
        public string Name { get; set; }
    }
}
