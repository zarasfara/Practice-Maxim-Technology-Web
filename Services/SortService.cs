using WebAppTechnology.Enums;
using WebAppTechnology.Services.Interfaces;
using WebAppTechnology.Sort;

namespace WebAppTechnology.Services;

public class SortService : ISortService
{
    public string SortString(string input, SortMethod sortMethod)
    {
        return sortMethod switch
        {
            SortMethod.QuickSort => QuickSort.Sort(input),
            SortMethod.TreeSort => TreeSort.Sort(input),
            _ => throw new ArgumentException("Неверный выбор метода сортировки.")
        };
    }
}