using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Application.Transport;
using TSB.Portal.Backend.CrossCutting.Constants;
using TSB.Portal.Backend.CrossCutting.Extensions;
using TSB.Portal.Backend.Infra.Repository;

namespace TSB.Portal.Backend.Application.UseCases.ChangeUserData;
public class ChangeUserDataUseCase : IDefaultUseCase<ChangeUserDataOutput, ChangeUserDataInput>
{
    private object changeUserDataInput;

    private DataContext database { get; set; }
    public ChangeUserDataUseCase(DataContext database)
    {
        this.database = database;
    }
    public DefaultResponse<ChangeUserDataOutput> Handle(ChangeUserDataInput changeUserData)
    {
        return this.ChangeUserData(changeUserData);
    }
    public DefaultResponse<ChangeUserDataOutput> ChangeUserData(ChangeUserDataInput changeUserData)
    {

        if (changeUserData.User == null && (changeUserData.Cliente == null || changeUserData.Funcionario == null))
        {
            return new DefaultResponse<ChangeUserDataOutput>
            {
                Status = 400,
                Error = true,
                Message = Messages.BadRequest,
            };
        }
        try
        {
            var UserId = changeUserData.ClaimsPrincipal.GetUserId();
            var user = this.database.Users.Find(UserId);
            user.Name = changeUserData.User.Name;
            database.Users.Update(user);
            database.SaveChanges();

            return new()
            {
                Error = false,
                Status = 200,
                Message = Messages.Updated,
                Data = null
            };
        }

        catch (Exception ex)
        {
            return new()
            {
                Status = 500,
                Error = true,
                Data = null,
                Message = Messages.Error + ex
            };
        }
    }
}
