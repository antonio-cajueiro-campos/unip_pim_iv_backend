using System.Security.Claims;
using System.Text.Json.Serialization;
using TSB.Portal.Backend.CrossCutting.Validation;

namespace TSB.Portal.Backend.Application.UseCases.ChangeUserData;
public class ChangeUserDataInput
{
    public string Name { get; set; }
    
    [Document]
	public string Document { get; set; }
    public string Telefone { get; set; }
    public string Cargo { get; set; }
    public string ChavePIX { get; set; }
	public string Username { get; set; }
    public string Password { get; set; }
    public string CEP { get; set; }
	public string Cidade { get; set; }
	public string Estado { get; set; }
	public string Bairro { get; set; }
	public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    
    [JsonIgnore]
    public ClaimsPrincipal  ClaimsPrincipal  { get; set; }
}