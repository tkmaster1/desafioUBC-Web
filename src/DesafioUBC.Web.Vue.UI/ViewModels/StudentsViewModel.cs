﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioUBC.Web.Vue.UI.ViewModels
{
    public class StudentsViewModel
    {
        public int? Code { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        /// <summary>
        /// idade do estudante
        /// </summary>
        [Required(ErrorMessage = "A Idade é obrigatória.")]
        public int Age { get; set; }

        /// <summary>
        /// série do estudante
        /// </summary>
        public int? Series { get; set; }

        /// <summary>
        /// nota média do estudante
        /// </summary>
        public double? AverageGrade { get; set; }

        /// <summary>
        /// endereço do estudant
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// nome do pai do estudante
        /// </summary>
        public string FatherName { get; set; }

        /// <summary>
        /// nome da mãe do estudante
        /// </summary>
        public string MotherName { get; set; }

        /// <summary>
        /// data de nascimento do estudante
        /// </summary>
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        public DateTime DateBirth { get; set; }

        [NotMapped]
        public string StatusMessage { get; set; }
    }
}