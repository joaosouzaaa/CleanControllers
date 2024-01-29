using CleanControllers.API.DataTransferObjects.Enums;
using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Entities;
using CleanControllers.API.Enums;

namespace UnitTests.TestBuilders;
public sealed class PersonBuilder
{
    private readonly int _id = 123;
    private string _email = "email@valid.com";
    private readonly EGender _gender = EGender.Male;
    private string _name = "test";
    private string _phone = "12345678911";
    private readonly EGenderRequest _genderRequest = EGenderRequest.Male;

    public static PersonBuilder NewObject() =>
        new();

    public Person DomainBuild() =>
        new()
        {
            Email = _email,
            Gender = _gender,
            Id = _id,
            Name = _name,
            Phone = _phone
        };

    public PersonSave SaveBuild() =>
        new(_name,
            _genderRequest,
            _email,
            _phone);

    public PersonUpdate UpdateBuild() =>
        new(_id,
            _name,
            _genderRequest,
            _email,
            _phone);

    public PersonResponse ResponseBuild() =>
        new()
        {
            Email = _email,
            Gender = (ushort)_gender,
            Id = _id,
            Name = _name,
            Phone = _phone
        };

    public PersonBuilder WithEmail(string email)
    {
        _email = email;

        return this;
    }

    public PersonBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public PersonBuilder WithPhone(string phone)
    {
        _phone = phone;

        return this;
    }
}
