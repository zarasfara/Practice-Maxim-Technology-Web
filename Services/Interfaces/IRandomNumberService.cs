namespace WebAppTechnology.Services.Interfaces;

public interface IRandomNumberService
{
    Task<int> GetRandomNumberAsync(int max);
}