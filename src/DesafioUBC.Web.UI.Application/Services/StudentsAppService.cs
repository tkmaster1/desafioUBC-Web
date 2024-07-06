using DesafioUBC.Web.UI.Application.DTOs;
using DesafioUBC.Web.UI.Application.DTOs.Students;
using DesafioUBC.Web.UI.Application.Extensions;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Services
{
    public class StudentsAppService : IStudentsAppService
    {
        #region Properties

        private readonly IBaseService _baseService;

        #endregion

        #region Constructor

        public StudentsAppService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        #endregion

        #region Methods Publics

        public async Task<ResponseAPIDataListPaginations<StudentsDTO>> ListByFilters(StudentsFilterDTO req)
        {
            string url = $"{_baseService.UrlBase}/Students/searchStudents/";

            return await _baseService.Post(url, req).ToResponseListPaginations<StudentsDTO>();
        }

        public async Task<ResponseAPIData<StudentsDTO>> GetByCode(int code)
        {
            string url = $"{_baseService.UrlBase}/Students/getByCode/{code.ToString()}";

            return await _baseService.Get(url).ToResponse<StudentsDTO>();
        }

        public async Task<ResponseAPIData<object>> CreateStudents(StudentsRequestDTO studentsRequest)
        {
            string url = $"{_baseService.UrlBase}/Students/include/";

            return await _baseService.Post(url, studentsRequest).ToResponse<object>();
        }

        public async Task<ResponseAPIData<object>> UpdateStudents(StudentsRequestDTO studentsRequest)
        {
            string url = $"{_baseService.UrlBase}/Students/edit/";

            return await _baseService.Put(url, studentsRequest).ToResponse<object>();
        }

        public async Task<ResponseAPIData<object>> RemoveStudents(int code)
        {
            string url = $"{_baseService.UrlBase}/Students/delete/{code.ToString()}";

            return await _baseService.Delete(url).ToResponse<object>();
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
