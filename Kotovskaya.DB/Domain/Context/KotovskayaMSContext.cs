using System.Data;
using System.Net.Http.Headers;
using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using Newtonsoft.Json;

namespace Kotovskaya.DB.Domain.Context;

public static class MsAttributes
{
    public static string IsNew =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/606fcab1-e6a0-11ed-0a80-078b001f31e9";
    
    public static string IsPopular =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/125e2108-e6a1-11ed-0a80-0859001ebc00";

    public static string IsSale =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/125e2108-e6a1-11ed-0a80-0859001ebc00";
}

public class KotovskayaMsContext : MoySkladApi
{
    public KotovskayaMsContext()
    {
        DotNetEnv.Env.TraversePath().Load();
        Credentials = new MoySkladCredentials()
        {
            AccessToken = Environment.GetEnvironmentVariable("MS_TOKEN")
        };
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.AccessToken);
        // setting base url of MoySklad API 
        Client.BaseAddress = new Uri("https://api.moysklad.ru/api/remap/1.2/");
    }

    /*
     * get all products with specified attribute
     */
    public async Task<List<string>> FindProductsIdByMoySkladAttribute<T>(string attribute, T value, int? limit = 8)
    {
        var responseMessage = await Client.GetAsync($"entity/product?limit={limit}&filter={attribute}=true");
        // decomposing gzip format to json string 
        var response = await responseMessage.Content.ReadAsStringAsync();
        // deserializing string as EntityResponse of Product
        var responseJson = (EntitiesResponse<Product>?)JsonConvert.DeserializeObject(response, typeof(EntitiesResponse<Product>));

        // when deserializing falls, response json got rows: [] if deserializing completes but no entities found
        if (responseJson == null)
        {
            throw new DataException("Response deserialization fault");
        }
        
        return responseJson.Rows
            .Where(row => row.Id != null)
            // MS type specifies that ID is nullable, that's why String.Empty mentioned here
            .Select(row =>row.Id.ToString() ?? string.Empty)
            .ToList();
    }
}