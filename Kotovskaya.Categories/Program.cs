using System.Net.Http.Headers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

namespace Kotovskaya.Categories
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var url = "https://api.moysklad.ru/api/remap/1.2/download/2cd5241b-f0c7-4ee4-b5db-de2aa75b4862?miniature=true";
            var fileName = "img.jpg";

            using (HttpClient client = new HttpClient())
            {
                using (var requestMessage =
                       new HttpRequestMessage(HttpMethod.Get, url))
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", "0f165f456ecc4bf2fa2f189b69be402f4dc430d8");
    
                    var response = await client.SendAsync(requestMessage);
                    Console.WriteLine(response);
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = response.Content.ReadAsStreamAsync().Result)
                        {
                            var image = Image.Load<Rgba32>(stream);
                            
                            int width = image.Width;
                            int height = image.Height;

                            // Обрезка центрального квадрата размером 450x450
                            int cropWidth = width >= 450 ? 450 : width;
                            int cropHeight = height >= 450 ? 450 : height;

                            Rectangle cropRect = new Rectangle(
                                (width - cropWidth) / 2,
                                (height - cropHeight) / 2,
                                cropWidth,
                                cropHeight
                            );

                            image.Mutate(x => x.Crop(cropRect)); // обрезка изображения

                            // сохранение обрезанного изображения
                            image.Save(fileName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("huy");
                    }
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                });

    }
}