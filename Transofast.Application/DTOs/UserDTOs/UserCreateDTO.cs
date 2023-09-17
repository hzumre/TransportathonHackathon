using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Application.DTOs.UserDTOs
{
    public class UserCreateDTO
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

        //Navigation Properties
        public List<Transport> Transports { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Message> Messages { get; set; }
    }
}
