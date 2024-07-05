namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface IUnitOfWork
    {
        //  IAlunoAppService AlunoApp { get; }
        ILoginRegisterUserAppService LoginRegisterUserApp { get; }
    }
}
