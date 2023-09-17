using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.Admin.Models.ViewModels
{
    public class AdminVM
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
        public Company? Company { get; set; }
        public int? CompanyID { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; }
    }
}
