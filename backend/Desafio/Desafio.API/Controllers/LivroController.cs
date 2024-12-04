﻿using Desafio.Application;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API
{
    [Route("livro")]
    public class LivroController : ControllerBaseLocal
    {
        /// <summary>
        /// Lista todos os livros.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Livros([FromRoute] LivroListarTodosQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Obtém um livro pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterLivro(int id)
        {
            return Ok(await Mediator.Send(LivroObterPorIdQuery.Create(id)));
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarLivro([FromBody] LivroCriarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Atualiza os dados de um livro existente.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> AtualizarLivro([FromBody] LivroAtualizarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Exclui livro pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            return Ok(await Mediator.Send(LivroExcluirComand.Create(id)));
        }
    }
}