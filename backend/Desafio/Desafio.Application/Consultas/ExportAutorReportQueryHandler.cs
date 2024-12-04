using Desafio.Core;
using Desafio.Infrastructure;
using MediatR;
using QuestPDF.Fluent;

namespace Desafio.Application
{
    public class ExportAutorReportQueryHandler : IRequestHandler<ExportAutorReportQuery, byte[]>
    {
        private readonly IAutorRepository _repository;

        public ExportAutorReportQueryHandler(IAutorRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Handle(ExportAutorReportQuery request, CancellationToken cancellationToken)
        {
            var reportData = _repository.Listar();

            var document = new AutorReportDocument(reportData);

            using var pdfStream = new MemoryStream();
            document.GeneratePdf(pdfStream);
            return pdfStream.ToArray();
        }
    }
}