using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace WebAppTechnology;

public static class Solution
{
    private const string Vowels = "aeiouy";
    private static List<string> _blackList;

    public static void Initialize(IConfiguration configuration)
    {
        _blackList = configuration.GetSection("Settings:BlackList").Get<List<string>>();
    }

    public static bool IsValidString(string input, out string invalidChars)
    {
        var regex = new Regex("[^a-z]");
        MatchCollection matches = regex.Matches(input);

        invalidChars = new string(matches.Select(m => m.Value[0]).Distinct().ToArray());

        return matches.Count == 0 && !_blackList.Contains(input);
    }

    public static (string ProcessedString, Dictionary<char, int> CharOccurrences, string LongestVowelSubstring)
        ProcessString(string input)
    {
        string processedString = ProcessInputString(input);
        Dictionary<char, int> charOccurrences = CountCharOccurrences(processedString);
        string longestVowelSubstring = FindLongestVowelSubstring(processedString);

        return (processedString, charOccurrences, longestVowelSubstring);
    }

    private static string ProcessInputString(string input)
    {
        if (input.Length % 2 == 0)
        {
            int midIndex = input.Length / 2;
            string firstHalf = ReverseString(input.Substring(0, midIndex));
            string secondHalf = ReverseString(input.Substring(midIndex));

            return firstHalf + secondHalf;
        }
        else
        {
            string reversed = ReverseString(input);
            return reversed + input;
        }
    }

    private static Dictionary<char, int> CountCharOccurrences(string input)
    {
        var charOccurrences = new Dictionary<char, int>();
        foreach (char c in input)
        {
            if (!charOccurrences.TryAdd(c, 1))
            {
                charOccurrences[c]++;
            }
        }

        return charOccurrences;
    }

    private static string FindLongestVowelSubstring(string input)
    {
        int maxLength = 0;
        string longestSubstring = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (Vowels.Contains(input[i]))
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (Vowels.Contains(input[j]))
                    {
                        int length = j - i + 1;
                        if (length > maxLength)
                        {
                            maxLength = length;
                            longestSubstring = input.Substring(i, length);
                        }
                    }
                }
            }
        }

        return longestSubstring;
    }

    private static string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
