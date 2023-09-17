using Transofast.Domain.Entities.Concrete;

namespace Transofast.Presentation.Areas.User.Models.ViewModels.MessageVM
{
    public class MessageCreateVM
    {
        public string SendMessage { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }
    }
}
