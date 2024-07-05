using AutoMapper;
using DesafioUBC.Web.UI.Application.DTOs;
using DesafioUBC.Web.Vue.UI.ViewModels;

namespace DesafioUBC.Web.Vue.UI.Mappers
{
    public class LoginRegisterProfile : Profile
    {
        public LoginRegisterProfile()
        {
            CreateLoginRegisterProfile();
        }

        private void CreateLoginRegisterProfile()
        {
            CreateMap<LoginUserViewModel, LoginUserRequestDTO>().ReverseMap();
          //  CreateMap<UserIdentityRegisterViewModel, UserIdentityRegisterRequestDTO>().ReverseMap();
        }
    }
}
