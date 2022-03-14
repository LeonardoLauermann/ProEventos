using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {//nome IEnumerable<Evento> porque o nome Evento vem da Model Evento
        public IEnumerable<Evento> _evento = new Evento[] {     //Retirado do GET e transoformado em um array
            new Evento() {
                EventoId = 1,
                Tema = "Angular 11 e .NET 5",
                Local = "Belo Horizonte",
                Lote = "1",
                QtdPessoas = 250,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemURL = "a.png"
            },
            new Evento() {
                EventoId = 2,
                Tema = "Angular e Suas Novidades",
                Local = "SP",
                Lote = "2",
                QtdPessoas = 450,
                DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                ImagemURL = "ab.png"
            }
        };

        public EventoController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }
        [HttpGet("{id}")]   //criado esse novo get porque ele vai receber o ID
        public IEnumerable<Evento> GetById(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
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
