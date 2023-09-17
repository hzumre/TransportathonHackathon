using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Application.DTOs.AppRoleDTOs;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.CompanyAdminDTOs;
using Transofast.Application.DTOs.CompanyDTOs;
using Transofast.Application.DTOs.CompanyEmployeeDTOs;
using Transofast.Application.DTOs.MessageDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Application.DTOs.TransportVehicleDTOs;
using Transofast.Application.DTOs.UserDTOs;
using Transofast.Domain.Entities.Concrete;

namespace Transofast.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //AppRole
            CreateMap<AppRole, AppRoleCreateDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleListDTO>().ReverseMap();
            CreateMap<AppRole, AppRoleUpdateDTO>().ReverseMap();
            CreateMap<AppUser, RoleAssignDTO>().ReverseMap();

            //AppUser
            CreateMap<AppUser, LoginDTO>().ReverseMap();
            CreateMap<AppUser, UserListDTO>().ReverseMap();
            CreateMap<AppUser, UserUpdateDTO>().ReverseMap();
            CreateMap<AppUser, RegisterDTO>().ReverseMap();

            //Comment
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentListDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();


            //Company
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyListDTO>().ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();

            //Message
            CreateMap<Message, MessageCreateDTO>().ReverseMap();
            CreateMap<Message, MessageListDTO>().ReverseMap();
            CreateMap<Message, MessageUpdateDTO>().ReverseMap();
            CreateMap<Message, MessageDTO>().ReverseMap();

            //Transport
            CreateMap<Transport, TransportCreateDTO>().ReverseMap();
            CreateMap<Transport, TransportListDTO>().ReverseMap();
            CreateMap<Transport, TransportUpdateDTO>().ReverseMap();
            CreateMap<Transport, TransportDTO>().ReverseMap();

            //TransportVehicle
            CreateMap<TransportVehicle, TransportVehicleCreateDTO>().ReverseMap();
            CreateMap<TransportVehicle, TransportVehicleListDTO>().ReverseMap();
            CreateMap<TransportVehicle, TransportVehicleUpdateDTO>().ReverseMap();
            CreateMap<TransportVehicle, TransportVehicleDTO>().ReverseMap();


        }
    }
}
