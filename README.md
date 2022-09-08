# Top Seguros Brasil - Backend | 1.0.4
Backend do projeto de PIM do quarto semestre da Universidade Paulista UNIP

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend)

Projeto criado utilizando a arquitetura limpa (Clean Architecture)

## Definições

- **API:** Contém as entradas da aplicação, Controllers e configurações de ambiente
- **Application:** Contém os use case (regras de negócio) da aplicação, responsável por ditar os fluxos internos
- **CrossCutting:** Contém as regras compartilhadas e códigos útils para as camadas
- **Infra:** Contém os códigos de banco de dados e de infraestrutura do projeto
- **UnitTest:** Contém os testes unitários do projeto

## Tecnologias Empregadas
- Linguagem: C#
- Framework: .NET ASPNet CORE 6
- Database: Microsoft SQL Server

## Instalação

```sh
git clone https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend.git
cd unip_pim_iv_backend
dotnet restore
dotnet run
```

## Conexão com o Front
- Para poder enviar requisições a API basta utilizar o link de deploy https://tsb-portal.herokuapp.com/
- Contará também com a documentação e consulta em tempo real da aplicação com o [Swagger da API](https://tsb-portal.herokuapp.com/swagger/index.html)

## Desenvolvedores
<table>
	<tr>
    	<td align="center">
			<a href="https://github.com/antonio-cajueiro-campos">
				<img src="https://avatars.githubusercontent.com/u/7028783?v=4" width="100px;" alt=""/><br />
				<sub>
					<b>Antonio Carlos</b>
				</sub>
			</a>
			<br />
			<a href="https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend/commits?author=antonio-cajueiro-campos" title="Code">Commits: 💻</a>
		</td>
    	<td align="center">
			<a href="https://github.com/Lucas4985">
				<img src="https://avatars.githubusercontent.com/u/102609797?v=4" width="100px;" alt=""/><br />
				<sub>
					<b>Lucas Fernandes</b>
				</sub>
			</a>
			<br />
			<a href="https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend/commits?author=Lucas4985" title="Code">Commits: 💻</a>
		</td>
    	<td align="center">
			<a href="https://github.com/BHOWXY">
				<img src="https://avatars.githubusercontent.com/u/91236653?v=4" width="100px;" alt=""/><br />
				<sub>
					<b>Maria Eduarda</b>
				</sub>
			</a>
			<br />
			<a href="https://github.com/antonio-cajueiro-campos/unip_pim_iv_backend/commits?author=BHOWXY" title="Code">Commits: 💻</a>
		</td>
	</tr>
</table>