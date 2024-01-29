using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Entities;

namespace CleanControllers.API.Interfaces.Mappers;

public interface IPersonMapper
{
    Person SaveToDomain(PersonSave personSave);
    void UpdateToDomain(PersonUpdate personUpdate, Person person);
    PersonResponse DomainToResponse(Person person);
    List<PersonResponse> DomainListToResponseList(List<Person> personList);
}
