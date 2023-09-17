using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.Services.CommentService
{
    public interface ICommentService:IBaseService<CommentCreateDTO,CommentUpdateDTO,CommentListDTO,CommentDTO,Comment,int>
    {
    }
}
