using Microsoft.AspNetCore.Mvc;
using WebAppTechnology.Enums;
using WebAppTechnology.Services.Interfaces;

namespace WebAppTechnology.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StringProcessingController : ControllerBase
{
    private readonly IRandomNumberService _randomNumberService;
    private readonly ISortService _sortService;

    public StringProcessingController(IRandomNumberService randomNumberService, ISortService sortService, IConfiguration configuration)
    {
        _randomNumberService = randomNumberService;
        _sortService = sortService;
        Solution.Initialize(configuration);
    }

    [HttpGet]
    public async Task<IActionResult> ProcessString([FromQuery] string input, [FromQuery] SortMethod sortMethod)
    {
        if (!Solution.IsValidString(input, out string invalidChars))
        {
            return BadRequest(new { Error = $"Ошибка: введены неподходящие символы или строка находится в черном списке: {invalidChars}" });
        }

        var (processedString, charOccurrences, longestString) = Solution.ProcessString(input);
        string sortedString = _sortService.SortString(input, sortMethod);

        int randomIndex = await _randomNumberService.GetRandomNumberAsync(processedString.Length);
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
}