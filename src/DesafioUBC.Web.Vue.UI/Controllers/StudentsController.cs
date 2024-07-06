using AutoMapper;
using DesafioUBC.Web.UI.Application.DTOs.Students;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.Vue.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioUBC.Web.Vue.UI.Controllers
{
    [Authorize]
    [Route("[controller]/[Action]")]
    public class StudentsController : MainController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public StudentsController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Views

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> ListByFilters([FromBody] StudentsFilterDTO requestFilter)
        {
            var response = await _unitOfWork.StudentsApp.ListByFilters(requestFilter);

            var retorno = _mapper.Map<PaginationViewModel<StudentsViewModel>>(response?.Data);

            return Ok(retorno);
        }

        #endregion
    }
}
