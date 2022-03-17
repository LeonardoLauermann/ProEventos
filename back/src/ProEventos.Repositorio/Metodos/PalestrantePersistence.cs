using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Metodos
{
    public class PalestrantePersistente : IPalestrantePersistence
    {
        private readonly ProEventosContext context;

        public PalestrantePersistente(ProEventosContext context)
        {
            this.context = context;
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