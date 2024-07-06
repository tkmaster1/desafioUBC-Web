const { createApp, ref } = Vue

const menuSystemApp = createApp({
    data() {
        return {

            // -- Inicio Listagem --
            studentsResponse: {},

            // -- Fim Listagem --

            //// -- Inicio Filtros --
            //filtroTitleMenu: '',
            //filtroControllerMenu: '',
            //textoBotaoStatus: "Status",
            //statusSelecionado: 0,
            //statusFilter: undefined,

            //filtroTitleMenuClicado: false,
            //filtroControllerMenuClicado: false,

            //// -- Fim Filtros --

            //// -- Inicio Edição --
            //editionMenuSystem: {
            //    title: '',
            //    controller: '',
            //    action: '',
            //    icon: '',
            //    status: true
            //},
            //isInvalidEditar: false,
            //statusChecked: false,
            //messageErrorValidationMenuSystemEdit: [],

            //// -- Fim Edição --

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
        classesErrorsValidationMenuEdit() {
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

        //async selecionarStatus(statusSelecionado) {
        //    if (statusSelecionado == "1")
        //        this.textoBotaoStatus = "Ativo"
        //    else if (statusSelecionado == "2")
        //        this.textoBotaoStatus = "Inativo"

        //    this.statusSelecionado = statusSelecionado
        //    this.listAllMenus(null)
        //},

        //async filterTitleOrControllerMenu(filtroClicado) {
        //    if (filtroClicado == 1)
        //        this.filtroTitleMenuClicado = true
        //    else if (filtroClicado == 2)
        //        this.filtroControllerMenuClicado = true

        //    this.listAllMenus(null)
        //},

        //async clearFilterMenu() {
        //    this.textoBotaoStatus = "Status"

        //    this.filtroTitleMenu = ''
        //    this.filtroControllerMenu = ''
        //    this.filtroTitleMenuClicado = false
        //    this.filtroControllerMenuClicado = false

        //    this.statusSelecionado = 0
        //    this.statusFilter = undefined

        //    this.listAllMenus()
        //},

        async listAllStudents(paginaAtual) {
            this.isLoading = true;

            let studentsFilterMapper = new Object({
                code: 0,
                name: '', //this.filtroTitleMenuClicado == true ? this.filtroTitleMenu : '',
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

                $('#toastErro').toast("show")
            }
        },

        // #endregion

        ////#region Métodos de Edição

        //async displayModalChangeMenuSystem(code) {
        //    const response = await fetchData.fetchGetJson("/MenuSystem/GetByCode/" + `${code}`)

        //    this.editionMenuSystem = response

        //    this.editionMenuSystem.status == true ? this.statusChecked = true : this.statusChecked = false

        //    this.isInvalidEditar = false
        //},

        //mapperObjectChangeMenuSystem() {
        //    var objEditMenuSystem = new Object({
        //        code: this.editionMenuSystem.code,
        //        title: this.editionMenuSystem.title,
        //        controller: this.editionMenuSystem.controller,
        //        action: this.editionMenuSystem.action,
        //        icon: this.editionMenuSystem.icon,
        //        Status: this.editionMenuSystem.status,
        //    })

        //    // console.log(objEditMenuSystem)
        //    return objEditMenuSystem
        //},

        //async saveChangeMenuSistema() {
        //    this.isLoading = true
        //    const menuSystemMapper = this.mapperObjectChangeMenuSystem()
        //    // console.log(menuSistemaMapper)

        //    try {
        //        const response = await fetchData.fetchPostJsonValidation("/MenuSystem/ChangeMenuSystem", menuSystemMapper)

        //        if (!response.success) {
        //            this.isLoading = false

        //            if (response.mensagem != undefined) {
        //                this.isInvalidEditar = true;

        //                if (this.editionMenuSystem.title == "" || this.editionMenuSystem.title == undefined || this.editionMenuSystem.title == null) {
        //                    this.messageErrorValidationMenuSystemEdit['title'] = 'O campo Título é obrigatório!'
        //                }

        //                if (this.editionMenuSystem.controller == "" || this.editionMenuSystem.controller == undefined || this.editionMenuSystem.controller == null) {
        //                    this.messageErrorValidationMenuSystemEdit['controller'] = 'O campo Controller é obrigatório!'
        //                }

        //                if (this.editionMenuSystem.action == "" || this.editionMenuSystem.action == undefined || this.editionMenuSystem.action == null) {
        //                    this.messageErrorValidationMenuSystemEdit['action'] = 'O campo Action é obrigatório!'
        //                }
        //            }
        //        } else {
        //            this.isLoading = false
        //            console.log('Enviado com sucesso!')

        //            Swal.fire(
        //                'Enviado com sucesso!',
        //                'Menu de Sistema alterado com sucesso.',
        //                'success'
        //            ).then((result) => {
        //                this.fecharModal('Alterar')

        //                this.listAllMenus(1)

        //                this.clearFieldsChangeMenuSystem()
        //            })
        //        }
        //    }
        //    catch (error) {
        //        this.isLoading = false

        //        this.fecharModal('Alterar')

        //        $("#toastErro").toast("show")

        //        this.clearFieldsChangeMenuSystem()
        //    }
        //},

        //clearFieldsChangeMenuSystem() {
        //    this.editionMenuSystem = {
        //        title: "",
        //        controller: "",
        //        action: "",
        //        icon: "",
        //        status: true
        //    }

        //    this.messageErrorValidationMenuSystemEdit = []
        //    this.isInvalidEditar = false
        //},

        //// #endregion

        //// #region Métodos de Exclusão

        //async displayModalDeleteMenuSystem(code) {

        //    try {
        //        const response = await fetchData.fetchGetJson(
        //            "/MenuSystem/GetByCode/" + `${code}`)

        //        this.editionMenuSystem = response
        //    }
        //    catch (error) {
        //        console.log(error)

        //        this.toastMensagemErro = "Ocorreu um erro ao tentar exibir a tela de excluir menu de sistema."

        //        $("#toastErro").toast("show")
        //    }
        //},

        //async saveRemoveMenuSystem(code) {
        //    this.isLoading = true

        //    try {
        //        const response = await fetchData.fetchGetJson("/MenuSystem/SaveRemoveMenuSystem/" + `${code}`)

        //        if (!response.success) {
        //            this.isLoading = false

        //            this.fecharModal("Excluir")

        //            $("#toastErro").toast("show")

        //        } else {
        //            this.isLoading = false

        //            console.log('Enviado com sucesso!')

        //            Swal.fire(
        //                'Enviado com sucesso!',
        //                'Menu de Sistema excluído com sucesso.',
        //                'success'
        //            ).then((result) => {
        //                this.fecharModal("Excluir")

        //                this.listAllMenus(1)

        //                this.clearFieldsChangeMenuSystem()
        //            })
        //        }
        //    }
        //    catch (error) {
        //        this.isLoading = false

        //        console.log(error)

        //        this.toastMensagemErro = "Ocorreu um erro ao tentar excluir o menu de sistema."

        //        this.fecharModal("Excluir")

        //        $("#toastErro").toast("show")
        //    }
        //},

        //// #endregion

        ////#region Métodos de SubMenuSistema

        //async displayListSubMenuSystem(codMenu, titulo) {
        //    window.location.href = '/SubMenuSystem/Index?codMenu=' + codMenu + '&titulo=' + titulo
        //},

        //// #endregion Fim SubMenuSistema

        fecharModal(operacao) {
            if (operacao == 'Alterar') {
                $("#modalUpdateMenuSystem").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalUpdateMenuSystem").hide();
            } else {
                $("#modalDeleteMenuSystem").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalDeleteMenuSystem").hide();
            }
        },

        //abrirModal(idModal) {
        //    let modalElement = document.querySelector(idModal);
        //    let modal = bootstrap.Modal.getOrCreateInstance(modalElement)
        //    modal.show()
        //},

        //fecharModal(idModal) {
        //    let modalElement = document.querySelector(idModal);

        //    let modal = bootstrap.Modal.getOrCreateInstance(modalElement)

        //    modal.hide()
        //},
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .mount('#dvStudents')