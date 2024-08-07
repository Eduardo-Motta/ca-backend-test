**Nexer: ca-backend-test**

---------------------
Tecnologias utilizadas
---------------------
* .NET Core 6.0
* Entity Framework
* FluentValidation
* xUnit
* Logger
* Docker
* Docker Compose
* PostgreSQL
* Swagger

---------------------
Execução do projeto
---------------------
1. **Executando o docker-compose todos os scripts e containers necessários serão configurados**:

    No terminal, execute o comando:
    ```shell
    docker-compose up --build
    ```
    
2. **Após o build, acesso o link abaixo**
- http://localhost:8080/swagger/index.html

---------------------
Endpoints
---------------------

### /api/Billings/importBillings
- POST: Importa os dados de faturamento da API externa para o banco de dados local

### /api/Customer
- GET: Retorna uma lista com todos os clientes
- POST: Cria um novo cliente

### /api/Customer/{id}
- GET: Retorna os dados do cliente especificado
- PUT: Atualiza os dados do cliente especificado
- DELETE: Remove o cliente especificado

### /api/Product
- GET: Retorna uma lista com todos os produtos
- POST: Cria um novo produto.

### /api/Product/{id}
- GET: Retorna os dados do produto especificado
- PUT: Atualiza os dados do produto especificado
- DELETE: Remove o produto especificado

---------------------
Criar uma API REST para gerenciar faturamento de clientes.
---------------------
**Funcionalidades 🛠️**

* Customer: CRUD; Criar um cadastro do cliente com os seguintes campos:
    * Id;
    * Name;
    * Email;
    * Address;
    * **Todos os campos são de preenchimento obrigatório.**
* Produtos: CRUD; Criar um cadastro de produtos com os seguintes campos:
    * Id;
    * Nome do produto;
    * **Todos os campos são de preenchimento obrigatório.**
* Controle de conferência e importação de billing.
    * Utilizar postman para consulta dos dados da API’s para criação das tabelas de billing e billingLines.
	  * Após consulta, e criação do passo anterior, inserir no banco de dados o primeiro registro do retorno da API de billing para criação de cliente e produto através do swagger ou dataseed.

    * Utilizar as API’s para consumo dos dados a partir da aplicação que está criada e fazer as seguintes verificações:
      * Se o cliente e o produto existirem, inserir o registro do billing e billingLines no DB local.
      * Caso se o cliente existir ou só o produto existir, deve retornar um erro na aplicação informando sobre a criação do registro faltante.
      * Criar exceptions tratando mal funcionamento ou interrupção de serviço quando API estiver fora.
* Lista de API’s :
	* Get https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing
	* Get https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
	* Post https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing
	* Delete https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
	* PUT https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing/:id
---------------------
