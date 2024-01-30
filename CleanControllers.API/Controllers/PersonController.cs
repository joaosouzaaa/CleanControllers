using CleanControllers.API.DataTransferObjects.Person;
using CleanControllers.API.Interfaces.Services;
using CleanControllers.API.Settings.NotificationSettings;
using Microsoft.AspNetCore.Mvc;

namespace CleanControllers.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public sealed class PersonController(IPersonService personService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> AddAsync([FromBody] PersonSave personSave) =>
        personService.AddAsync(personSave);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> UpdateAsync([FromBody] PersonUpdate personUpdate) =>
        personService.UpdateAsync(personUpdate);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<bool> DeleteAsync([FromQuery] int id) =>
        personService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PersonResponse?> GetByIdAsync([FromQuery] int id) =>
        personService.GetByIdAsync(id);

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<PersonResponse>> GetAllAsync() =>
        personService.GetAllAsync();
}
