using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Entities;
using CleanControllers.API.Enums;
using CleanControllers.API.Interfaces.Mappers;

namespace CleanControllers.API.Mappers;

public sealed class PersonMapper : IPersonMapper
{
    public Person SaveToDomain(PersonSave personSave) =>
        new()
        {
            Name = personSave.Name,
            Gender = (EGender)personSave.Gender,
            Email = personSave.Email,
            Phone = personSave.Phone
        };

    public void UpdateToDomain(PersonUpdate personUpdate, Person person)
    {
        person.Name = personUpdate.Name;
        person.Gender = (EGender)personUpdate.Gender;
        person.Email = personUpdate.Email;
        person.Phone = personUpdate.Phone;
    }

    public PersonResponse DomainToResponse(Person person) =>
        new()
        {
            Id = person.Id,
            Name = person.Name,
            Gender = (ushort)person.Gender,
            Email = person.Email,
            Phone = person.Phone
        };


    public List<PersonResponse> DomainListToResponseList(List<Person> personList) =>
        personList.Select(DomainToResponse).ToList();
}
