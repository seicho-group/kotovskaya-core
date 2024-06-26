﻿using System.Data;
using System.Net.Http.Headers;
using System.Text;
using Confiti.MoySklad.Remap.Api;
using Confiti.MoySklad.Remap.Client;
using Confiti.MoySklad.Remap.Entities;
using Confiti.MoySklad.Remap.Models;
using DotNetEnv;
using Kotovskaya.DB.Domain.Entities.MoySkladExtensions;
using Kotovskaya.DB.Domain.Entities.Requests;
using Newtonsoft.Json;

namespace Kotovskaya.DB.Domain.Context;

public static class MsAttributes
{
    public const string IsNew =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/606fcab1-e6a0-11ed-0a80-078b001f31e9";

    public const string IsPopular =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/125e2108-e6a1-11ed-0a80-0859001ebc00";

    public const string IsSale =
        "https://online.moysklad.ru/api/remap/1.2/entity/product/metadata/attributes/125e2108-e6a1-11ed-0a80-0859001ebc00";
}

public class KotovskayaMsContext : MoySkladApi
{
    public KotovskayaMsContext()
    {
        Env.TraversePath().Load();
        Credentials = new MoySkladCredentials
        {
            AccessToken = Environment.GetEnvironmentVariable("MS_TOKEN")
        };
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Credentials.AccessToken);
        // setting base url of MoySklad API 
        Client.BaseAddress = new Uri("https://api.moysklad.ru/api/remap/1.2/");
        Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
    }

    /**
     * !! DANGEROUS METHOD !!
     * get product images
     */
    public async Task<Stream?> FetchProductImage(string imageUrl)
    {
        try
        {
            var productImages = await GetAsyncStream(imageUrl);
            return productImages;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    /**
     * !! DANGEROUS METHOD !!
     * get product images
     */
    public async Task<PagedEntities<Image>?> FetchProductImagesExtended(Guid productId)
    {
        try
        {
            var productImages = await GetAsyncJson<PagedEntities<Image>?>($"entity/product/{productId}/images");
            return productImages;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    /**
     * !! DANGEROUS METHOD !!
     * get product info
     */
    public async Task<Product?> FetchProductInfoExtended(Guid productId)
    {
        try
        {
            var products = await GetAsyncJson<Product?>($"entity/product/{productId}");
            return products;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    public async Task<ProductFolder[]?> FetchProductFoldersExtended()
    {
        try
        {
            var products = await GetAsyncJson<EntitiesResponse<ProductFolder>?>($"entity/productfolder");
            return products?.Rows;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    /**
     * !! DANGEROUS METHOD !!
     * get product info
     */
    public async Task<KotovskayaAssortment?[]> FetchAssortmentInfoExtended(string filter = "")
    {
        try
        {
            var products = await GetAsyncJson<EntitiesResponse<KotovskayaAssortment?>>($"entity/assortment?filter=" +
                                                                             $"{filter}" +
                                                                             $"stockStore=https://api.moysklad.ru/api/remap/1.2/entity/store/0f06c398-00a0-4db2-af9d-d48d0a02a5b4" +
                                                                             $"&expand=images&limit=100");
            return products.Rows;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    /**
     * get all products with specified attribute
     */
    public async Task<List<string>> FindProductsIdByMoySkladAttribute<T>(string attribute, T value, int? limit = 8)
    {
        var products = await GetAsyncJson<EntitiesResponse<Product>>($"entity/product?limit={limit}&filter={attribute}=true");

        return products.Rows
            .Where(row => row.Id != null)
            // MS type specifies that ID is nullable, that's why String.Empty mentioned here
            .Select(row => row.Id.ToString() ?? string.Empty)
            .ToList();
    }

    /**
     * create position by ms id
     */
    public async Task CreateOrderPositionByOrderId(Guid orderId, Guid productId, int quantity, int price)
    {
        var request = new CreateMoySkladOrderPositionRequest
        {
            quantity = quantity,
            price = price,
            assortment = new CreateMoySkladOrderPositionRequestAssortment
            {
                meta = new CreateMoySkladOrderPositionRequestAssortmentMeta
                {
                    type = "product",
                    href = $"https://api.moysklad.ru/api/remap/1.2/entity/product/{productId}",
                    mediaType = "application/json"
                }
            }
        };
        var json = JsonConvert.SerializeObject(request).ToLower();
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        Client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
        await Client.PostAsync($"entity/customerorder/{orderId}/positions", data);
    }

    /**
     * get request to ms with credentials
     */
    private async Task<T> GetAsyncJson<T>(string url)
    {
        var responseMessage = await Client.GetAsync(url);
        // decomposing gzip format to json string
        var response = await responseMessage.Content.ReadAsStringAsync();
        // deserializing string as EntityResponse of Product
        var responseJson = (T?)JsonConvert.DeserializeObject(response, typeof(T));

        // when deserializing falls, response json got rows: [] if deserializing completes but no entities found
        if (responseJson == null) throw new DataException("Response deserialization fault");

        return responseJson;
    }

    /**
     * get request to ms with credentials
     */
    private async Task<Stream> GetAsyncStream(string url)
    {
        var responseMessage = await Client.GetAsync(url);
        // decomposing gzip format to json string
        var response = await responseMessage.Content.ReadAsStreamAsync();

        return response;
    }
}
