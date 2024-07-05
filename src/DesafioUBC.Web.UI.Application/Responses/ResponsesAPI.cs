namespace DesafioUBC.Web.UI.Application.Responses
{
    public class ResponseAPI
    {
        public object Errors { get; set; }

        public bool Success { get; set; }
    }

    public class ResponseAPIData<TEntity> : ResponseAPI
    {
        public TEntity Data { get; set; }
    }

    public class ResponseAPIDataList<TEntity> : ResponseAPI
    {
        public List<TEntity> Data { get; set; }
    }

    public class ResponseAPIDataListPaginations<TEntity> : ResponseAPI
    {
        public DataListPagination<TEntity> Data { get; set; }
    }

    public class DataListPagination<TEntity>
    {
        public int CurrentPage { get; set; }

        public int Count { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public IList<TEntity> Result { get; set; }
    }

    public class ResponseErrors
    {
        public bool Succeeded { get; set; }

        public IList<Erros> Errors { get; set; }
    }

    public class Erros
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
