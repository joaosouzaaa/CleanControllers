using System.ComponentModel;

namespace CleanControllers.API.Enums;

public enum EMessage : ushort
{
    [Description("{0} has invalid length. It should be {1}.")]
    InvalidLength,

    [Description("{0} needs to be filled.")]
    Required,

    [Description("{0} was not found.")]
    NotFound,

    [Description("{0} has invalid format")]
    InvalidFormat,

    [Description("An unexpected error happened")]
    UnexpectedError
}
