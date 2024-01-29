using CleanControllers.API.DataTransferObjects.Enums;

namespace CleanControllers.API.DataTransferObjects.Person;

public sealed record PersonUpdate(int Id,
                                  string Name,
                                  EGenderRequest Gender,
                                  string Email,
                                  string Phone);
