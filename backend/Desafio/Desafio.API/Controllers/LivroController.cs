using Desafio.Application;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarLivro(int id, [FromBody] LivroAtualizarComand request)
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

        /// <summary>
        /// Lista todas as formas de compra de livros
        /// </summary>
        [HttpGet("forma-compra")]
        public async Task<IActionResult> FormasDeCompra([FromRoute] LivroFormaCompraQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Lista o preço do livro por forma de compra
        /// </summary>
        [HttpGet, Route("{id}/preco")]
        public async Task<IActionResult> Precos(int id)
        {
            return Ok(await Mediator.Send(LivroPrecoQuery.Create(id)));
        }
    }
}