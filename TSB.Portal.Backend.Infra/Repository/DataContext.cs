using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Infra.Repository.Entities;

namespace TSB.Portal.Backend.Infra.Repository;
public class DataContext : DbContext, IDataContext
{
	public DbSet<Credential> Credentials { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Apolice> Apolices { get; set; }
	public DbSet<Cliente> Clientes { get; set; }
	public DbSet<Cobertura> Coberturas { get; set; }
	public DbSet<Endereco> Enderecos { get; set; }
	public DbSet<Funcionario> Funcionarios { get; set; }
	public DbSet<HistoricoPagamento> HistoricoPagamentos { get; set; }
	public DbSet<HistoricoSinistro> HistoricoSinistros { get; set; }
	public DbSet<Sinistro> Sinistros { get; set; }
	
	public DataContext() {}
	public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
			options.AddDefaultConnection();
        }
    }
    
}
