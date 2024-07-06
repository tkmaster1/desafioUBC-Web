createApp({
    data() {
        return {
            newStudents: {
                name: "",
                age: 0,
                series: 0,
                averageGrade: 0,
                address: "",
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
    },
    methods: {

        mapperObjectCreateStudents() {

            let campoDataNascimento = this.$refs.dataNascimentoStudentsRef.value
            let d1 = moment(campoDataNascimento, 'DD/MM/YYYY', true).format()

            var objNewStudents = new Object({
                name: this.newStudents.name,
                age: this.newStudents.age != '' ? this.newStudents.age : 0,
                series: this.newStudents.series != '' ? this.newStudents.series : 0,
                averageGrade: this.newStudents.averageGrade != '' ? this.newStudents.averageGrade : 0,
                address: this.newStudents.address,
                fatherName: this.newStudents.fatherName,
                motherName: this.newStudents.motherName,
                dateBirth: d1 != 'Invalid date' ? d1 : "",
            })

            return objNewStudents
        },

        async saveNewStudents() {
            //
            const newStudentsMapper = this.mapperObjectCreateStudents()
            //   console.log(newStudentsMapper)

            try {
                const response = await fetchData.fetchPostJsonValidation("/Students/SaveNewStudents", newStudentsMapper)

                if (!response.success) {
                    // debugger;

                    this.isLoading = false

                    if (response.mensagem != undefined) {
                        this.isInvalid = true;

                        if (this.newStudents.name == "" || this.newStudents.name == undefined || this.newStudents.name == null) {
                            this.messageErrorValidationStudents['name'] = 'O campo Nome é obrigatório!'
                        }

                        if (this.newStudents.age == "" || this.newStudents.age == undefined || this.newStudents.age == null) {
                            this.messageErrorValidationStudents['age'] = 'O campo Idade é obrigatório!'
                        }

                        if (this.$refs.dataNascimentoStudentsRef.value == "" || this.$refs.dataNascimentoStudentsRef.value == undefined) {
                            this.messageErrorValidationStudents['dateBirth'] = 'O campo Data de Nascimento é obrigatório!'
                        }

                        if (this.newStudents.StatusMessage != "" && this.newStudents.StatusMessage != undefined) {
                            this.toastMensagemErro = response.mensagem

                            $("#toastErroCadastrar").toast("show")

                            this.clearFieldsStudents()

                            this.isLoading = false
                        }
                    }
                } else {

                    this.isLoading = false

                    //   console.log('Enviado com sucesso!')

                    Swal.fire(
                        'Enviado com sucesso!',
                        'Estudante incluído com sucesso.',
                        'success'
                    ).then((result) => {
                        this.fecharModal()

                        studentsApp.listAllStudents(1)

                        this.clearFieldsStudents()
                    })
                }
            }
            catch (error) {
                // debugger;
                console.log(error)

                this.fecharModal()

                this.toastMensagemErro = "Erro ao tentar cadastrar o Estudante."

                $("#toastErroCadastrar").toast("show")

                this.clearFieldsStudents()

                this.isLoading = false
            }
        },

        clearFieldsStudents() {
            this.newStudents = {
                name: "",
                age: 0,
                series: 0,
                averageGrade: 0,
                address: "",
                fatherName: "",
                motherName: "",
                dateBirth: ""
            }

            this.$refs.dataNascimentoStudentsRef.value = ""

            this.messageErrorValidationStudents = []
            this.isInvalid = false
        },

        fecharModal() {
            $("#modalCreateStudents").removeClass("show");
            $(".modal-backdrop").remove();
            $("#modalCreateStudents").hide();
        },
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .directive("maska", Maska.vMaska)
    .component('msgsucessoerro', msgToastComponent)
    .mount('#mdCreateStudents')