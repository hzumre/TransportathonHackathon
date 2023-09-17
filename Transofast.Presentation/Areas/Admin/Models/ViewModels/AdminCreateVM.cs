using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.Admin.Models.ViewModels
{
    public class AdminCreateVM
    {
        //IBaseEntity
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }

        //IUser
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }

        //Additional Properties
        public string? Picture { get; set; }
        public string? Phone { get; set; }

        //Navigation Properties
        public Company Company { get; set; }
        public int CompanyID { get; set; }
    }
}
