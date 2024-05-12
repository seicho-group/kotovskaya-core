using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaMsContext : MoySkladApi
{
    public KotovskayaMsContext()
    {
        DotNetEnv.Env.TraversePath().Load();
        Credentials = new MoySkladCredentials()
        {
            AccessToken = Environment.GetEnvironmentVariable("MS_TOKEN")
        };
    }
}