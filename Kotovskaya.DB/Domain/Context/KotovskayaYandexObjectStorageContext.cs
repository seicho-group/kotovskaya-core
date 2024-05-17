using AspNetCore.Yandex.ObjectStorage;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using Microsoft.Extensions.Options;

namespace Kotovskaya.DB.Domain.Context;

public class KotovskayaYandexObjectStorageContext(YandexStorageOptions options) : YandexStorageService(options)
{

}
