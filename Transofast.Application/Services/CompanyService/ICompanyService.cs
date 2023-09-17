using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.CompanyService
{
    public interface ICompanyService : IBaseService<CompanyCreateDTO, CompanyUpdateDTO, CompanyListDTO, CompanyDTO, Company, int>
    {
    }
}
