using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using Microsoft.EntityFrameworkCore;

namespace TSB.Portal.Backend.Application.UseCases.GetSinistro;

public class GetSinistroUseCase : IDefaultUseCase<GetSinistroOutput, GetSinistroInput>
{
	private readonly DataContext database;

	public GetSinistroUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<GetSinistroOutput> Handle(GetSinistroInput sinistroInput)
	{
		return this.GetSinistro(sinistroInput);
	}
	
	private DefaultResponse<GetSinistroOutput> GetSinistro(GetSinistroInput sinistroInput)
	{
		try
		{
			var historicoSinistro = database.HistoricoSinistros
				.Include(hs => hs.Cliente)
				.Include(hs => hs.Sinistro)
				.FirstOrDefault(hs => hs.Cliente.Id == sinistroInput.IdCliente);

			if (historicoSinistro == null) return GetHistoricoSinistrosNotFoundError();
			
			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Success,
				Data = new()
				{
					Valor = historicoSinistro.Valor,
					Ocorrencia = historicoSinistro.Ocorrencia,
					TipoSinistro = historicoSinistro.Sinistro.Tipo
				}
			};
		}
		catch (Exception ex)
		{
			return GetServerError(ex);
		}
	}

	private DefaultResponse<GetSinistroOutput> GetServerError(Exception ex = null)
	{
		return new()
		{
			Status = 500,
			Error = true,
			Message = Messages.Error + ex,
			Data = null
		};
	}

	private DefaultResponse<GetSinistroOutput> GetHistoricoSinistrosNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.HistoricoSinistrosNotFound
		};
	}

	private DefaultResponse<GetSinistroOutput> GetApoliceNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.ApoliceNotFound
		};
	}

	private DefaultResponse<GetSinistroOutput> GetCoberturaNotFoundError()
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