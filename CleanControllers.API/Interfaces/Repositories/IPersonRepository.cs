using CleanControllers.API.Entities;

namespace CleanControllers.API.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<bool> AddAsync(Person person);
    Task<bool> UpdateAsync(Person person);
    Task<bool> ExistsAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<Person?> GetByIdAsync(int id, bool asNoTracking);
    Task<List<Person>> GetAllAsync();
}
