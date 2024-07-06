namespace DesafioUBC.Web.UI.Application.DTOs.Students
{
    public class StudentsDTO
    {
        public int Code { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// idade do estudante
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// série do estudante
        /// </summary>
        public int Series { get; set; }

        /// <summary>
        /// nota média do estudante
        /// </summary>
        public double AverageGrade { get; set; }

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
        public DateTime DateBirth { get; set; }
    }
}
