using AutoMapper;
using DesafioUBC.Web.UI.Application.Interfaces;
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

        #endregion
    }
}
