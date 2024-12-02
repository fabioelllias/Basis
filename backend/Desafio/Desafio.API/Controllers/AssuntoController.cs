using Microsoft.AspNetCore.Mvc;

namespace Desafio.API
{
    [Route("assunto")]
    public class AssuntoController : ControllerBaseLocal
    {
        /// <summary>
        /// Lista todos os Assuntos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Assuntos([FromRoute] object request)
        {
            return Ok(Mediator.Send(request));
        }

        /// <summary>
        /// Obtém um Assunto pelo ID.
        /// </summary>
        [HttpGet, Route("{id}")]
        public async Task<IActionResult> ObterAssunto([FromRoute] object request)
        {
            return Ok(Mediator.Send(request));
        }

        /// <summary>
        /// Cria um novo Assunto
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarAssunto([FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Atualiza os dados de um Assunto existente.
        /// </summary>
        [HttpPut, Route("{id}")]
        public async Task<IActionResult> AtualizarAssunto([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }

        /// <summary>
        /// Exclui Assunto pelo ID.
        /// </summary>
        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> ExcluirAssunto([FromRoute] int id, [FromBody] object request)
        {
            return Ok(Mediator.Send(request).Result);
        }
    }
}