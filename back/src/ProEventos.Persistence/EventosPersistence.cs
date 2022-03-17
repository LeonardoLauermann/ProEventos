using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventosPersistence : IEventosPersistence
    {
        private readonly ProEventosContext context;

        public EventosPersistence(ProEventosContext context)
        {
            this.context = context;
        }
        
        public void Add<T>(T entity) where T : class
        {
            this.context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            this.context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            this.context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await this.context.SaveChangesAsync()) > 0;
        }

//------------------------------------- EVENTO -----------------------------------------------\\  

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {                                            //dado o evento "e", vai ser incluido nos Lotes/RedesSociais
            IQueryable<Evento> query = this.context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
                //se o includePalestrantes for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos     
            if(includePalestrantes){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = this.context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
                //se o includePalestrantes for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos                
            if(includePalestrantes){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
                                    //A cada evento que tiver, procura o tema, converte para lower e analisa se contem um tema convertido em lower 
            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = this.context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
                //se o includePalestrantes for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos
            if(includePalestrantes){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(e => e.PalestrantesEventos).ThenInclude(pe => pe.Palestrante);
            }
                                    
            query = query.OrderBy(e => e.Id).Where(e => e.Id == eventoId);
                                //retornando apenas 1   
            return await query.FirstOrDefaultAsync();
        }

//------------------------------------- PALESTRANTES -----------------------------------------------\\  
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);
                //se o IncludeEventos for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos
            if(includeEventos){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
            //ordenar Pelo Id
            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);
                //se o IncludeEventos for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos
            if(includeEventos){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
                                    //A cada evento que tiver, procura o tema, converte para lower e analisa se contem um tema convertido em lower 
            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }


        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = this.context.Palestrantes.Include(p => p.RedesSociais);
                //se o IncludeEventos for positivo, ele irá entrar no IF para adicionar Evento no PalestranteEventos
            if(includeEventos){
            //Dado o PalestranteEventos, Irá ser adicionado o Palestrante
            query = query.Include(p => p.PalestrantesEventos).ThenInclude(pe => pe.Evento);
            }
                                    
            query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);
                                //retornando apenas 1   
            return await query.FirstOrDefaultAsync();
        }


    }
}