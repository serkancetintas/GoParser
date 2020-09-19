using System.ComponentModel.DataAnnotations;

namespace GoService.Models
{
    public enum ExchangeType
    {
        DOLAR,
        EURO,
        STERLİN,
        [Display(Name="GRAM ALTIN")]
        GRAMALTIN
    }
    public class Exchange
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public ExchangeType ExchangeType { get; set; }
        public string ExchangeName { get; set; }
    }
}
