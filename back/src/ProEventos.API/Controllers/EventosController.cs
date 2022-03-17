using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Domain;
using ProEventos.Repositorio;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly ProEventosContext context;
        public EventosController(ProEventosContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return this.context.Eventos;
        }
        [HttpGet("{id}")]   //criado esse novo get porque ele vai receber o ID
        public Evento GetById(int id)
        {
            return this.context.Eventos.FirstOrDefault(evento => evento.Id == id);
        }
        [HttpPost]
        public string Post()
        {
            return "Exemplo de Post";
        }

        [HttpPut("{id}")] //recebe o parametro de ID
        public string Put(int id)
        {
            return $"Exemplo de Put {id}";
        }
        [HttpDelete("{id}")] //recebe o parametro de ID
        public string delete(int id)
        {
            return $"Exemplo de delete {id}";
        }
    }
}
