using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;

namespace Transofast.Application.Services.CommentService
{
    public class CommentService:ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<List<CommentListDTO>> All()
        {
            return _mapper.Map<List<CommentListDTO>>(await _commentRepository.GetAll());
        }

        public async Task Create(CommentCreateDTO createDTO)
        {
            var batch = _mapper.Map<Comment>(createDTO);
            await _commentRepository.Add(batch);
        }

        public async Task Delete(int id)
        {
            await _commentRepository.Delete(await _commentRepository.GetById(x => x.ID == id));
        }

        public async Task<CommentDTO> GetById(int id)
        {
            return _mapper.Map<CommentDTO>(await _commentRepository.GetById(x => x.ID == id));
        }

        public async Task<List<CommentListDTO>> GetDefaults(Expression<Func<Comment, bool>> expression)
        {
            return _mapper.Map<List<CommentListDTO>>(await _commentRepository.GetDefaults(expression));
        }

        public async Task Update(CommentUpdateDTO updateDTO)
        {
            await _commentRepository.Update(_mapper.Map<Comment>(updateDTO));
        }
    }
}
