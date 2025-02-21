# Gerenciamento de Cidadãos - SQLite e C#

## Descrição
Este projeto é uma aplicação console desenvolvida em C# com foco no **Windows 11**, permitindo o gerenciamento de cidadãos em um banco de dados SQLite. Foi projetada para utilizar **emojis e personalizações do console**, proporcionando uma experiência visual aprimorada.  
Apesar de oferecer suporte a outros sistemas operacionais e versões do Windows, a **UI pode apresentar inconsistências** dependendo do ambiente.

Inclui funcionalidades para criação, leitura, atualização e remoção de registros, garantindo a consistência dos dados, como a unicidade do CPF.

## Instruções para rodar a aplicação no **Windows 11**
(Em outras versões do Windows, pode haver inconsistências quanto à UI)

1. Instale o SDK do .NET 8.0 com o comando:  
    ```sh
    winget install Microsoft.DotNet.SDK.8
    ```

2. Feche o terminal e abra-o novamente na pasta do projeto.

3. Restaure as dependências do projeto com:  
    ```sh
    dotnet restore
    ```

4. Aguarde até que as dependências sejam restauradas e, então, rode o projeto com:  
    ```sh
    dotnet run
    ```

## Instruções para rodar a aplicação no **Linux (Ubuntu/Debian)**

1. Atualize os pacotes e instale o SDK do .NET 9.0 com o comando:
    ```sh
    sudo apt-get update && \
      sudo apt-get install -y dotnet-sdk-9.0
    ```

2. Instale o runtime do ASP.NET Core:
    ```sh
    sudo apt-get update && \
      sudo apt-get install -y aspnetcore-runtime-9.0
    ```

3. Instale o runtime do .NET caso necessário:
    ```sh
    sudo apt-get install -y dotnet-runtime-9.0
    ```

4. Na pasta do projeto, rode o comando:
    ```sh
    dotnet run
    ```

## Funcionalidades
- Criar uma tabela `Cidadaos` no banco de dados SQLite.
- Inserir novos cidadãos com nome e CPF (CPF armazenado sem máscaras).
- Validar e formatar CPFs.
- Verificar se um CPF já existe antes da inserção.
- Atualizar nome e/ou CPF de um cidadão com base no ID.
- Excluir registros com base no ID.
- Consultar cidadãos por diferentes critérios.

## Tecnologias Utilizadas
- **Linguagem:** C#
- **Banco de Dados:** SQLite

## Observações
- O CPF é armazenado sem caracteres especiais, garantindo um padrão consistente.
- As consultas são parametrizadas para evitar SQL Injection.
- A atualização permite modificar apenas o nome, apenas o CPF ou ambos.
- **Boas práticas de design:**  
  - **Validação de dados:** O uso de validações para o CPF assegura que apenas dados consistentes sejam inseridos no banco. Esse tipo de validação segue boas práticas de design e segurança.
  - **SQL parametrizado:** As consultas SQL são feitas de forma parametrizada, evitando SQL Injection, o que é uma prática recomendada para segurança.
  - **Principais princípios de design:** O código segue conceitos de baixo acoplamento e alta coesão, pois as funcionalidades estão bem separadas (ex.: validação de CPF, manipulação de dados no banco, etc.), facilitando a manutenção e a evolução do projeto.
  - **Testes automatizados:** O projeto inclui testes automatizados para garantir o funcionamento correto das funcionalidades. Estes testes são fornecidos caso o usuário precise, mas não são obrigatórios para rodar a aplicação.

Este projeto foi desenvolvido para praticar manipulação de banco de dados SQLite com C#, incluindo boas práticas de validação de dados e uso de comandos SQL parametrizados.

> **Este projeto foi desenvolvido como solução ao desafio CIENTEC.**

