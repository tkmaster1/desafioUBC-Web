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

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
