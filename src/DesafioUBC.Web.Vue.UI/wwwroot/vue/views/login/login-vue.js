const { createApp, ref } = Vue

createApp({
    data() {
        return {
            newLoginUser: {
                userName: "",
                password: "",
                returnUrl: ""
            },

            toastSucessoMensagem: "",
            toastMensagemErro: "",
            isInvalid: false,
            messageErrorValidation: [],
            messageErrorValidationViewData: "",

            // Loading page
            isLoading: false,
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
        mapperObjectLoginRegister() {
            var objNewLoginRegister = new Object({
                userName: this.newLoginUser.userName,
                password: this.newLoginUser.password,
                returnUrl: this.newLoginUser.returnUrl
            })

            return objNewLoginRegister
        },

        async saveNewLoginRegister() {
            this.isLoading = true

            const loginMapper = this.mapperObjectLoginRegister()

            try {
                const response = await fetchData.fetchPostJsonValidation("/Identidade/SaveRegisterAndLogin", loginMapper)

                if (!response.success) {
                    this.isLoading = false

                    if (response.mensagem != undefined) {
                        this.isInvalid = true;

                        this.messageErrorValidationViewData = response.mensagem

                        if (this.newLoginUser.email == "" || this.newLoginUser.email == undefined || this.newLoginUser.email == null) {
                            this.messageErrorValidation['email'] = 'O E-mail é obrigatório!'
                        }

                        if (this.newLoginUser.password == "" || this.newSubMenuSystem.password == undefined || this.newLoginUser.password == null) {
                            this.messageErrorValidation['password'] = 'A senha é obrigatória!'
                        }
                    }
                } else {
                    this.isLoading = false

                    //console.log('Enviado com sucesso!')

                    this.clearFieldsLogin()

                    window.location.href = '/Home/Index'
                }
            }
            catch (error) {
                // debugger;
                console.log(error)

                this.toastMensagemErro = "Erro ao tentar realizar o login no Sistema."

                $("#toastErroLogin").toast("show")
            }
        },

        clearFieldsLogin() {

            this.newLoginUser = {
                email: "",
                password: "",
                returnUrl: ""
            }

            this.messageErrorValidationViewData = ''
            this.messageErrorValidation = []
            this.isInvalid = false
        },
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .component('msgsucessoerro', msgToastComponent)
    .mount('#dvLogin')