using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.TransportVehicleDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.TransportVehicleService
{
    public interface ITransportVehicleService : IBaseService<TransportVehicleCreateDTO, TransportVehicleUpdateDTO, TransportVehicleListDTO, TransportVehicleDTO, TransportVehicle, int>
    {
    }
}
