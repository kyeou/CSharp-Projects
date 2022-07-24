using System.Text.Json;

namespace DATA_HANDLER
{
    public class TRANSACTION
    {
        public DateTimeOffset Date { get; set; }
        public double Amount { get; set; }
        public string? Summary { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            // var weatherForecast = new WeatherForecast
            // {
            //     Date = DateTime.Parse("2019-08-01"),
            //     TemperatureCelsius = 25,
            //     Summary = "Hot"
            //  };

            // string jsonString = JsonSerializer.Serialize(weatherForecast);

            //Console.WriteLine(jsonString);

            //  File.WriteAllTextAsync("transactions.json", jsonString);

            List<TRANSACTION> trans = new List<TRANSACTION> { };

            trans.Add(new TRANSACTION
            {
                Date = DateTime.Parse("2019-08-01"),
                Amount = 12.34,
                Summary = "This is a test"
            });

            File.WriteAllTextAsync("transactions.json", JsonSerializer.Serialize(trans));

        }
    }

}