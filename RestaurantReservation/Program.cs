using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace RestaurantReservation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSetting.json")
                .Build();

            var connectionString = configuration.GetSection("constr").Value;
        }
    }
}
