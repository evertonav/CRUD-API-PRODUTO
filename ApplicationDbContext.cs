using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext {
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Produto>()
            .Property(p => p.Nome).HasMaxLength(50).IsRequired(false);
        
        builder.Entity<Categoria>().ToTable("Categorias");
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseNpgsql("server=localhost;Port=5432;userid=sageuser;password=s4g3u53r;database=ProdutosExercicio");
}
