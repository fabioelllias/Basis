using Desafio.Core;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Desafio.Infrastructure;
public class AutorReportDocument : IDocument
{
    private readonly List<AutorReportDto> _data;

    public AutorReportDocument(List<AutorReportDto> data)
    {
        _data = data;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(20);
            page.Size(PageSizes.A4);

            page.Header().Text("Relatório de Livros por Autor").FontSize(20).Bold().AlignCenter();

            page.Content().Column(column =>
            {
                foreach (var group in _data.GroupBy(x => x.Autor))
                {
                    column.Item().Text($"Autor: {group.Key}").FontSize(16).Bold().Underline();

                    foreach (var item in group)
                    {
                        column.Item().Text(
                            $"{item.Livro} - {item.Editora} - {item.AnoPublicacao} - {item.Assuntos}");
                    }

                    column.Item().Text(""); 
                }
            });

            page.Footer().AlignRight().Text($"Gerado em {DateTime.Now:dd/MM/yyyy}");
        });
    }
}

