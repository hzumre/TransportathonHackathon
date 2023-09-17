using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM
{
    public class TransportUpdateVM
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
        public int ID { get; set; }
        public string? Description { get; set; }
        public TransportType? TransportType { get; set; }
        public TransportStatus? TransportStatus { get; set; }
        public DateTime TransportDate { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal? Price { get; set; }
        public List<CompanyListDTO>? CompanyList { get; set; }
        public List<AppUser>? EmployeeList { get; set; }
        public int CompanyID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }
        public Guid? EmployeeID { get; set; }
        public int? Rate { get; set; }
        public Comment? Comment { get; set; }
        public int? CommentID { get; set; }
    }
}
