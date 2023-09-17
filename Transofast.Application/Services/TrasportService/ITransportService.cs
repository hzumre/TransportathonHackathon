using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.TrasportService
{
    public interface ITransportService : IBaseService<TransportCreateDTO, TransportUpdateDTO, TransportListDTO, TransportDTO, Transport, int>
    {
    }
}
