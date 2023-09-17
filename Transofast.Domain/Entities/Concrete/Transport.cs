using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Enum;

namespace Transofast.Domain.Entities.Concrete
{
    public class Transport : IBaseEntity, IEntity<int>
    {
        //IBaseEntity
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status? Status { get; set; }

        //IEntity
        public int ID { get; set; }

        //Additional Properties
        public string? Description { get; set; }
        public TransportType? TransportType { get; set; }
        public TransportStatus? TransportStatus { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public DateTime TransportDate { get; set; }
        public decimal? Price { get; set; }
        public int? Rate { get; set; }

        //Navigation Properties
        public Company Company { get; set; }
        public int CompanyID { get; set; }
        public Comment Comment { get; set; }
        public int? CommentID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }
        public Guid? EmployeeID { get; set; }

    }
}
