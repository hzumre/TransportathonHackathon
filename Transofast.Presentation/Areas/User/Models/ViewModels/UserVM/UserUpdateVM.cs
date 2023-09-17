using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.UserVM
{
    public class UserUpdateVM
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int? CompanyID { get; set; }
        public Guid AppUserID { get; set; }
        public Company? Company { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
