using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Entities;
using CleanControllers.API.Enums;
using CleanControllers.API.Extensions;
using CleanControllers.API.Interfaces.Mappers;
using CleanControllers.API.Interfaces.Repositories;
using CleanControllers.API.Interfaces.Services;
using CleanControllers.API.Interfaces.Settings;
using FluentValidation;

namespace CleanControllers.API.Services;

public sealed class PersonService(IPersonRepository personRepository, IPersonMapper personMapper,
                                  IValidator<Person> validator, INotificationHandler notificationHandler) : IPersonService
{
    public async Task<bool> AddAsync(PersonSave personSave)
    {
        var person = personMapper.SaveToDomain(personSave);

        if (!await IsValidAsync(person))
            return false;

        return await personRepository.AddAsync(person);
    }

    public async Task<bool> UpdateAsync(PersonUpdate personUpdate)
    {
        var person = await personRepository.GetByIdAsync(personUpdate.Id, false);

        if (person is null)
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Person"));

            return false;
        }

        personMapper.UpdateToDomain(personUpdate, person);

        if (!await IsValidAsync(person))
            return false;

        return await personRepository.UpdateAsync(person);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!await personRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Person"));

            return false;
        }

        return await personRepository.DeleteAsync(id);
    }

    public async Task<PersonResponse?> GetByIdAsync(int id)
    {
        var person = await personRepository.GetByIdAsync(id, true);

        if (person is null)
            return null;

        return personMapper.DomainToResponse(person);
    }

    public async Task<List<PersonResponse>> GetAllAsync()
    {
        var personList = await personRepository.GetAllAsync();

        return personMapper.DomainListToResponseList(personList);
    }

    private async Task<bool> IsValidAsync(Person person)
    {
        var validationResult = await validator.ValidateAsync(person);

        if (validationResult.IsValid)
            return true;

        foreach (var error in validationResult.Errors)
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);

        return false;
    }
}
