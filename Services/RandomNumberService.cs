using System.Text.Json;
using WebAppTechnology.Services.Interfaces;

namespace WebAppTechnology.Services;

public class RandomNumberService : IRandomNumberService
{
    private readonly string _url;
    
    public RandomNumberService(IConfiguration configuration)
    {
        _url = configuration.GetValue<string>("RandomApi");
    }

    public async Task<int> GetRandomNumberAsync(int max)
    {
        HttpClient client = new();
        try
        {
            string url = $"{_url}/random?min=0&max={max - 1}&count=1";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            int[] randomNumbers = JsonSerializer.Deserialize<int[]>(responseBody)!;

            return randomNumbers[0];
        }
        catch
        {
            var rnd = new Random();
            return rnd.Next(0, max);
        }
    }
}