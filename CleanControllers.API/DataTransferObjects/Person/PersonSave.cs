using CleanControllers.API.DataTransferObjects.Enums;

namespace CleanControllers.API.DataTransferObjects.Person;

public sealed record PersonSave(string Name,
                                EGenderRequest Gender,
                                string Email,
                                string Phone);
