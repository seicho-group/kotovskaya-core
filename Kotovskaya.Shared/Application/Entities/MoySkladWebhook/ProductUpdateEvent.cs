using Confiti.MoySklad.Remap.Entities;

namespace Kotovskaya.Shared.Application.Entities.MoySkladWebhook;

public class ProductUpdateEventMeta
{
    public string type { get; set; }
    public string href { get; set; }
}

public class ProductUpdateEvent
{
    public required ProductUpdateEventMeta meta { get; set; }
    public string action { get; set; } = "UPDATE";
}