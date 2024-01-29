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
    private readonly IPersonRepository _personRepository = personRepository;
    private readonly IPersonMapper _personMapper = personMapper;
    private readonly IValidator<Person> _validator = validator;
    private readonly INotificationHandler _notificationHandler = notificationHandler;

    public async Task<bool> AddAsync(PersonSave personSave)
    {
        var person =_personMapper.SaveToDomain(personSave);

        if (!await IsValidAsync(person))
            return false;

        return await _personRepository.AddAsync(person);
    }

    public async Task<bool> UpdateAsync(PersonUpdate personUpdate)
    {
        var person = await _personRepository.GetByIdAsync(personUpdate.Id, false);

        if (person is null)
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Person"));

            return false;
        }
        
        _personMapper.UpdateToDomain(personUpdate, person);

        if (!await IsValidAsync(person))
            return false;

        return await _personRepository.UpdateAsync(person);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if(!await _personRepository.ExistsAsync(id))
        {
            _notificationHandler.AddNotification(nameof(EMessage.NotFound), EMessage.NotFound.Description().FormatTo("Person"));

            return false;
        }

        return await _personRepository.DeleteAsync(id);
    }

    public async Task<PersonResponse?> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id, true);

        if (person is null)
            return null;

        return _personMapper.DomainToResponse(person);
    }

    public async Task<List<PersonResponse>> GetAllAsync()
    {
        var personList = await _personRepository.GetAllAsync();

        return _personMapper.DomainListToResponseList(personList);
    }

    private async Task<bool> IsValidAsync(Person person)
    {
        var validationResult = await _validator.ValidateAsync(person);

        if (validationResult.IsValid)
            return true;

        foreach(var error in validationResult.Errors)
            _notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);

        return false;
    }
}
