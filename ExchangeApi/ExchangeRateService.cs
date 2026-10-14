public class ExchangeRateService
{
    private readonly List<ExchangeRate> _taxas = new List<ExchangeRate>
    {
        new ExchangeRate("BRL", "EUR", 0.17m, DateTime.UtcNow, "Fonte Educacional"),
        new ExchangeRate("EUR", "BRL", 5.88m, DateTime.UtcNow, "Fonte Educacional")
    };

    public ExchangeRate? Find(string from, string to)
    {
        return _taxas.FirstOrDefault(t => t.from == from && t.to == to);
    }
}