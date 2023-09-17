using AutoMapper;
using Transofast.Application.DTOs.AppUserDTOs;
using Transofast.Application.DTOs.CommentDTOs;
using Transofast.Application.DTOs.CompanyAdminDTOs;
using Transofast.Application.DTOs.MessageDTOs;
using Transofast.Application.DTOs.TransportDTOs;
using Transofast.Application.DTOs.UserDTOs;
using Transofast.Domain.Entities.Concrete;
using Transofast.Presentation.Areas.Admin.Models.ViewModels;
using Transofast.Presentation.Areas.User.Models.ViewModels.MessageVM;
using Transofast.Presentation.Areas.User.Models.ViewModels.TransportVM;
using Transofast.Presentation.Areas.User.Models.ViewModels.UserVM;
using Transofast.Presentation.Models;

namespace Transofast.Presentation.MapperProfile
{
    public class MappingUI : Profile
    {
        public MappingUI()
        {
            CreateMap<RegisterDTO, UserRegisterVM>().ReverseMap();
            CreateMap<UserCreateDTO, UserRegisterVM>().ReverseMap();
            CreateMap<UserListDTO, UserListVM>().ReverseMap();
            CreateMap<AppUser, UserListVM>().ReverseMap();
            CreateMap<UserUpdateDTO, UserUpdateVM>().ReverseMap();

            CreateMap<RegisterDTO, AdminRegisterVM>().ReverseMap();
            CreateMap<RegisterDTO, UserVM>().ReverseMap();
            CreateMap<LoginDTO, UserSignInVM>().ReverseMap();

            CreateMap<TransportCreateDTO, TransportCreateVM>().ReverseMap();
            CreateMap<TransportUpdateDTO, TransportUpdateVM>().ReverseMap();
            CreateMap<TransportDTO, TransportUpdateVM>().ReverseMap();
            CreateMap<TransportCreateVM, TransportCreateDTO>().ReverseMap();
            CreateMap<TransportListDTO, TransportListVM>().ReverseMap();

            CreateMap<MessageCreateDTO, MessageCreateVM>().ReverseMap();
            CreateMap<MessageListDTO, MessageListVM>().ReverseMap();

            CreateMap<CompanyAdminCreateDTO, AdminRegisterVM>().ReverseMap();

            CreateMap<CommentListDTO, CommentListVM>().ReverseMap();

        }
    }
}
