using DotNetEnv;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Application.Services.MoySkladMigration;
using Kotovskaya.DB.Domain.Context;

Env.TraversePath().Load();

Console.WriteLine("Password: ");
var pw = Console.ReadLine();
if (pw != Environment.GetEnvironmentVariable("MIGRATION_PASSWORD")) throw new Exception("Password check fault");

// setting up controllers
var categoriesController = new CategoriesMoySkladMigrationController();
var productsController = new ProductsMoySkladMigrationController();
var saleTypesController = new SaleTypesMoySkladMigrationController();

var controllers = new List<IMigrationController<KotovskayaMsContext, KotovskayaDbContext>>
    { saleTypesController };

// migrating to database using all controllers

await using var dbContext = new KotovskayaDbContext();
var msContext = new KotovskayaMsContext();
var integrator = new MoySkladMigrator(dbContext, msContext);
await integrator.Migrate(controllers);
