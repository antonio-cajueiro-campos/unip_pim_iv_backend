using System.Security.Claims;
using TSB.Portal.Backend.Infra.Repository.Entities;

namespace TSB.Portal.Backend.Application.UseCases.ChangeUserData;
public class ChangeUserDataInput
{
    public User User { get; set; }
    public Funcionario Funcionario { get; set; }
    public Cliente Cliente { get; set; }
    public ClaimsPrincipal  ClaimsPrincipal  { get; set; }
}