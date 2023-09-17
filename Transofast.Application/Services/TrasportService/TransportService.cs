using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;

namespace Transofast.Application.Services.TrasportService
{
    public class TransportService : ITransportService
    {
        private readonly ITransportRepository _transportRepository;
        private readonly IMapper _mapper;

        public TransportService(ITransportRepository transportRepository, IMapper mapper)
        {
            _transportRepository = transportRepository;
            _mapper = mapper;
        }

        public async Task<List<TransportListDTO>> All()
        {
            return _mapper.Map<List<TransportListDTO>>(await _transportRepository.GetAll());
        }

        public async Task Create(TransportCreateDTO createDTO)
        {
            var batch = _mapper.Map<Transport>(createDTO);
            await _transportRepository.Add(batch);
        }

        public async Task Delete(int id)
        {
            await _transportRepository.Delete(await _transportRepository.GetById(x => x.ID == id));
        }

        public async Task<TransportDTO> GetById(int id)
        {
            return _mapper.Map<TransportDTO>(await _transportRepository.GetById(x => x.ID == id));
        }

        public async Task<List<TransportListDTO>> GetDefaults(Expression<Func<Transport, bool>> expression)
        {
            return _mapper.Map<List<TransportListDTO>>(await _transportRepository.GetDefaults(expression));
        }

        public async Task Update(TransportUpdateDTO updateDTO)
        {
            await _transportRepository.Update(_mapper.Map<Transport>(updateDTO));
        }
    }
}
