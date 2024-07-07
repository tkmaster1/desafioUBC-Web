# desafioUBC-Web
Teste de Front-end com consumo de WebAPI para trabalho

# --------------------------------------------------------------------------

##### Front-end (Vue)
1. **Framework**: Vue.
2. **Componentes**:
    - **Login**: Tela de login.
    - **StudentList**: Exibe a lista de estudantes.
    - **StudentForm**: Formulário para criar/atualizar e excluir estudantes.

3. **Funcionalidades**:
    - Login de usuário.
    - Listar todos os estudantes (após login).
    - Adicionar um novo estudante (após login).
    - Atualizar um estudante existente (após login).
    - Excluir um estudante (após login).

4. **UI/UX**:
    - Utilize uma biblioteca de componentes UI: ** R.:(Bootstrap: Adminlte.io free).
    - A interface deve ser responsiva e de fácil utilização. ** R.: Sim está responsiva e de fácil utilização.
    - Exiba mensagens de erro e sucesso para as operações de CRUD. ** R.: Exibindo.

# --------------------------------------------------------------------------

- ** Students:
    - Listar todos os estudantes (após login).
    - Adicionar um novo estudante (após login).
    - Atualizar um estudante existente (após login).
    - Excluir um estudante (após login).

# --------------------------------------------------------------------------

- ** User
    - Login
	- Logout
	
# --------------------------------------------------------------------------
	
#### Entregáveis
1. **Código Fonte**:    
    - Para rodar o projeto localmente:
		- Baixar os códigos fontes dos seus respectivos repositórios, abrir na IDE (Microsoft Visual Studio Community 2022 (64-bit), Visual Studio Core ou superior).
		- Rodar cada aplicação no IIS Express simultaneamente. 
			- Projeto WebAPI: Startar o projeto UBC.Core.WebApi e depois,
			- Projeto Frot-End: Startar o projeto DesafioUBC.Web.Vue.UI.
	* Ambos no browaer de preferência: Chrome.

2. **Documentação**:
    - Documentação da API: Swagger utilizado (O Swagger é usado para gerar documentação útil e páginas de ajuda para APIs Web)
    - Documentação explicando as principais decisões de arquitetura e design:
		- Arquitetura utilizada: Clean Architecture (Explicação conceitual: https://macoratti.net/22/09/asp_cleanarq1.htm) onde esta utiliza uma abordagem que pode ser usada em aplicações que devem crescer 
em tamanho e dessa forma podemos criar uma solução que também pode melhorar a cobertura do código.
		- MVC é um design de projeto que conceitua a separação em Model, View e Controller: Utilizado no Front-End.
		- Design Patterns utiliazado: 
			- Structural Design Patterns (Explicação conceitual: https://dotnettutorials.net/lesson/facade-design-pattern/):
				- Facade: O Facade Design Pattern é um padrão estrutural que fornece uma interface simplificada para um sistema complexo de classes, bibliotecas ou frameworks. O objetivo principal do padrão Facade 
é apresentar uma interface clara, simplificada e minimizada para os clientes externos, ao mesmo tempo em que delega todas as operações subjacentes complexas para as classes apropriadas dentro do sistema.
				
	
3. **GitHub**:
    - O código deve ser disponibilizado no GitHub em um repositório público.
		- ** WebAPI: https://github.com/tkmaster1/desafioUBC-Core
		- ** Front-end: https://github.com/tkmaster1/desafioUBC-Web

#### Critérios de Avaliação
2. **Qualidade do Código**: O código é limpo, bem organizado e segue as boas práticas de desenvolvimento? ** R: Sim.
4. **UI/UX**: A interface é intuitiva e agradável de usar? ** R: Sim.
5. **Testes**: Existem testes automatizados (unitários/integrados) que cobrem as principais funcionalidades? ** R: Infelizmente não sei fazer.