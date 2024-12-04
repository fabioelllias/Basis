using Desafio.Application;
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
        public async Task<IActionResult> CriarAutor([FromBody] AutorCriarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Atualiza os dados de um autor existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAutor(int id, [FromBody] AutorAtualizarComand request)
        {
            return Ok(await Mediator.Send(request));
        }

        /// <summary>
        /// Exclui autor pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAutor(int id)
        {
            return Ok(await Mediator.Send(AutorExcluirComand.Create(id)));
        }

        [HttpGet("relatorio")]
        public async Task<IActionResult> Relatorio([FromRoute] ExportAutorReportQuery request)
        {
            var pdfBytes = await Mediator.Send(request);

            return File(pdfBytes, "application/pdf", "AutorReport.pdf");
        }
    }
}