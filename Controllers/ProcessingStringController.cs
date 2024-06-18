using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebAppTechnology.Enums;
using WebAppTechnology.Sort;

namespace WebAppTechnology.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringProcessingController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ProcessString([FromQuery] string input, [FromQuery] SortMethod sortMethod)
        {
            if (!Solution.IsValidString(input, out string invalidChars))
            {
                return BadRequest(new { Error = $"Ошибка: введены неподходящие символы: {invalidChars}" });
            }

            var (processedString, charOccurrences, longestString) = Solution.ProcessString(input);
            string sortedString = ChooseAndSortString(input, sortMethod);

            int randomIndex = await GetRandomNumberAsync(processedString.Length);
            string stringWithRemovedChar = processedString.Remove(randomIndex, 1);

            var result = new
            {
                ProcessedString = processedString,
                CharOccurrences = charOccurrences,
                LongestVowelSubstring = longestString,
                SortedString = sortedString,
                StringWithRemovedChar = stringWithRemovedChar
            };

            return Ok(result);
        }

        private static async Task<int> GetRandomNumberAsync(int max)
        {
            HttpClient client = new();
            try
            {
                string url = $"https://www.randomnumberapi.com/api/v1.0/random?min=0&max={max - 1}&count=1";
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

        private static string ChooseAndSortString(string input, SortMethod sortMethod)
        {
            return sortMethod switch
            {
                SortMethod.QuickSort => QuickSort.Sort(input),
                SortMethod.TreeSort => TreeSort.Sort(input),
                _ => throw new ArgumentException("Неверный выбор метода сортировки.")
            };
        }
    }
}