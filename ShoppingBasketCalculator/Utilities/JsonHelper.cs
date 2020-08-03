using System.IO;
using System.Text.Json;

namespace ShoppingBasketCalculator.Utilities
{
    public static class JsonHelper
    {
        public static T Deserialise<T>(string fileName)
        {
            var jsonString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Repository",
                fileName));
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}