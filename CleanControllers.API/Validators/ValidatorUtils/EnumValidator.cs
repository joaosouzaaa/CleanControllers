namespace CleanControllers.API.Validators.ValidatorUtils;

public static class EnumValidator
{
    public static bool IsValidInputEnum<TEnum>(ushort enumInput)
        where TEnum : Enum
        =>
        Enum.IsDefined(typeof(TEnum), enumInput);
}
