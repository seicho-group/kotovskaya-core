namespace Kotovskaya.Order.Infrastructure
{
    public class DBInitializer
    {
        public static void Initializer(OrderDBContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
