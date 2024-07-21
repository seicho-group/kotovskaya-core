using Confiti.MoySklad.Remap.Client;
using Kotovskaya.DB.Domain.Context;
using Kotovskaya.DB.Domain.Entities.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

var msC = new KotovskayaMsContext();
var dbC = new KotovskayaDbContext();
var yaC = new KotovskayaYandexObjectStorageContext();


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
// foreach (var cat in cats)
// {
//     var query = new AssortmentApiParameterBuilder();
//     query.Limit(100);
//     query.Offset(100 * 0);
//     query.Expand().With("Product");
//     var categoryPathName = cat.PathName == "" ? cat.Name : $"{cat.PathName}/{cat.Name}";
//     query.Parameter("pathname")
//         .Should().Be(categoryPathName);
//
//     query.Parameter("stockStore")
//         .Should().Be("https://api.moysklad.ru/api/remap/1.2/entity/store/0f06c398-00a0-4db2-af9d-d48d0a02a5b4");
//
//     query.Parameter("quantityMode")
//         .Should().Be("positiveOnly");
//
//     var products = await msC.Assortment.GetAllAsync(query);
//
//     foreach (var product in products.Payload.Rows)
//     {
//         // var desc = product.Product?.Description?.Length > 2040 ? "" : product.Product?.Description;
//         // var newProduct = new ProductEntity()
//         // {
//         //     Id = (Guid)product.Id!,
//         //     Name = product.Name,
//         //     MsId = (Guid)product.Id!,
//         //     Description = desc,
//         //     Quantity = (int)(product.Quantity ?? 0),
//         //     Categories = [cat],
//         //     ImageLink = $"{product.Id}/0",
//         //     LastUpdatedAt = DateTime.Now.ToUniversalTime()
//         // };
//         // dbC.Products.Add(newProduct);
//         var currentDbProduct = await dbC.Products.Where(pr => pr.MsId == (Guid)product.Id!).FirstOrDefaultAsync();
//         if (currentDbProduct != null)
//         {
//             // var saleType = new SaleTypes()
//             // {
//             //     Product = currentDbProduct,
//             //     ProductId = currentDbProduct.Id,
//             //     Price = (int)(product.Product.SalePrices[0].Value ?? 0),
//             //     OldPrice = (int)(product.Product.SalePrices[2].Value ?? 0)
//             // };
//             // currentDbProduct.SaleTypes = saleType;
//         }
//
//         await dbC.SaveChangesAsync();
//     }
// }

// var productsList = await dbC.Products.ToListAsync();
// foreach (var productEntity in productsList)
// {
//     var pr = await msC.Product.Images.GetAllAsync(productEntity.MsId);
//     foreach (var image in pr.Payload.Rows)
//     {
//         var imageDataResponse = await msC.Product.Images.DownloadAsync(image);
//
//         await using (imageDataResponse.Payload)
//         {
//             if (imageDataResponse.Payload is MemoryStream memoryStream)
//             {
//                 var imageData = memoryStream.ToArray();
//
//                 await yaC.ObjectService.PutAsync(imageData, $"{productEntity.MsId}/0.jpg");
//             }
//         }
//     }
// }

foreach (var category in cats)
{
    var path = category.PathName.Split("/");

    var parentCategory = path.Length > 0 ? path[^1] : null;
    if (parentCategory != null)
    {
        var parent = await dbC.Categories.FirstOrDefaultAsync(cat => cat.Name == parentCategory);
        if (parent != null)
        {
            category.ParentCategory = parent;
            await dbC.SaveChangesAsync();
        }
    }
}
