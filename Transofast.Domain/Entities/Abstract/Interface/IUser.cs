using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transofast.Domain.Entities.Abstract.Interface
{
    public interface IUser : IBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
    }
}
