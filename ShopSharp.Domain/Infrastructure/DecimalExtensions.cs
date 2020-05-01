namespace ShopSharp.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetFormattedValue(this decimal value)
        {
            return $"${value:N2}";
        }
    }
}