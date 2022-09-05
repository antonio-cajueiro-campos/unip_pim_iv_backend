using System.ComponentModel.DataAnnotations;

namespace TSB.Portal.Backend.Infra.Repository.Entities;
public class Funcionario : User
{
    public DateTime DataNasc { get; set; }
    public string Cargo { get; set; }
}