using Microsoft.EntityFrameworkCore;
using TSB.Portal.Backend.Infra.Repository.Entities;

namespace TSB.Portal.Backend.Infra.Repository;
public class DataContext : DbContext, IDataContext
{
	public DbSet<Credential> Credentials { get; set; }
	public DbSet<User> Users { get; set; }
	
	public DataContext() {}
	public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
		System.Console.WriteLine(options.IsConfigured);
        if (!options.IsConfigured)
        {
			options.AddDefaultConnection();
        }
    }
    
}
