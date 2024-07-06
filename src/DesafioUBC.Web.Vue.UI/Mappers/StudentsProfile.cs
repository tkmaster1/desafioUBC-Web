using AutoMapper;
using DesafioUBC.Web.UI.Application.DTOs.Students;
using DesafioUBC.Web.UI.Application.Responses;
using DesafioUBC.Web.Vue.UI.ViewModels;

namespace DesafioUBC.Web.UI.Application.Mappers
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
            CreateStudentsProfile();
        }

        private void CreateStudentsProfile()
        {
            CreateMap<StudentsDTO, StudentsViewModel>().ReverseMap();

            CreateMap<StudentsViewModel, StudentsRequestDTO>().ReverseMap();

            CreateMap<DataListPagination<StudentsDTO>, PaginationViewModel<StudentsViewModel>>()
                .AfterMap((source, converted, context) =>
                {
                    converted.Result = context.Mapper.Map<IList<StudentsViewModel>>(source.Result);
                });

        }
    }
}
