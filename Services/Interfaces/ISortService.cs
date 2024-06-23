using WebAppTechnology.Enums;

namespace WebAppTechnology.Services.Interfaces;

public interface ISortService
{
    string SortString(string input, SortMethod sortMethod);
}