using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;
using Transofast.Infrastructure.DataAccess;

namespace Transofast.Infrastructure.Repository
{
    public class TransportVehicleRepository : BaseRepository<TransportVehicle>,ITransportVehicleRepository
    {
        public TransportVehicleRepository(TransofastDbContext transofastDb) : base(transofastDb) { }
    }
}
