using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;
using TSB.Portal.Backend.Infra.Repository.Entities;

namespace TSB.Portal.Backend.Application.UseCases.GetAllClients;

public class GetAllClientsOutput
{
	public List<Cliente> Clientes { get; set; }
}
