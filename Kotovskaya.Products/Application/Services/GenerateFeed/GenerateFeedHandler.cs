using System.Xml;
using Kotovskaya.DB.Domain.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kotovskaya.Products.Application.Services.GenerateFeed;

public class GenerateFeedHandler(KotovskayaDbContext dbContext) : IRequestHandler<GenerateFeedRequest, string>
{
    public Task<string> Handle(GenerateFeedRequest request, CancellationToken cancellationToken)
    {
        // Создание XML документа
        var xmlDoc = new XmlDocument();
        var rootElement = xmlDoc.CreateElement("yml_catalog");
        rootElement.SetAttribute("date", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
        xmlDoc.AppendChild(rootElement);

        var shopElement = xmlDoc.CreateElement("shop");

        // name, company, url
        var shopNameElement = xmlDoc.CreateElement("name");
        var shopCompanyElement = xmlDoc.CreateElement("company");
        var shopUrlElement = xmlDoc.CreateElement("url");
        shopNameElement.InnerText = "Мыловарня Мадам Котовской";
        shopCompanyElement.InnerText = "ИП Осинцева Мария Алексеевна";
        shopUrlElement.InnerText = "https://mkotovskaya.ru";

        // currencies
        var shopCurrenciesElement = xmlDoc.CreateElement("currencies");
        var shopCurrencyElement = xmlDoc.CreateElement("currency");
        shopCurrencyElement.SetAttribute("id", "RUB");
        shopCurrencyElement.SetAttribute("rate", "1");
        shopCurrenciesElement.AppendChild(shopCurrencyElement);

        // categories
        var categoriesElement = xmlDoc.CreateElement("categories");
        var index = 0;
        foreach (var category in new[]
                     { "Наборы для мыловарения", "Мыловарение", "Хобби и творчество", "Детские товары", "Все товары" })
        {
            index++;
            var categoryElement = xmlDoc.CreateElement("category");
            categoryElement.InnerText = category;
            categoryElement.SetAttribute("id", index.ToString());
            if (index != 5) categoryElement.SetAttribute("parentId", (index + 1).ToString());
            categoriesElement.AppendChild(categoryElement);
        }

        // products
        var offersElement = xmlDoc.CreateElement("offers");
        var products = dbContext.Products.Include(productEntity => productEntity.SaleTypes).ToList();
        foreach (var product in products)
        {
            var offerElement = xmlDoc.CreateElement("offer");
            offerElement.SetAttribute("id", product.Id.ToString());
            offerElement.SetAttribute("available", "true");
            var nameElement = xmlDoc.CreateElement("name");
            nameElement.InnerText = product.Name;
            var urlElement = xmlDoc.CreateElement("url");
            urlElement.InnerText = $"https://mkotovskaya.ru/product/{product.Id}";
            var priceElement = xmlDoc.CreateElement("price");
            priceElement.InnerText = product.SaleTypes?.Price.ToString() ?? "0";
            var currencyIdElement = xmlDoc.CreateElement("currencyId");
            currencyIdElement.InnerText = "RUB";
            var categoryIdElement = xmlDoc.CreateElement("categoryId");
            categoryIdElement.InnerText = "1";
            var descriptionElement = xmlDoc.CreateElement("categoryId");
            descriptionElement.InnerText = product.Description ?? "";
            var pictureElement = xmlDoc.CreateElement("picture");
            pictureElement.InnerText = $"https://storage.yandexcloud.net/kotovskaya.products/{product.MsId}/0.jpg";
            offerElement.AppendChild(nameElement);
            offerElement.AppendChild(descriptionElement);
            offerElement.AppendChild(urlElement);
            offerElement.AppendChild(priceElement);
            offerElement.AppendChild(currencyIdElement);
            offerElement.AppendChild(categoryIdElement);
            offerElement.AppendChild(pictureElement);
            offersElement.AppendChild(offerElement);
        }

        shopElement.AppendChild(shopNameElement);
        shopElement.AppendChild(shopCompanyElement);
        shopElement.AppendChild(shopUrlElement);
        shopElement.AppendChild(shopCurrenciesElement);
        shopElement.AppendChild(categoriesElement);
        rootElement.AppendChild(shopElement);
        rootElement.AppendChild(offersElement);

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
