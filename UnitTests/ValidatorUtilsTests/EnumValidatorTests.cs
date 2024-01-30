using CleanControllers.API.Enums;
using CleanControllers.API.Validators.ValidatorUtils;

namespace UnitTests.ValidatorUtilsTests;
public sealed class EnumValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void IsValidInputEnum_SuccessfulScenario_ReturnsTrue(ushort enumInput)
    {
        var isValid = EnumValidator.IsValidInputEnum<EGender>(enumInput);

        Assert.True(isValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(-5)]
    public void IsValidInputEnum_InvalidValue_ReturnsFalse(ushort enumInput)
    {
        var isValid = EnumValidator.IsValidInputEnum<EGender>(enumInput);

        Assert.False(isValid);
    }
}
