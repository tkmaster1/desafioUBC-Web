using DesafioUBC.Web.UI.Application.DTOs.Students;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface IStudentsAppService : IDisposable
    {
        Task<ResponseAPIDataListPaginations<StudentsDTO>> ListByFilters(StudentsFilterDTO req);

        Task<ResponseAPIData<StudentsDTO>> GetByCode(int code);

        Task<ResponseAPIData<object>> CreateStudents(StudentsRequestDTO menuSystemReq);

        //Task<ResponseAPIData<object>> UpdateMenuSystem(MenuSystemRequestDTO menuSystemReq);

        //Task<ResponseAPIData<object>> RemoveMenuSystem(int code);
    }
}
