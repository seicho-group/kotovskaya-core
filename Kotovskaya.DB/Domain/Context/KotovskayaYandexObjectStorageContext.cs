using AspNetCore.Yandex.ObjectStorage;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using Microsoft.Extensions.Options;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaYandexObjectStorageContext() : YandexStorageService(new YandexStorageOptions()
{
    BucketName = "kotovskaya.products",
    AccessKey = Environment.GetEnvironmentVariable("YA_ID_KEY"),
    SecretKey = Environment.GetEnvironmentVariable("YA_ID_SECRET_KEY")
});
