using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.Enum;

namespace Transofast.Application.DTOs.CommentDTOs
{
    public class CommentUpdateDTO
    {
        //IBaseEntity
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }


        //IEntity
        public int ID { get; set; }

        //Additional Properties
        public string SendComment { get; set; }
        public int Score { get; set; }

        //Navigation Properties
        public Company Company { get; set; }
        public int CompanyID { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserID { get; set; }
    }
}
