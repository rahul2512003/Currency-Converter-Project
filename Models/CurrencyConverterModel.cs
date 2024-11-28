namespace CurrencyConverter.Models
{
    public class CurrencyConverterModel
    {
        public double Amount { get; set; } // The amount to convert
        public string FromCurrency { get; set; } // Currency to convert from
        public string ToCurrency { get; set; } // Currency to convert to
        public double ConvertedAmount { get; set; } // Result of conversion
    }
}
