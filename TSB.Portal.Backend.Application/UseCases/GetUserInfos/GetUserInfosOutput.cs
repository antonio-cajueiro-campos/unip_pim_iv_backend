using TSB.Portal.Backend.Application.EntitiesUseCase.DTO;

namespace TSB.Portal.Backend.Application.UseCases.GetUserInfos;

public class GetUserInfosOutput
{
	public FuncionarioDTO Funcionario { get; set; }
	public ClienteDTO Cliente { get; set; }
}
