using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Application.DTOs.CompanyAdminDTOs
{
    public class CompanyAdminCreateDTO
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresini giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }
        public List<CompanyListDTO>? CompanyList { get; set; }
        public Company? CompanyId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
