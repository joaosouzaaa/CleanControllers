using CleanControllers.API.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorTests;
public sealed class PersonValidatorTests
{
    private readonly PersonValidator _personValidator;

    public PersonValidatorTests()
    {
        _personValidator = new PersonValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personToValidate = PersonBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var personWithInvalidName = PersonBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidNameParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            new string('a', count: 101)
        };
    }

    [Theory]
    [MemberData(nameof(InvalidEmailParameters))]
    public async Task ValidateAsync_InvalidEmail_ReturnsFalse(string email)
    {
        // A
        var personWithInvalidEmail = PersonBuilder.NewObject().WithEmail(email).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidEmail);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static IEnumerable<object[]> InvalidEmailParameters()
    {
        yield return new object[]
        {
            ""
        };

        yield return new object[]
        {
            "test"
        };

        yield return new object[]
        {
             "invalid@"
        };

        yield return new object[]
        {
             "invalid.com"
        };

        yield return new object[]
        {
            $"tes{new string('a', count: 100)}t@valid.com"
        };

        yield return new object[]
        {
            new string('a', count: 101)
        };
    }

    [Theory]
    [InlineData("")]
    [InlineData("test")]
    [InlineData("invalid")]
    [InlineData("123456789112")]
    [InlineData("1234567891")]
    public async Task ValidateAsync_InvalidPhone_ReturnsFalse(string phone)
    {
        // A
        var personWithInvalidPhone = PersonBuilder.NewObject().WithPhone(phone).DomainBuild();

        // A
        var validationResult = await _personValidator.ValidateAsync(personWithInvalidPhone);

        // A
        Assert.False(validationResult.IsValid);
    }
}
