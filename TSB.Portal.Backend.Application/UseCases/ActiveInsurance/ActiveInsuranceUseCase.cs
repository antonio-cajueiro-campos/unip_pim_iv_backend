using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using Microsoft.EntityFrameworkCore;

namespace TSB.Portal.Backend.Application.UseCases.ActiveInsurance;

public class ActiveInsuranceUseCase : IDefaultUseCase<ActiveInsuranceOutput, ActiveInsuranceInput>
{
	private readonly DataContext database;

	public ActiveInsuranceUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<ActiveInsuranceOutput> Handle(ActiveInsuranceInput activeInsurance)
	{
		return this.ActiveInsurance(activeInsurance);
	}

	private DefaultResponse<ActiveInsuranceOutput> ActiveInsurance(ActiveInsuranceInput activeInsurance)
	{
		try
		{
			var apolice = this.database.Apolices
				.Include(a => a.Cobertura)
				.Include(a => a.Cliente)
				.FirstOrDefault(a => a.Cliente.Id == activeInsurance.Id);

			if (apolice == null) return new()
			{
				Status = 404,
				Error = false,
				Message = Messages.ApoliceNotFound
			};

			var sinistro = this.database.Sinistros
				.FirstOrDefault(s => s.Tipo == (activeInsurance.Tipo).ToString() && s.CoberturaId == apolice.Cobertura.Id);

			if (sinistro == null) return new()
			{
				Status = 404,
				Error = false,
				Message = Messages.SinistroNotFound
			};

			var hs = new HistoricoSinistro();

			hs.Cliente = apolice.Cliente;
			hs.Sinistro = sinistro;
			hs.Ocorrencia = DateTime.Now;
			hs.Valor = sinistro.ValorSinistro;

			this.database.HistoricoSinistros.Add(hs);
			this.database.SaveChanges();

			return new()
			{
				Status = 200,
				Error = false,
				Message = Messages.Updated
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				Status = 500,
				Error = true,
				Message = Messages.Error + ex,
				Data = null
			};
		}
	}
}
