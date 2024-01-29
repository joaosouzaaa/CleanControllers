using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Entities;
using CleanControllers.API.Interfaces.Mappers;
using CleanControllers.API.Interfaces.Repositories;
using CleanControllers.API.Interfaces.Settings;
using CleanControllers.API.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;
public sealed class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IPersonMapper> _personMapperMock;
    private readonly Mock<IValidator<Person>> _validatorMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _personMapperMock = new Mock<IPersonMapper>();
        _validatorMock = new Mock<IValidator<Person>>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _personService = new PersonService(_personRepositoryMock.Object, _personMapperMock.Object, _validatorMock.Object,
            _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveToDomain(It.IsAny<PersonSave>()))
            .Returns(person);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.AddAsync(It.IsAny<Person>()))
            .ReturnsAsync(true);

        // A
        var addResult = await _personService.AddAsync(personSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(addResult);
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var personSave = PersonBuilder.NewObject().SaveBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personMapperMock.Setup(p => p.SaveToDomain(It.IsAny<PersonSave>()))
            .Returns(person);

        var validationFailureList = new List<ValidationFailure>()
        {
            new()
            {
                ErrorMessage = "test",
                PropertyName = "random"
            },
            new()
            {
                ErrorMessage = "rrr",
                PropertyName = "test"
            }
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var addResult = await _personService.AddAsync(personSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(addResult);
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.Is<bool>(p => p == false)))
            .ReturnsAsync(person);

        _personMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()));

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _personRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<Person>()))
            .ReturnsAsync(true);

        // A
        var updateResult = await _personService.UpdateAsync(personUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Once());

        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.Is<bool>(p => p == false)))
            .Returns(Task.FromResult<Person?>(null));

        // A
        var updateResult = await _personService.UpdateAsync(personUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _personMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Never());
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task UpdateAsync_InvalidEntity_ReturnsFalse()
    {
        // A
        var personUpdate = PersonBuilder.NewObject().UpdateBuild();

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.Is<bool>(p => p == false)))
            .ReturnsAsync(person);

        _personMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<PersonUpdate>(), It.IsAny<Person>()));

        var validationFailureList = new List<ValidationFailure>()
        {
            new()
            {
                ErrorMessage = "test",
                PropertyName = "random"
            },
            new()
            {
                ErrorMessage = "rrr",
                PropertyName = "test"
            },
            new()
            {
                ErrorMessage = "rrr",
                PropertyName = "test"
            }
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        var updateResult = await _personService.UpdateAsync(personUpdate);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _personRepositoryMock.Verify(p => p.UpdateAsync(It.IsAny<Person>()), Times.Never());

        Assert.False(updateResult);
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        _personRepositoryMock.Setup(p => p.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);

        // A
        var deleteResult = await _personService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Once());

        Assert.True(deleteResult);
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        // A
        var deleteResult = await _personService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _personRepositoryMock.Verify(p => p.DeleteAsync(It.IsAny<int>()), Times.Never());

        Assert.False(deleteResult);
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsEntity()
    {
        // A
        var id = 123;

        var person = PersonBuilder.NewObject().DomainBuild();
        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.Is<bool>(p => p == true)))
            .ReturnsAsync(person);

        var personResponse = PersonBuilder.NewObject().ResponseBuild();
        _personMapperMock.Setup(p => p.DomainToResponse(It.IsAny<Person>()))
            .Returns(personResponse);

        // A
        var personResponseResult = await _personService.GetByIdAsync(id);

        // A
        _personMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Person>()), Times.Once());

        Assert.NotNull(personResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = 123;

        _personRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<int>(), It.Is<bool>(p => p == true)))
            .Returns(Task.FromResult<Person?>(null));

        // A
        var personResponseResult = await _personService.GetByIdAsync(id);

        // A
        _personMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Person>()), Times.Never());

        Assert.Null(personResponseResult);
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsEntityList()
    {
        // A
        var personList = new List<Person>()
        {
            PersonBuilder.NewObject().DomainBuild(),
            PersonBuilder.NewObject().DomainBuild()
        };
        _personRepositoryMock.Setup(p => p.GetAllAsync())
            .ReturnsAsync(personList);

        var personResponseList = new List<PersonResponse>()
        {
            PersonBuilder.NewObject().ResponseBuild(),
            PersonBuilder.NewObject().ResponseBuild(),
            PersonBuilder.NewObject().ResponseBuild()
        };
        _personMapperMock.Setup(p => p.DomainListToResponseList(It.IsAny<List<Person>>()))
            .Returns(personResponseList);

        // A
        var personResponseListResult = await _personService.GetAllAsync();

        // A
        Assert.Equal(personResponseListResult.Count, personResponseList.Count);
    }
}
