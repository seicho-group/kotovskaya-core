using System.Xml;
using MediatR;

namespace Kotovskaya.Products.Application.Services.GenerateFeed;

public class GenerateFeedHandler : IRequestHandler<GenerateFeedRequest, string>
{
    public Task<string> Handle(GenerateFeedRequest request, CancellationToken cancellationToken)
    {
        // Создание XML документа
        var xmlDoc = new XmlDocument();
        var rootElement = xmlDoc.CreateElement("Root");
        xmlDoc.AppendChild(rootElement);

        var exampleElement = xmlDoc.CreateElement("Example");
        exampleElement.InnerText = "This is an example XML response";
        rootElement.AppendChild(exampleElement);

        // Преобразование XML документа в строку
        using (var stringWriter = new StringWriter())
        using (var xmlTextWriter = XmlWriter.Create(stringWriter))
        {
            xmlDoc.WriteTo(xmlTextWriter);
            xmlTextWriter.Flush();
            return Task.FromResult(stringWriter.GetStringBuilder().ToString());
        }
    }
}
