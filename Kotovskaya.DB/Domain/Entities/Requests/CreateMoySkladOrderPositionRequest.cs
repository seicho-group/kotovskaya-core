namespace Kotovskaya.DB.Domain.Entities.Requests;

public class CreateMoySkladOrderPositionRequestAssortment
{
    public required CreateMoySkladOrderPositionRequestAssortmentMeta meta { get; set; }
}

public class CreateMoySkladOrderPositionRequestAssortmentMeta
{
    public required string type { get; set; }
    public required string href { get; set; }
    public required string mediaType { get; set; }
}

public class CreateMoySkladOrderPositionRequest
{
    public int quantity { get; set; } = 0;
    public required CreateMoySkladOrderPositionRequestAssortment assortment { get; set; }
}