using Confiti.MoySklad.Remap.Entities;

namespace Kotovskaya.DB.Domain.Entities.MoySkladExtensions;

public class KotovskayaAssortmentImages
{
    public Image[] Rows { get; set; }
}

public class KotovskayaAssortment : Assortment
{
    public Price[] SalePrices { get; set; } = [];
    public string Description { get; set; } = "";
    public Group? Group { get; set; }
    public string PathName { get; set; } = "";
    public KotovskayaAssortmentImages Images { get; set; }
}
