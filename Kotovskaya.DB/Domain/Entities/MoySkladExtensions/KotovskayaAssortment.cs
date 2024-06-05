using Confiti.MoySklad.Remap.Entities;

namespace Kotovskaya.DB.Domain.Entities.MoySkladExtensions;

public class KotovskayaAssortment : Assortment
{
    public Price[] SalePrices { get; set; } = [];
    public string Description { get; set; } = "";
    public Group? Group { get; set; }
}
