﻿using Desafio.Application;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API
{
    [Route("autor")]
    public class AutorController : ControllerBaseLocal
    {
        /// <summary>
        /// Lista todos os autores.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Autores([FromRoute] AutorListarTodosQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Obtém um autor pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAutor(int id)
        {
            return Ok(await Mediator.Send(AutorObterPorIdQuery.Create(id)));
        }

        /// <summary>
        /// Cria um novo autor
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarAutor([FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Atualiza os dados de um autor existente.
        /// </summary>
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> AtualizarAutor([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Exclui autor pelo ID.
        /// </summary>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> ExcluirAutor([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }
    }
}