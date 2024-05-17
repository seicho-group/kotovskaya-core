using AspNetCore.Yandex.ObjectStorage.Configuration;
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

var yandexObjectStorage = new KotovskayaYandexObjectStorageContext(new YandexStorageOptions()
{
    BucketName = "kotovskaya.products",
    AccessKey = Environment.GetEnvironmentVariable("YA_ID_KEY"),
    SecretKey = Environment.GetEnvironmentVariable("YA_ID_SECRET_KEY")
});
var imagesController = new ImagesMoySkladMigrationController(yandexObjectStorage);

var controllers = new List<IMigrationController<KotovskayaMsContext, KotovskayaDbContext>>
    { imagesController };

// migrating to database using all controllers

await using var dbContext = new KotovskayaDbContext();
var msContext = new KotovskayaMsContext();
var integrator = new MoySkladMigrator(dbContext, msContext);
await integrator.Migrate(controllers);
