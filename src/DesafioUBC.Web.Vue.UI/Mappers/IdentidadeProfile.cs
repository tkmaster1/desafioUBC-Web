using AutoMapper;
using DesafioUBC.Web.UI.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioUBC.Web.UI.Application.Mappers
{
    public class IdentidadeProfile : Profile
    {
        //public IdentidadeProfile()
        //{
        //    CreateIdentidadeProfile();
        //}

        //private void CreateIdentidadeProfile()
        //{
        //    //#region Login

        //    //CreateMap<LoginUsuarioViewModel, LoginUsuarioRequestDTO>().ReverseMap();
        //    //CreateMap<UsuarioIdentityRegisterViewModel, UsuarioIdentityRegisterRequestDTO>().ReverseMap();

        //    //#endregion

        //    #region Usuário

        //    CreateMap<UserIdentityDTO, UserIdentityViewModel>().ReverseMap();

        //    CreateMap<DataListPagination<UserIdentityDTO>, PaginationViewModel<UserIdentityViewModel>>()
        //        .AfterMap((source, converted, context) =>
        //        {
        //            converted.Result = context.Mapper.Map<IList<UserIdentityViewModel>>(source.Result);
        //        });

        //    #endregion
        //}
    }
}
