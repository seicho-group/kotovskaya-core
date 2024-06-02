using Kotovskaya.DB.Domain.Context;
using Kotovskaya.MoySkladUpdater.Application.MigrationServices;

var dbContext = new KotovskayaDbContext();
var msContext = new KotovskayaMsContext();

var prUpdater = new ProductUpdater(msContext, dbContext);

await prUpdater.Migrate();
