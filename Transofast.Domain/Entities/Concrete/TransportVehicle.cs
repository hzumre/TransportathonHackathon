using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Abstract.Interface;
using Transofast.Domain.Enum;

namespace Transofast.Domain.Entities.Concrete
{
    public class TransportVehicle : IBaseEntity, IEntity<int>
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
        public string Plaka { get; set; }
        public VehicleType VehicleType { get; set; }


        //Navigation Properties
        public Company Company { get; set; }
        public int CompanyID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }


    }
}
