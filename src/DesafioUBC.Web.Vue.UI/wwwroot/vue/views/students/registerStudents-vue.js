createApp({
    data() {
        return {
            newStudents: {
                name: "",
                age: "",
                series: "",
                averageGrade: "",
                Address: "",
                fatherName: "",
                motherName: "",
                dateBirth: ""
            },

            toastSucessoMensagem: "",
            toastMensagemErro: "",
            isInvalid: false,

            messageErrorValidationStudents: [],

            // Loading page
            isLoading: false,
            fullPage: true,
        }
    },
    computed: {
        classesErrorsValidation() {
            return {
                "was-validated": this.isInvalid
            }
        }
    },
    mounted() {
        // this.listarTodosMenus(1)
    },
    methods: {

        mapperObjectCreateStudents() {
            var objNewStudents = new Object({
                name: this.newStudents.name,
                age: this.newStudents.age,
                series: this.newStudents.series,
                averageGrade: this.newStudents.averageGrade,
                Address: this.newStudents.Address,
                fatherName: this.newStudents.fatherName,
                motherName: this.newStudents.motherName,
                dateBirth: this.newStudents.dateBirth,
            })

            // console.log(objNewMenuSystem)
            return objNewStudents
        },

        async saveNewStudents() {
            //  this.isLoading = true
            const newStudentsMapper = this.mapperObjectCreateStudents()
            console.log(newStudentsMapper)
        },

        clearFieldsStudents() { },
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .directive("maska", Maska.vMaska)
    .mount('#mdCreateStudents')