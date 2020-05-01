namespace ShopSharp.Domain.Infrastructure
{
    public static class DecimalExtensions
    {
        public static string GetValueString(this decimal value)
        {
            return $"${value:N2}";
        }
    }
}