using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Enum;

namespace Transofast.Domain.Entities.Concrete
{
    public class AppUser : IdentityUser<Guid>,IBaseEntity
    {

        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }
        public Company Company { get; set; }
        public int? CompanyID { get; set; }

        //Navigation Properties
        public List<Transport> Transports { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Message> Messages { get; set; }

    }
}
