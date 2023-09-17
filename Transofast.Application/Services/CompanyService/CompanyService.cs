using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;

namespace Transofast.Application.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<List<CompanyListDTO>> All()
        {
            return _mapper.Map<List<CompanyListDTO>>(await _companyRepository.GetAll());
        }

        public async Task Create(CompanyCreateDTO createDTO)
        {
            var batch = _mapper.Map<Company>(createDTO);
            await _companyRepository.Add(batch);
        }

        public async Task Delete(int id)
        {
            await _companyRepository.Delete(await _companyRepository.GetById(x => x.ID == id));
        }

        public async Task<CompanyDTO> GetById(int id)
        {
            return _mapper.Map<CompanyDTO>(await _companyRepository.GetById(x => x.ID == id));
        }

        public async Task<List<CompanyListDTO>> GetDefaults(Expression<Func<Company, bool>> expression)
        {
            return _mapper.Map<List<CompanyListDTO>>(await _companyRepository.GetDefaults(expression));
        }

        public async Task Update(CompanyUpdateDTO updateDTO)
        {
            await _companyRepository.Update(_mapper.Map<Company>(updateDTO));
        }
    }
}
