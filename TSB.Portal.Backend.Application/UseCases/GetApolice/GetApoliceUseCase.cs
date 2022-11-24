using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using Microsoft.EntityFrameworkCore;

namespace TSB.Portal.Backend.Application.UseCases.GetApolice;

public class GetApoliceUseCase : IDefaultUseCase<GetApoliceOutput, GetApoliceInput>
{
	private readonly DataContext database;

	public GetApoliceUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetApoliceOutput> Handle(GetApoliceInput apoliceInput)
	{
		return this.GetApolice(apoliceInput);
	}

	private DefaultResponse<GetApoliceOutput> GetApolice(GetApoliceInput apoliceInput)
	{
		try
		{
			var apolice = database.Apolices
				.Include(a => a.Cliente)
				.Include(a => a.Cobertura)
				.FirstOrDefault(a => a.Cliente.Id == apoliceInput.IdCliente);

			if (apolice == null) return GetApoliceNotFoundError();

			var sinistros = database.Sinistros
				.FirstOrDefault(s => s.CoberturaId == apolice.Cobertura.Id);

			var results = database.Sinistros.Where(s => s.CoberturaId == apolice.Cobertura.Id).ToList();
    	

			if (results.Count() == 0) return GetSinistroNotFoundError();

			var sinistrosDict = new Dictionary<string, decimal>();

			foreach(var sinistroResult in results)
			{
				sinistrosDict.Add(sinistroResult.Tipo, sinistroResult.ValorSinistro);
			}
			
			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new()
				{
					ValorCobertura = apolice.Cobertura.ValorCobertura,
					Emissao = apolice.Emissao,
					Vigencia = apolice.Vigencia,
					Sinistros = sinistrosDict
				}
			};
		}
		catch (Exception ex)
		{
			return GetServerError(ex);
		}
	}

	private DefaultResponse<GetApoliceOutput> GetServerError(Exception ex = null)
	{
		return new()
		{
			Status = 500,
			Error = true,
			Message = Messages.Error + ex,
			Data = null
		};
	}

	private DefaultResponse<GetApoliceOutput> GetSinistroNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.SinistroNotFound
		};
	}

	private DefaultResponse<GetApoliceOutput> GetApoliceNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.ApoliceNotFound
		};
	}

	private DefaultResponse<GetApoliceOutput> GetCoberturaNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.CoberturaNotFound
		};
	}
}