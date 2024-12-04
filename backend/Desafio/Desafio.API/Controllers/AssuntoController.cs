using Desafio.Application;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API
{
    [Route("assunto")]
    public class AssuntoController : ControllerBaseLocal
    {
        /// <summary>
        /// Lista todos os assuntoes.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Assuntos([FromRoute] AssuntoListarTodosQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Obtém um assunto pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterAssunto(int id)
        {
            return Ok(await Mediator.Send(AssuntoObterPorIdQuery.Create(id)));
        }

        /// <summary>
        /// Cria um novo assunto
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarAssunto([FromBody] AssuntoCriarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Atualiza os dados de um assunto existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAssunto(int id, [FromBody] AssuntoAtualizarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Exclui assunto pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAssunto(int id)
        {
            return Ok(await Mediator.Send(AssuntoExcluirComand.Create(id)));
        }
    }
}