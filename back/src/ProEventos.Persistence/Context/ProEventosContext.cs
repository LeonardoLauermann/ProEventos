using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Context
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>().HasKey(PE => new { PE.EventoId, PE.PalestranteId });
            /*modelBuilder possui a entidade PalestranteEvento, onde ele possui chaves e dado um PalestranteEvento
            o PE.EventoId está para PE.PalestranteId
            classe de junção entre eventos e palestrantes*/

            modelBuilder.Entity<Evento>().HasMany(e => e.RedesSociais).WithOne(rs => rs.Evento).OnDelete(DeleteBehavior.Cascade);
            //HasMany(ela tem muitos), WithOne(cada redeSocial, com um evento), OnDelete(quando for deletar, que seja em cascade)
            modelBuilder.Entity<Palestrante>().HasMany(e => e.RedesSociais).WithOne(rs => rs.Palestrante).OnDelete(DeleteBehavior.Cascade);
            //HasMany(ela tem muitos), WithOne(cada redeSocial, com um Palestrante), OnDelete(quando for deletar, que seja em cascade)

        }

    }
}