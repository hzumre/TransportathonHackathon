using System.ComponentModel.DataAnnotations;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Presentation.Areas.Admin.Models.ViewModels
{
    public class AdminRegisterVM
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
        public int? CompanyID { get; set; }
        public string PhoneNumber { get; set; }
    }
}
