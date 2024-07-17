using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

var msC = new KotovskayaMsContext();
var dbC = new KotovskayaDbContext();


// var query = new ApiParameterBuilder<ProductFolderQuery>();
// query.Limit(99);
// query.Offset(99 * 1);
// query.Expand().With("PathName");
// var catsResponse = await msC.ProductFolder.GetAllAsync(query);
// var cats = catsResponse.Payload.Rows;
//
// foreach (var cat in cats)
// {
//     var newCat = new Category
//     {
//         Id = new Guid(),
//         Name = cat.Name,
//         MsId = (Guid)cat.Id!,
//         Type = CategoryType.Soapmaking,
//         IsVisible = true,
//         PathName = cat.PathName
//     };
//     dbC.Categories.Add(newCat);
//     await dbC.SaveChangesAsync();
// }

var cats = await dbC.Categories.ToListAsync();
foreach (var cat in cats)
{
    var query = new AssortmentApiParameterBuilder();
    query.Limit(100);
    query.Offset(100 * 0);
    query.Expand().With("Product");
    var categoryPathName = cat.PathName == "" ? cat.Name : $"{cat.PathName}/{cat.Name}";
    query.Parameter("pathname")
        .Should().Be(categoryPathName);

    query.Parameter("stockStore")
        .Should().Be("https://api.moysklad.ru/api/remap/1.2/entity/store/0f06c398-00a0-4db2-af9d-d48d0a02a5b4");

    query.Parameter("quantityMode")
        .Should().Be("positiveOnly");

    var products = await msC.Assortment.GetAllAsync(query);

    foreach (var product in products.Payload.Rows)
    {
        var desc = product.Product?.Description?.Length > 2040 ? "" : product.Product?.Description;
        var newProduct = new ProductEntity()
        {
            Id = (Guid)product.Id!,
            Name = product.Name,
            MsId = (Guid)product.Id!,
            Description = desc,
            Quantity = (int)(product.Quantity ?? 0),
            Categories = [cat],
            ImageLink = $"{product.Id}/0",
            LastUpdatedAt = DateTime.Now.ToUniversalTime()
        };
        dbC.Products.Add(newProduct);
        await dbC.SaveChangesAsync();
    }
}

//https://api.moysklad.ru/api/remap/1.2/entity/productfolder/2ffa54a6-0ed6-11ec-0a80-030f0028984e
