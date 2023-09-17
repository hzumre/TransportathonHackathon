using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.MessageVM
{
    public class MessageListVM
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }
        public int ID { get; set; }
        public string SendMessage { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }
    }
}
