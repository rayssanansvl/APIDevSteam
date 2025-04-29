using APIDevSteam.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using APIDevSteam.Models;

namespace APIDevSteam.Data
{
    public class APIContext : IdentityDbContext<Usuario>
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        { }
        // DbSet
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<JogoMidia> JogosMidia { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<JogoCategoria> JogosCategorias { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinhos { get; set; }
        public DbSet<Cupom> Cupons { get; set; }
        public DbSet<CupomCarrinho> CuponsCarrinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Tabelas
            builder.Entity<Jogo>().ToTable("Jogos");
            builder.Entity<JogoMidia>().ToTable("JogosMidia");
            builder.Entity<Categoria>().ToTable("Categorias");
            builder.Entity<JogoCategoria>().ToTable("JogosCategorias");
            builder.Entity<Carrinho>().ToTable("Carrinhos");
            builder.Entity<ItemCarrinho>().ToTable("ItemCarrinhos");
            builder.Entity<Cupom>().ToTable("Cupons");
            builder.Entity<CupomCarrinho>().ToTable("CuponsCarrinhos");
        }
        public DbSet<APIDevSteam.Models.CupomCarrinho> CupomCarrinho { get; set; } = default!;
    }
}
