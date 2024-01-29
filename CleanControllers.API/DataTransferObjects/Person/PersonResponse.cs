namespace CleanControllers.API.DataTransferObjects.Person;

public sealed class PersonResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required ushort Gender { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
}
