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
        public async Task<IActionResult> Livros([FromRoute] object request)
        {
            return Ok(Mediator.Send(request));
        }

        /// <summary>
        /// Obtém um livro pelo ID.
        /// </summary>
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> ObterLivro([FromRoute] object request)
        {
            return Ok(Mediator.Send(request));
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarLivro([FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Atualiza os dados de um livro existente.
        /// </summary>
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> AtualizarLivro([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Exclui livro pelo ID.
        /// </summary>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> ExcluirLivro([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }
    }
}