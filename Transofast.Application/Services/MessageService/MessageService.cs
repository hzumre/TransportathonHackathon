using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.MessageDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;

namespace Transofast.Application.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageListDTO>> All()
        {
            return _mapper.Map<List<MessageListDTO>>(await _messageRepository.GetAll());
        }

        public async Task Create(MessageCreateDTO createDTO)
        {
            var batch = _mapper.Map<Message>(createDTO);
            await _messageRepository.Add(batch);
        }

        public async Task Delete(int id)
        {
            await _messageRepository.Delete(await _messageRepository.GetById(x => x.ID == id));
        }

        public async Task<MessageDTO> GetById(int id)
        {
            return _mapper.Map<MessageDTO>(await _messageRepository.GetById(x => x.ID == id));
        }

        public async Task<List<MessageListDTO>> GetDefaults(Expression<Func<Message, bool>> expression)
        {
            return _mapper.Map<List<MessageListDTO>>(await _messageRepository.GetDefaults(expression));
        }

        public async Task Update(MessageUpdateDTO updateDTO)
        {
            await _messageRepository.Update(_mapper.Map<Message>(updateDTO));
        }
    }
}
