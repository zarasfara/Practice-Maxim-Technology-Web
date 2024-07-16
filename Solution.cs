using System.Text.RegularExpressions;

namespace WebAppTechnology;

public static class Solution
{
    private static readonly string Vowels = "aeiouy";

    public static (string ProcessedString, Dictionary<char, int> CharOccurrences, string LongestVowelSubstring)
        ProcessString(string input)
    {
        Dictionary<char, int> charOccurrences = new Dictionary<char, int>();
        string processedString;

        if (input.Length % 2 == 0)
        {
            int midIndex = input.Length / 2;
            string firstHalf = input.Substring(0, midIndex);
            string secondHalf = input.Substring(midIndex);

            firstHalf = ReverseString(firstHalf);
            secondHalf = ReverseString(secondHalf);

            processedString = firstHalf + secondHalf;
        }
        else
        {
            string reversed = ReverseString(input);
            processedString = reversed + input;
        }

        foreach (char c in processedString)
        {
            if (!charOccurrences.TryAdd(c, 1))
            {
                charOccurrences[c]++;
            }
        }

        string longestVowelSubstring = FindLongestVowelSubstring(processedString);

        return (processedString, charOccurrences, longestVowelSubstring);
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

    public static bool IsValidString(string input, out string invalidChars)
    {
        Regex regex = new Regex("[^a-z]");
        MatchCollection matches = regex.Matches(input);

        invalidChars = new string(matches.Select(m => m.Value[0]).Distinct().ToArray());
        return matches.Count == 0;
    }

    private static string ReverseString(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}