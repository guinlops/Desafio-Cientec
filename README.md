# Gerenciamento de Cidadãos - SQLite e C#

## Descrição
Este projeto é uma aplicação em C# que permite o gerenciamento de cidadãos em um banco de dados SQLite.
Inclui funcionalidades para criação, leitura, atualização e remoção de registros, garantindo a consistência dos dados, como a unicidade do CPF.


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

Este projeto foi desenvolvido para praticar manipulação de banco de dados SQLite com C#, incluindo boas práticas de validação de dados e uso de comandos SQL parametrizados.






> **Este projeto foi desenvolvido como solução ao desafio CIENTEC.**
