using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IGeralPersistence
    {
        //GERAL
        //T é generico
        //todo e qualquer add,atualizar ou deletar que tivermos, e salvar, vai ser utilizado esses métodos
        void Add<T>(T entity) where T:class;
        void Update<T>(T entity) where T:class;
        void Delete<T>(T entity) where T:class;
        void DeleteRange<T>(T[] entity) where T:class;

        Task<bool> SaveChangesAsync();
    
    }
}