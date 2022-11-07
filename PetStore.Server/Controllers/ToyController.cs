using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.BLL.Toys.Commands;
using PetStore.BLL.Toys.Queries;
using PetStore.DataContracts.Toys;

using static PetStore.Server.Configuration.PolicyConstants;

namespace PetStore.Server.Controllers;

/// <summary>
/// Handles toys creation, modification, removal and retrieval
/// </summary>
[ApiController]
[Route("[controller]")]
public class ToyController : BaseController
{
    /// <summary>
    /// Retrieves toys based on search filters
    /// </summary>
    /// <param name="request">Search filters</param>
    /// <returns></returns>
    [HttpPost("search")]
    [Authorize]
    public async Task<ICollection<GetToyResult>> SearchToysAsync(SearchToysRequest request)
    {
        var query = Mapper.Map<SearchToysRequest, SearchToysQuery>(request);
        var result = await Mediator.Send(query);
        return result;
    }

    /// <summary>
    /// Adds a toy to the system
    /// </summary>
    /// <param name="request">Toy details</param>
    /// <returns>Id of the created toy</returns>
    [HttpPost]
    [Authorize(Policy = ADMIN)]
    public async Task<ActionResult> CreateToyAsync(CreateToyRequest request)
    {
        var command = Mapper.Map<CreateToyRequest, CreateToyCommand>(request);
        var result = await Mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Updates existing toy fully or partially
    /// </summary>
    /// <param name="id">Toy id</param>
    /// <param name="request">Toy details</param>
    [HttpPatch("{id}")]
    [Authorize(Policy = ADMIN)]
    public async Task<ActionResult> PatchToyAsync(int id, PatchToyRequest request)
    {
        var command = Mapper.Map<PatchToyRequest, PatchToyCommand>(request);
        command.Id = id;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Soft removes toy from the system
    /// </summary>
    /// <param name="id">Toy id</param>
    [HttpDelete("{id}")]
    [Authorize(Policy = ADMIN)]
    public async Task<ActionResult> DeleteToyAsync(int id)
    {
        var command = new DeleteToyCommand { Id = id };
        await Mediator.Send(command);
        return NoContent();
    }
}