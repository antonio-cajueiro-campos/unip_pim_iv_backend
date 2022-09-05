namespace TSB.Portal.Backend.Application.Transport;
public interface IDefaultUseCase<Output, Input>
{
	public DefaultResponse<Output> Handle(Input credentials);
}