const { createApp, ref } = Vue

createApp({
    data() {
        return {
            // msg: 'Welcome to Your Vue.js App1'
            textoIndex: "",
            subTextoIndex: "",

            toastSucessoMensagem: '',
            toastMensagemErro: '',

            welcome: "",
            isLoading: true,
            fullPage: true,
        }
    },
    mounted() {
        this.mensagens()
    },
    methods: {
        async mensagens() {
            this.isLoading = false
            this.welcome = 'Bem vindo!'
            this.textoIndex = 'Projeto Teste: Desafio UBC'
            this.subTextoIndex = 'Aplicação com ASP.NET Core e Vue'
        },        
    }
})
    .component('msgsucessoerro', msgToastComponent)
    .mount('#dvHome')