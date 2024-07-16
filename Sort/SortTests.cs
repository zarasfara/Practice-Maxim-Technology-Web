using NUnit.Framework;

namespace WebAppTechnology.Sort;

public class SortTests
{
    [Test]
    [TestCase("", "")]
    [TestCase("a", "a")]
    [TestCase("cba", "abc")]
    [TestCase("dcba", "abcd")]
    [TestCase("zxyabc", "abcxyz")]
    [TestCase("aabbcc", "aabbcc")]
    public void QuickSort_Sort_Test(string input, string expected)
    {
        string result = QuickSort.Sort(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("", "")]
    [TestCase("a", "a")]
    [TestCase("cba", "abc")]
    [TestCase("dcba", "abcd")]
    [TestCase("zxyabc", "abcxyz")]
    [TestCase("aabbcc", "aabbcc")]
    public void TreeSort_Sort_Test(string input, string expected)
    {
        string result = TreeSort.Sort(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}