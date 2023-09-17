using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.TransportVehicleDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;

namespace Transofast.Application.Services.TransportVehicleService
{
    public class TransportVehicleService : ITransportVehicleService
    {
        private readonly ITransportVehicleRepository _transportVehicleRepository;
        private readonly IMapper _mapper;

        public TransportVehicleService(ITransportVehicleRepository transportVehicleRepository, IMapper mapper)
        {
            _transportVehicleRepository = transportVehicleRepository;
            _mapper = mapper;
        }

        public async Task<List<TransportVehicleListDTO>> All()
        {
            return _mapper.Map<List<TransportVehicleListDTO>>(await _transportVehicleRepository.GetAll());
        }

        public async Task Create(TransportVehicleCreateDTO createDTO)
        {
            var batch = _mapper.Map<TransportVehicle>(createDTO);
            await _transportVehicleRepository.Add(batch);
        }

        public async Task Delete(int id)
        {
            await _transportVehicleRepository.Delete(await _transportVehicleRepository.GetById(x => x.ID == id));
        }

        public async Task<TransportVehicleDTO> GetById(int id)
        {
            return _mapper.Map<TransportVehicleDTO>(await _transportVehicleRepository.GetById(x => x.ID == id));
        }

        public async Task<List<TransportVehicleListDTO>> GetDefaults(Expression<Func<TransportVehicle, bool>> expression)
        {
            return _mapper.Map<List<TransportVehicleListDTO>>(await _transportVehicleRepository.GetDefaults(expression));
        }

        public async Task Update(TransportVehicleUpdateDTO updateDTO)
        {
            await _transportVehicleRepository.Update(_mapper.Map<TransportVehicle>(updateDTO));
        }
    }
}
