using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.MessageDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.MessageService
{
    public interface IMessageService : IBaseService<MessageCreateDTO, MessageUpdateDTO, MessageListDTO, MessageDTO, Message, int>
    {
    }
}
