using CleanControllers.API.Enums;

namespace CleanControllers.API.Entities;

public sealed class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required EGender Gender { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
