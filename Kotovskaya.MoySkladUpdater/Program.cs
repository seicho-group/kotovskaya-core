using Kotovskaya.DB.Domain.Context;
using Kotovskaya.MoySkladUpdater.Application.MigrationServices;

var dbContext = new KotovskayaDbContext();
var msContext = new KotovskayaMsContext();
var yaContext = new KotovskayaYandexObjectStorageContext();

var prUpdater = new ProductUpdater(msContext, dbContext, yaContext);
var catUpdater = new CategoriesUpdater(msContext, dbContext);
var imgUpdater = new ImagesUpdater(msContext, dbContext, yaContext);

// Console.WriteLine("Categories updating...");
// await catUpdater.Migrate();
// Console.WriteLine("Products updating...");
// await prUpdater.Migrate();
// Console.WriteLine("Images updating...");
// await imgUpdater.Migrate();
