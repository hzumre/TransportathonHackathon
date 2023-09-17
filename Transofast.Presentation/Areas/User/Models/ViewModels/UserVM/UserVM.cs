using System.ComponentModel.DataAnnotations;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.UserVM
{
    public class UserVM
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresini giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numarasını giriniz")]
        public string PhoneNumber { get; set; }
        public int? CompanyID { get; set; }
        public Guid AppUserID { get; set; }
        public Company? Company { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
