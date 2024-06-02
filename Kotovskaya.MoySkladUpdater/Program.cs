using Kotovskaya.DB.Domain.Context;
using Kotovskaya.MoySkladUpdater.Application.MigrationServices;

var dbContext = new KotovskayaDbContext();
var msContext = new KotovskayaMsContext();

var prUpdater = new ProductUpdater(msContext, dbContext);
var catUpdater = new CategoriesUpdater(msContext, dbContext);

Console.WriteLine("Categories updating...");
await catUpdater.Migrate();
Console.WriteLine("Products updating...");
await prUpdater.Migrate();
