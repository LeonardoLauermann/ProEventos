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



    }
}