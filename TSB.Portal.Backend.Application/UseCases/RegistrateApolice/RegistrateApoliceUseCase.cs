using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;
using TSB.Portal.Backend.Infra.Repository.Entities;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.Application.UseCases.RegistrateCobertura;
using TSB.Portal.Backend.Application.UseCases.RegistrateSinistro;
using TSB.Portal.Backend.CrossCutting.Enums;

namespace TSB.Portal.Backend.Application.UseCases.RegistrateApolice;

public class RegistrateApoliceUseCase : IDefaultUseCase<RegistrateApoliceOutput, RegistrateApoliceInput>
{
	private readonly DataContext database;
	private readonly IDefaultUseCase<RegistrateCoberturaOutput, RegistrateCoberturaInput> registrateCobertura;
	private readonly IDefaultUseCase<RegistrateSinistroOutput, RegistrateSinistroInput> registrateSinistro;

	public RegistrateApoliceUseCase(DataContext database,
		IDefaultUseCase<RegistrateCoberturaOutput, RegistrateCoberturaInput> registrateCobertura,
		IDefaultUseCase<RegistrateSinistroOutput, RegistrateSinistroInput> registrateSinistro)
	{
		this.database = database;
		this.registrateCobertura = registrateCobertura;
		this.registrateSinistro = registrateSinistro;
	}

	public DefaultResponse<RegistrateApoliceOutput> Handle(RegistrateApoliceInput registrateApolice)
	{
		return this.RegistrateApolice(registrateApolice);
	}

	private DefaultResponse<RegistrateApoliceOutput> RegistrateApolice(RegistrateApoliceInput registrateApolice)
	{
		try
		{
			if (registrateApolice.ClienteId == 0 || registrateApolice.Sinistros == default || registrateApolice.ValorCobertura == decimal.Zero)
			{
				return new()
				{
					Status = 400,
					Error = true,
					Message = Messages.BadRequest,
					Data = null
				};
			}

			var apoliceDB = this.database.Apolices.FirstOrDefault(a => a.Cliente.Id == registrateApolice.ClienteId);

			if (apoliceDB != null) return ApoliceAlreadyExists();

			var CoberturaOutput = registrateCobertura.Handle(new() {
				ValorCobertura = registrateApolice.ValorCobertura
			});

			//var sinistroSelected = this.database.Sinistros.FirstOrDefault(s => s.Tipo == registrateApolice.Tipo.ToString());

			foreach (KeyValuePair<Sinistros, decimal> entry in registrateApolice.Sinistros)
			{

				var Sinistro = new Sinistro();
				
				Sinistro.Tipo = entry.Key.ToString();
				Sinistro.ValorSinistro = entry.Value;
				Sinistro.CoberturaId = CoberturaOutput.Data.Cobertura.Id;

				this.database.Sinistros.Add(Sinistro);
			}

			var CoberturaDB = database.Coberturas
				.FirstOrDefault(c => c.Id == CoberturaOutput.Data.Cobertura.Id);

			if (CoberturaDB == null) return GetCoberturaDBNotFoundError();

			var ClienteDB = database.Clientes
				.FirstOrDefault(c => c.Id == registrateApolice.ClienteId);

			if (ClienteDB == null) return GetClienteDBNotFoundError();

			var apolice = new Apolice();

			apolice.Emissao = DateTime.Now;
			apolice.Vigencia = DateTime.Now.AddYears(1);
			apolice.Cobertura = CoberturaDB;
			apolice.Cliente = ClienteDB;

			this.database.Apolices.Add(apolice);
			this.database.SaveChanges();

			database.Entry(apolice).GetDatabaseValues();

			return new()
			{
				Status = 201,
				Error = false,
				Message = Messages.Created,
				Data = new() {
					Apolice = apolice
				}
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

	private DefaultResponse<RegistrateApoliceOutput> GetCoberturaDBNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.CoberturaDBNotFound
		};
	}

	private DefaultResponse<RegistrateApoliceOutput> GetClienteDBNotFoundError()
	{
		return new()
		{
			Status = 404,
			Error = true,
			Data = null,
			Message = Messages.ClienteDBNotFound
		};
	}
	private DefaultResponse<RegistrateApoliceOutput> ApoliceAlreadyExists()
	{
		return new()
		{
			Status = 400,
			Error = true,
			Data = null,
			Message = Messages.ApoliceAlreadyExists
		};
	}
}
