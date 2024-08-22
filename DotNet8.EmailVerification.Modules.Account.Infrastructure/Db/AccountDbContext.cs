namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Tbl_User> Tbl_Users { get; set; }
    public DbSet<Tbl_Setup> Tbl_Setups { get; set; }
}
