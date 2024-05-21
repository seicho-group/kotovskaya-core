namespace Kotovskaya.DB.Domain.Entities.Enum;

public enum DeliveryWay
{
    Self,
    Courier,
    Mail
}

public static class DeliveryWayExtension
{
    public static string GetDeliveryWayName(this DeliveryWay deliveryWay)
    {
        return deliveryWay switch
        {
            DeliveryWay.Self => "Самовывоз",
            DeliveryWay.Courier => "Курьер",
            DeliveryWay.Mail => "Почта",
            _ => throw new ArgumentOutOfRangeException(nameof(deliveryWay), deliveryWay, null)
        };
    }
}
