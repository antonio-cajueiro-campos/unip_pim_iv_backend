using TSB.Portal.Backend.Application.Transport;

namespace TSB.Portal.Backend.Application.UseCases.Authenticate.Interfaces;
public interface IDefaultUseCase<Output, Input>
{
	public DefaultResponse<Output> Handle(Input credentials);
}