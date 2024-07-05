namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentsAppService StudentsApp { get; }

        ILoginRegisterUserAppService LoginRegisterUserApp { get; }
    }
}
