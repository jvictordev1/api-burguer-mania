# BurguerMania API

## Descrição do Projeto

A BurguerMania API é a solução back-end para o sistema de gestão de pedidos de uma hamburgueria. Desenvolvida utilizando C# e Entity Framework, a API permite que os usuários visualizem e interajam com o cardápio, que inclui categorias de hambúrgueres e seus respectivos ingredientes. A API também gerencia a criação de pedidos, incluindo detalhes como nome do produto, quantidade e observações. A estrutura foi projetada para integração com o front-end, permitindo uma experiência de compra interativa e responsiva.

## Instruções de Como Executar a Aplicação

Siga os passos abaixo para executar a API localmente. Certifique-se de que você tenha o .NET 8.0 ou superior instalado em sua máquina.

### Configuração da API

1. Clone o repositório:

   ```bash
   git clone https://github.com/usuario/burguer-mania-api.git
   ```

2. Acesse a pasta do projeto:

   ```bash
   cd burguer-mania-api
   ```

3. Configure a string de conexão do banco de dados **PostgreSQL** no arquivo `appsettings.json`. Altere o valor da chave `ConnectionStrings:DefaultConnection` para o seu banco de dados:

   ```json
   {
     {
        "DefaultConnection": "Host=localhost;Port=Porta do banco;Database=burguerMania;Username=Nome de usuario do banco;Password=senha do banco, se houver"
     }
   }
   ```

4. Instale as dependências do projeto:

   ```bash
   dotnet restore
   ```

5. Para criar o banco de dados e aplicar as migrações, execute:

   ```bash
   dotnet ef database update
   ```

6. Inicie a API:

   ```bash
   dotnet run
   ```

A API estará disponível em `http://localhost:5031`.

### Testando a API

Você pode testar os endpoints da API utilizando ferramentas como o Postman ou cURL.

Exemplo de requisição para listar todos os hambúrgueres:

```bash
GET http://localhost:5000/api/hamburgueres
```

## Tecnologias Utilizadas

- C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger

## Possíveis Melhorias Futuras

- Implementar autenticação e autorização com JWT para proteger os endpoints.
- Adicionar endpoints para personalização de hambúrgueres (escolha de ingredientes extras).
- Implementar um sistema de pagamento integrado.
- Adicionar avaliações e comentários dos usuários para os hambúrgueres.
- Adicionar suporte para múltiplos idiomas.
