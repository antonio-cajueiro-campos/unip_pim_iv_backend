namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Endereco {
	public string CEP { get; set; }
	public string Cidade { get; set; }
	public string Estado { get; set; }
	public string Bairro { get; set; }
	public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
}