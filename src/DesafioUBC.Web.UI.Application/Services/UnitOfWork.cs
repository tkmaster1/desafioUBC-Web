using DesafioUBC.Web.UI.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DesafioUBC.Web.UI.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        private readonly IBaseService _baseService;
        private readonly IHttpContextAccessor _context;

        #endregion

        #region Constructor

        public UnitOfWork(IBaseService baseService,
                          IHttpContextAccessor context)
        {
            _baseService = baseService;
            _context = context;
        }

        #endregion

        #region Instância Privadas

        private readonly ILoginRegisterUserAppService _loginRegisterUserApp;

        private readonly IStudentsAppService _studentsApp;

        #endregion

        #region Propriedades Públicas

        public ILoginRegisterUserAppService LoginRegisterUserApp
       => _loginRegisterUserApp ?? new LoginRegisterUserAppService(_baseService);

        public IStudentsAppService StudentsApp => _studentsApp ?? new StudentsAppService(_baseService);

        #endregion
    }
}
