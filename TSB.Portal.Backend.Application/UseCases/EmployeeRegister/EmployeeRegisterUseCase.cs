
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.EmployeeRegister;
public class EmployeeRegisterUseCase : IDefaultUseCase<EmployeeRegisterOutput, EmployeeRegisterInput>
{
	private readonly DataContext database;

	public EmployeeRegisterUseCase(DataContext database)
	{
		this.database = database;
	}

	public DefaultResponse<EmployeeRegisterOutput> Handle(EmployeeRegisterInput authenticateInput)
	{
		return this.EmployeeRegister(authenticateInput);
	}


	public DefaultResponse<EmployeeRegisterOutput> EmployeeRegister(EmployeeRegisterInput authenticateInput) {


		//dawda this.database
		return new();
	}

	
}