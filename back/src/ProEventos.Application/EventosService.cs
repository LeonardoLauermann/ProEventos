using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventosService : IEventosService
    {
        private readonly IGeralPersistence geralPersistence;
        private readonly IEventosPersistence eventosPersistence;
        public EventosService(IGeralPersistence geralPersistence, IEventosPersistence eventosPersistence)
        {
            this.eventosPersistence = eventosPersistence;
            this.geralPersistence = geralPersistence;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                this.geralPersistence.Add<Evento>(model); //vai adicionar no evento, o model, que é o  valor que está recebendo
                if (await this.geralPersistence.SaveChangesAsync())
                { //caso ele salve, vai retornar o ID do nosso model
                    return await this.eventosPersistence.GetEventoByIdAsync(model.Id, false);//tendo como retorno, o item que foi modificado
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //vai retornar a mensagem de erro 
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await this.eventosPersistence.GetEventoByIdAsync(eventoId, false); //dentro do eventosPersistence, ele vai buscar/receber o valor de eventoId(que é um parametro)
                if (evento == null) return null; //se não for retornado ninguem, o evento vai receber null

                model.Id = evento.Id; //se foi encontrado algum elemento para eventoId, ele irá passar para o model tambem

                this.geralPersistence.Update(model); //vai atualizar 
                if (await this.geralPersistence.SaveChangesAsync())
                { //caso ele salve, vai retornar o ID do nosso model
                    return await this.eventosPersistence.GetEventoByIdAsync(model.Id, false);//tendo como retorno, o item que foi modificado 
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    //vai retornar a mensagem de erro 
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await this.eventosPersistence.GetEventoByIdAsync(eventoId, false);  //dentro do eventosPersistence, ele vai buscar/receber o valor de eventoId(que é um parametro)
                if (evento == null) throw new Exception("Evento para delete não foi encontrado."); //se não for retornado ninguem, o evento vai retornar mensagem 
                this.geralPersistence.Delete<Evento>(evento); //vai deletar   
                                                              //ele não vai ter ninguem para retornar porque está deletando, por isso vai salvar 
                return await this.geralPersistence.SaveChangesAsync(); //vai retornar salvando as mudanças
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    //vai retornar a mensagem de erro 
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await this.eventosPersistence.GetAllEventosAsync(includePalestrantes);      //dentro do eventosPersistence, ele vai buscar/receber o valor de includePalestrantes(que é um parametro)
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    //vai retornar a mensagem de erro 
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await this.eventosPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);  //dentro do eventosPersistence, ele vai buscar/receber o valor de tema/includePalestrantes(que é um parametro)
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    //vai retornar a mensagem de erro 
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await this.eventosPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);  //dentro do eventosPersistence, ele vai buscar/receber o valor de eventoId/includePalestrantes(que é um parametro)
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    //vai retornar a mensagem de erro 
            }
        }


    }
}