using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetStore.BLL.Orders.Commands;
using PetStore.BLL.Orders.Queries;
using PetStore.DataContracts.Orders;

using static PetStore.Server.Configuration.PolicyConstants;

namespace PetStore.Server.Controllers;

/// <summary>
/// Handles order creation and retrieval
/// </summary>
[ApiController]
[Route("[controller]")]
public class OrderController : BaseController
{
    /// <summary>
    /// Retrieves orders based on search filters
    /// </summary>
    /// <param name="request">Search filters</param>
    /// <returns></returns>
    [HttpPost("search")]
    [Authorize(Policy = ADMIN)]
    public async Task<ICollection<GetOrderResult>> SearchOrdersAsync(SearchOrdersRequest request)
    {
        var query = Mapper.Map<SearchOrdersRequest, SearchOrdersQuery>(request);
        var result = await Mediator.Send(query);
        return result;
    }

    /// <summary>
    /// Adds an order to the system
    /// </summary>
    /// <param name="request">Order details</param>
    /// <returns>Id of the created order</returns>
    [HttpPost]
    [Authorize(Policy = USER)]
    public async Task<ActionResult> CreateOrderAsync(CreateOrderRequest request)
    {
        var command = Mapper.Map<CreateOrderRequest, CreateOrderCommand>(request);
        var result = await Mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}