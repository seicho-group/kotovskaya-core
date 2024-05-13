using Confiti.MoySklad.Remap.Api;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Application.Services.MoySkladMigration;
using Kotovskaya.DB.Domain.Context;

DotNetEnv.Env.TraversePath().Load();

Console.WriteLine("Password: ");
var pw = Console.ReadLine();
if (pw != Environment.GetEnvironmentVariable("MIGRATION_PASSWORD"))
{
    throw new Exception("Password check fault");
}
// setting up controllers
var categoriesController = new CategoriesMoySkladMigrationController();
var productsController = new ProductsMoySkladMigrationController();

var controllers = new List<IMigrationController<MoySkladApi, KotovskayaDbContext>>() { categoriesController, productsController };

// migrating to database using all controllers
using (var dbContext = new KotovskayaDbContext())
{
    var integrator = new MoySkladMigrator(dbContext);
    await integrator.Migrate(controllers);
}
