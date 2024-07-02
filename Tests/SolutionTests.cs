using NUnit.Framework;

namespace WebAppTechnology.Tests;

public class SolutionTests
{
    [SetUp]
    public void Setup()
    {
        var inMemorySettings = new Dictionary<string, string>
        {
            { "Settings:BlackList:0", "blacklist1" },
            { "Settings:BlackList:1", "blacklist2" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        Solution.Initialize(configuration);
    }

    [Test]
    [TestCase("a", true, "")]
    [TestCase("abcdef", true, "")]
    [TestCase("abcde", true, "")]
    [TestCase("invalid string!", false, " !")]
    [TestCase("blacklist1", false, "1")]
    public void IsValidString_Test(string input, bool expectedResult, string expectedInvalidChars)
    {
        bool result = Solution.IsValidString(input, out string invalidChars);
        Assert.That(result, Is.EqualTo(expectedResult));
        Assert.That(invalidChars, Is.EqualTo(expectedInvalidChars));
    }

    [Test]
    [TestCase("a", "aa", new char[] { 'a' }, "aa")]
    [TestCase("abcdef", "cbafed", new char[] { 'a', 'b', 'c', 'd', 'e', 'f' }, "afe")]
    [TestCase("abcde", "edcbaabcde", new char[] {'e', 'd', 'c', 'b', 'a' }, "edcbaabcde")]
    public void ProcessString_Test(string input, string expectedProcessedString, char[] expectedCharKeys,
        string expectedLongestVowelSubstring)
    {
        var result = Solution.ProcessString(input);
        Assert.That(result.ProcessedString, Is.EqualTo(expectedProcessedString));
        Assert.That(result.CharOccurrences.Keys, Is.EquivalentTo(expectedCharKeys));
        Assert.That(result.LongestVowelSubstring, Is.EqualTo(expectedLongestVowelSubstring));
    }
}