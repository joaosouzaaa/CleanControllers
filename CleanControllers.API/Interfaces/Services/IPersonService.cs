using CleanControllers.API.DataTransferObjects.Person;

namespace CleanControllers.API.Interfaces.Services;

public interface IPersonService
{
    Task<bool> AddAsync(PersonSave personSave);
    Task<bool> UpdateAsync(PersonUpdate personUpdate);
    Task<bool> DeleteAsync(int id);
    Task<PersonResponse?> GetByIdAsync(int id);
    Task<List<PersonResponse>> GetAllAsync();
}
