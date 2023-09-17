using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Enum;

namespace Transofast.Domain.Entities.Concrete
{
    public class Company : IBaseEntity, IEntity<int>
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
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double? AverageScore { get; set; }

        public Dictionary<TransportType, bool> SupportedTransportTypes;

        //Navigation Properties

        public List<Transport>? Transports { get; set; }
        public List<TransportVehicle>? TransportVehicles { get; set; }
        public List<AppUser>? AppUsers { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
