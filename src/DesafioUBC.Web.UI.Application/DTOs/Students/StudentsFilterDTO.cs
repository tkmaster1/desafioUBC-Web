using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioUBC.Web.UI.Application.DTOs.Students
{
    public class StudentsFilterDTO
    {
        /// <summary>
        /// Id do estudante
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Nome do estudante
        /// </summary>
        public string Name { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string OrderBy { get; set; } = "firstName";

        public string SortBy { get; set; } = "asc";
    }
}
