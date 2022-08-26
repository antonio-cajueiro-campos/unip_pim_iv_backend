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



