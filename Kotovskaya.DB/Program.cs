using Confiti.MoySklad.Remap.Api;
using Kotovskaya.DB.Application.Services.Interfaces;
using Kotovskaya.DB.Application.Services.MoySkladIntegration;
using Kotovskaya.DB.Domain.Context;

DotNetEnv.Env.TraversePath().Load();

// setting up controllers
var categoriesController = new CategoriesMoySkladIntegrationController();
var productsController = new ProductsMoySkladIntegrationController();

var controllers = new List<IIntegrationController<MoySkladApi, KotovskayaDbContext>>() { categoriesController, productsController };

// migrating to database using all controllers
using (var dbContext = new KotovskayaDbContext())
{
    var integrator = new MoySkladIntegrator(dbContext);
    await integrator.Migrate(controllers);
}