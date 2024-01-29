using CleanControllers.API.Entities;
using CleanControllers.API.Enums;
using CleanControllers.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;
public class PersonMapperTests
{
    private readonly PersonMapper _personMapper;

    public PersonMapperTests()
    {
        _personMapper = new PersonMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        // A
        var personResult = _personMapper.SaveToDomain(personSave);

        // A
        Assert.Equal(personResult.Name, personSave.Name);
        Assert.Equal(personResult.Gender, (EGender)personSave.Gender);
        Assert.Equal(personResult.Email, personSave.Email);
        Assert.Equal(personResult.Phone, personSave.Phone);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario()
    {
        // A
        var personUpdate= PersonBuilder.NewObject().UpdateBuild();
        var personResult = PersonBuilder.NewObject().DomainBuild();

        // A
        _personMapper.UpdateToDomain(personUpdate, personResult);

        // A
        Assert.Equal(personResult.Name, personUpdate.Name);
        Assert.Equal(personResult.Gender, (EGender)personUpdate.Gender);
        Assert.Equal(personResult.Email, personUpdate.Email);
        Assert.Equal(personResult.Phone, personUpdate.Phone);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario()
    {
        // A
        var person = PersonBuilder.NewObject().DomainBuild();

        // A
        var personResponseResult = _personMapper.DomainToResponse(person);

        // A
        Assert.Equal(personResponseResult.Id, person.Id);
        Assert.Equal(personResponseResult.Name, person.Name);
        Assert.Equal(personResponseResult.Gender, (ushort)person.Gender);
        Assert.Equal(personResponseResult.Email, person.Email);
        Assert.Equal(personResponseResult.Phone, person.Phone);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario()
    {
        // A
        var personList = new List<Person>()
        {
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild()
        };

        // A
        var personResponseListResult = _personMapper.DomainListToResponseList(personList);

        // A
        Assert.Equal(personResponseListResult.Count, personList.Count);
    }
}
