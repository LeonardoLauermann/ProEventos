using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence.Context;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventosService eventosService;
        public EventosController(IEventosService eventosService) //alterado aqui, que ele começa a receber o IEventosService
        {
            this.eventosService = eventosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() //IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)
        {
            try
            {
                var eventos = await this.eventosService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Nenhum Evento encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]   //criado esse novo get porque ele vai receber o ID
        public async Task<IActionResult> GetById(int id)  //IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)

        {
            try
            {
                var evento = await this.eventosService.GetEventoByIdAsync(id, true);
                if(evento == null) return NotFound("Evento por Id não encontrado");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]   //criado esse novo get porque ele vai receber o string
        public async Task<IActionResult> GetByTema(string tema) //IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)
        {
            try
            {
                var eventos = await this.eventosService.GetAllEventosByTemaAsync(tema, true);
                if(eventos == null) return NotFound("Eventos por tema não encontrado");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)//IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)
        {
            try
            {
                var evento = await this.eventosService.AddEventos(model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Eventos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")] //recebe o parametro de ID
        public async Task<IActionResult> Put(int id, Evento model)//IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)
        {
            try
            {
                var evento = await this.eventosService.UpdateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar Eventos. Erro: {ex.Message}");
            }
        }



        [HttpDelete("{id}")] //recebe o parametro de ID
        public async Task<IActionResult> delete(int id)//IActionResult nos permite retornar o statuscode do http(100-200-300-400-500)
        {
            try
            { 
            return await this.eventosService.DeleteEvento(id) ? Ok("Deletado") : BadRequest("Evento não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Eventos. Erro: {ex.Message}");
            }
        }
    }
}
