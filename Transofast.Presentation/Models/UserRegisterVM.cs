using System.ComponentModel.DataAnnotations;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Presentation.Models
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen mail adresini giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parolayı Boş Geçemezsiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
        [Compare("Password", ErrorMessage = "şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }

        //public Guid AppUserID { get; set; }
        //public AppUser AppUser { get; set; }
    }
}
