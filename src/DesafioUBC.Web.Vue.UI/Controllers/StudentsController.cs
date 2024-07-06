using AutoMapper;
using DesafioUBC.Web.UI.Application.Common;
using DesafioUBC.Web.UI.Application.DTOs.Students;
using DesafioUBC.Web.UI.Application.Extensions;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;
using DesafioUBC.Web.Vue.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(int code)
        {
            var response = await _unitOfWork.StudentsApp.GetByCode(code);

            var retorno = _mapper.Map<StudentsViewModel>(response?.Data ?? new StudentsDTO());

            return Ok(retorno);
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewStudents([FromBody] StudentsViewModel studentsViewModel)
        {
            string Mensagem;
            bool Success;

            if (!ModelState.IsValid) return Json(new { success = false, mensagem = ModelState });

            var studentsDomain = _mapper.Map<StudentsRequestDTO>(studentsViewModel);

            var response = await _unitOfWork.StudentsApp.CreateStudents(studentsDomain);

            if (!response.Success)
            {
                var retorno = JsonConvert.DeserializeObject<Erros>(response.Errors.ToString());

                studentsViewModel.StatusMessage += "* " + retorno.Value + "<br/>";

                Success = false;
                Mensagem = PersonalizedMessages.MSG_FALHA.ToFormat("Incluir", "o Estudante");
            }
            else
            {
                Success = true;
                Mensagem = PersonalizedMessages.MSG_SUCESSO.ToFormat("realizada", "Inclusão");
            }

            return Json(new { success = Success, mensagem = Mensagem });
        }

        [HttpPost]
        public async Task<IActionResult> SaveChangeStudents([FromBody] StudentsViewModel studentsViewModel)
        {
            bool Success;
            string Mensagem;

            if (!ModelState.IsValid) return Json(new { success = false, mensagem = ModelState });

            var studentsDomain = _mapper.Map<StudentsRequestDTO>(studentsViewModel);

            var response = await _unitOfWork.StudentsApp.UpdateStudents(studentsDomain);

            if (!response.Success)
            {
                var retorno = JsonConvert.DeserializeObject<Erros>(response.Errors.ToString());

                studentsViewModel.StatusMessage += "* " + retorno.Value + "<br/>";

                Success = false;
                Mensagem = PersonalizedMessages.MSG_FALHA.ToFormat("alterar", "o Estudante");
            }
            else
            {
                Success = true;
                Mensagem = PersonalizedMessages.MSG_SUCESSO.ToFormat("realizada", "Alteração");
            }

            return Json(new { success = Success, mensagem = Mensagem });
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> SaveRemoveStudents(int code)
        {
            string Mensagem;
            bool Success;

            var response = await _unitOfWork.StudentsApp.RemoveStudents(code);

            if (!response.Success)
            {
                Success = false;
                Mensagem = PersonalizedMessages.MSG_FALHA.ToFormat("excluir", "o Estudante");
            }
            else
            {
                Success = true;
                Mensagem = PersonalizedMessages.MSG_SUCESSO.ToFormat("realizada", "Exclusão");
            }

            return Json(new { success = Success, mensagem = Mensagem });
        }

        #endregion
    }
}
