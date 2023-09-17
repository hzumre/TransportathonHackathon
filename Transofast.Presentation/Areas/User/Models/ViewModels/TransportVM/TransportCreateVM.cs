using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM
{
    public class TransportCreateVM
    {
        public string? Description { get; set; }
        public TransportType? TransportType { get; set; }
        public TransportStatus? TransportStatus { get; set; }
        public DateTime TransportDate { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal? Price { get; set; }
        public List<CompanyListDTO>? CompanyList { get; set; }
        public int CompanyID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } 
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
    }
}
