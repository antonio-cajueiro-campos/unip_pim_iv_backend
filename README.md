# unip_pim_iv_backend
Backend do projeto de PIM do quarto semestre da Universidade Paulista UNIP

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend)

Projeto criado utilizando a arquitetura limpa (Clean Architecture)

# Definições

## API 
Contém as entradas da aplicação, Controllers e configurações de ambiente

## Application
Contém os use case (regras de negócio) da aplicação, responsável por ditar os fluxos internos

## CrossCutting
Contém as regras compartilhadas e códigos útils para as camadas

## Infra
Contém os códigos de banco de dados e de infraestrutura do projeto

## UnitTest
Contém os testes unitários do projeto

## Tecnologias Empregadas
- Linguagem: C#
- Framework: .NET ASPNet CORE 6
- Database: SQL Server

## Instalação

```sh
git clone https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend.git
cd unip_pim_iv_backend
dotnet restore
dotnet run
```

# Banco de dados
## Acesso
- Para poder obter acesso ao banco de dados do projeto irá ser necessário a [Cloud SQL Proxy da Google](https://cloud.google.com/sql/docs/mysql/sql-proxy#windows-64-bit)
- Entre em contato com o EM do projeto https://github.com/antonio-cajueiro-campos para mais informações

## Conectando
- Após ter obtido os arquivos necessários para a conexão, execute o comando que está dentro do tsb-gcp-proxy
- Uma vez executado, a conexão com o banco de dados irá ser válida para localhost (127.0.0.1:1433)

# Conexão com o Front
- Para poder enviar requisições a API basta utilizar o link de deploy https://tsb-portal.herokuapp.com/
- Contará também com a documentação e consulta em tempo real da aplicação com o [Swagger da API](https://tsb-portal.herokuapp.com/swagger/index.html)
