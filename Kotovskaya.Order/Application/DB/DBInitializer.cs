namespace Kotovskaya.Order.Application.DB
{
    public class DBInitializer
    {
        public static void Initializer(OrderDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
