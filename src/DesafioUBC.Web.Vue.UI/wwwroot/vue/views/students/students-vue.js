const { createApp, ref } = Vue

const studentsApp = createApp({
    data() {
        return {

            // -- Inicio Listagem --
            studentsResponse: {},

            // -- Fim Listagem --

            // -- Inicio Filtros --
            filterName: '',
            filtroNameClicado: false,

            // -- Fim Filtros --

            // -- Inicio Edição --
            editStudents: {
                name: "",
                age: 0,
                series: 0,
                averageGrade: 0,
                Address: "",
                fatherName: "",
                motherName: "",
                dateBirth: ""
            },

            toastSucessoMensagem: '',
            toastMensagemErro: '',

            isInvalidEditar: false,
            messageErrorValidationEditStudents: [],

            // -- Fim Edição --

            // -- Inicio Excluir --

            deleteStudents: {},

            // -- Fim Excluir --

            // -- Inicio Paginação  --

            studentsResponsePagination: {},
            isLoading: true,
            fullPage: true,
            totalPages: 1,
            maxPages: 10

            // -- Fim Paginação  --
        }
    },
    computed: {
        classesErrorsValidationEditStudents() {
            return {
                "was-validated": this.isInvalidEditar
            }
        },

        pages() {
            const numShown = this.studentsResponsePagination.totalPages > this.maxPages ?
                this.maxPages : this.studentsResponsePagination.totalPages;

            let first = this.studentsResponsePagination.currentPage - Math.floor(numShown / 2);
            first = Math.max(first, 1);
            first = Math.min(first, this.studentsResponsePagination.totalPages - numShown + 1);

            return [...Array(numShown)].map((k, i) => i + first);
        },
    },
    mounted() {
        this.listAllStudents(1)
    },
    methods: {

        //#region Filtros e Listagem - Index e Detalhe

        async filterNameClick(filtroClicado) {
            if (filtroClicado == 1)
                this.filtroNameClicado = true

            this.listAllStudents(null)
        },

        async clearFilterName() {
            this.filterName = ''

            this.listAllStudents(null)
        },

        async listAllStudents(paginaAtual) {
            this.isLoading = true;

            let studentsFilterMapper = new Object({
                code: 0,
                name: this.filtroNameClicado == true ? this.filterName : '',
                currentPage: paginaAtual == null ? 1 : paginaAtual,
                pageSize: 5,
                orderBy: "firstName",
                sortBy: "asc",
            })

            try {
                const studentsFilter = studentsFilterMapper

                const response = await fetchData.fetchPostJson(
                    "/Students/ListByFilters", studentsFilter
                )

                //  console.log(response)
                this.studentsResponsePagination = response

                this.studentsResponse = response.result

                this.isLoading = false
            } catch (error) {
                this.isLoading = false

                console.log(error);

                this.toastMensagemErro = "Erro ao tentar listar os Estudante."

                $('#toastAlteracaoErro').toast("show")
            }
        },

        moment: function (date) {
            moment.locale('pt-br')
            return moment(date).format('DD/MM/YYYY');
        },

        formatarData() {
            this.editStudents.dateBirth = moment(this.editStudents.dateBirth).format('DD/MM/YYYY')
        },

        // #endregion

        //#region Métodos de Edição

        async displayModalChangeStudents(code) {
            try {
                const response = await fetchData.fetchGetJson(
                    "/Students/GetByCode/" + `${code}`)

                this.editStudents = response

                this.formatarData()

                this.isInvalidEditar = false
            }
            catch (error) {
                console.log(error)

                this.toastMensagemErro = "Ocorreu um erro ao tentar exibir a tela de alterar estudante."

                $("#toastAlteracaoErro").toast("show")
            }
        },

        mapperObjectChangeStudents() {
            let campoDataNascimento = this.$refs.dataNascimentoEditStudentsRef.value
            let d1 = moment(campoDataNascimento, 'DD/MM/YYYY', true).format()

            var objEditStudents = new Object({
                code: this.editStudents.code,
                name: this.editStudents.name,
                age: this.editStudents.age != '' ? this.editStudents.age : 0,
                series: this.editStudents.series != '' ? this.editStudents.series : 0,
                averageGrade: this.editStudents.averageGrade != '' ? this.editStudents.averageGrade : 0,
                address: this.editStudents.address,
                fatherName: this.editStudents.fatherName,
                motherName: this.editStudents.motherName,
                dateBirth: d1 != 'Invalid date' ? d1 : "",
            })

            return objEditStudents
        },

        async saveChangeStudents() {
            this.isLoading = true

            const studentsMapper = this.mapperObjectChangeStudents()
            //  console.log(studentsMapper)

            try {
                const response = await fetchData.fetchPostJsonValidation("/Students/SaveChangeStudents", studentsMapper)

                if (!response.success) {
                    this.isLoading = false

                    if (response.mensagem != undefined) {
                        this.isInvalidEditar = true;

                        if (this.editStudents.name == "" || this.editStudents.name == undefined || this.editStudents.name == null) {
                            this.messageErrorValidationEditStudents['name'] = 'O campo Nome é obrigatório!'
                        }

                        if (this.editStudents.age == "" || this.editStudents.age == undefined || this.editStudents.age == null) {
                            this.messageErrorValidationEditStudents['age'] = 'O campo Idade é obrigatório!'
                        }

                        if (this.$refs.dataNascimentoEditStudentsRef.value == "" || this.$refs.dataNascimentoEditStudentsRef.value == undefined) {
                            this.messageErrorValidationEditStudents['dateBirth'] = 'O campo Data de Nascimento é obrigatório!'
                        }

                        if (this.editStudents.StatusMessage != "" && this.editStudents.StatusMessage != undefined) {
                            this.toastMensagemErro = response.mensagem

                            $("#toastAlteracaoErro").toast("show")

                            this.clearFieldsChangeStudents()

                            this.isLoading = false
                        }
                    }
                } else {
                    this.isLoading = false
                    //  console.log('Enviado com sucesso!')

                    Swal.fire(
                        'Enviado com sucesso!',
                        'Estudante alterado com sucesso.',
                        'success'
                    ).then((result) => {
                        this.fecharModal('Alterar')

                        this.listAllStudents(1)

                        this.clearFieldsChangeStudents()
                    })
                }
            }
            catch (error) {
                this.isLoading = false

                this.fecharModal('Alterar')

                this.toastMensagemErro = "Erro ao tentar editar o Estudante."

                $("#toastAlteracaoErro").toast("show")

                this.clearFieldsChangeStudents()
            }
        },

        clearFieldsChangeStudents() {
            this.editStudents = {
                name: "",
                age: 0,
                series: 0,
                averageGrade: 0,
                Address: "",
                fatherName: "",
                motherName: "",
                dateBirth: ""
            }

            this.messageErrorValidationEditStudents = []
            this.isInvalidEditar = false
        },

        // #endregion

        // #region Métodos de Exclusão

        async displayModalDeleteStudents(code) {

            try {
                const response = await fetchData.fetchGetJson(
                    "/Students/GetByCode/" + `${code}`)

                this.deleteStudents = response
            }
            catch (error) {
                console.log(error)

                this.toastMensagemErro = "Ocorreu um erro ao tentar exibir a tela de excluir estudante."

                $("#toastAlteracaoErro").toast("show")
            }
        },

        async saveRemoveStudents(code) {
            //   this.isLoading = true

            try {
                const response = await fetchData.fetchGetJson("/Students/SaveRemoveStudents/" + `${code}`)

                if (!response.success) {
                    this.isLoading = false

                    this.fecharModal("Excluir")

                    this.toastMensagemErro = "Ocorreu um erro ao tentar excluir o estudante."

                    $("#toastAlteracaoErro").toast("show")

                    this.clearFieldsRemoveStudents()

                } else {
                    this.isLoading = false

                    // console.log('Enviado com sucesso!')

                    Swal.fire(
                        'Enviado com sucesso!',
                        'Estudante excluído com sucesso.',
                        'success'
                    ).then((result) => {
                        this.fecharModal("Excluir")

                        this.listAllStudents(1)

                        this.clearFieldsRemoveStudents()
                    })
                }
            }
            catch (error) {
                this.isLoading = false

                console.log(error)

                this.toastMensagemErro = "Ocorreu um erro ao tentar excluir o estudante."

                this.fecharModal("Excluir")

                $("#toastAlteracaoErro").toast("show")

                this.clearFieldsRemoveStudents()
            }
        },

        clearFieldsRemoveStudents() {
            this.deleteStudents = {}
        },

        // #endregion

        fecharModal(operacao) {
            if (operacao == 'Alterar') {
                $("#modalUpdateStudents").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalUpdateStudents").hide();
            } else {
                $("#modalDeleteStudents").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalDeleteStudents").hide();
            }
        },
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .component('msgsucessoerro', msgToastComponent)
    .directive("maska", Maska.vMaska)
    .mount('#dvStudents')