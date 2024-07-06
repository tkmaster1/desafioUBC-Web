namespace DesafioUBC.Web.Vue.UI.ViewModels
{
    public class PaginationViewModel<T>
    {
        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public IList<T> Result { get; set; }
    }
}
