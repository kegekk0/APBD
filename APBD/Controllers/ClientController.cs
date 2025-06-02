using APBD.Models;
using APBD.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{clientId}")]
    public async Task<ActionResult<ClientWithRentals>> GetClientWithRentals(int clientId)
    {
        var client = await _clientService.GetClientWithRentalsAsync(clientId);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }

    [HttpPost]
    public async Task<IActionResult> AddClientWithRental([FromBody] NewClientWithRentalRequest request)
    {
        try
        {
            await _clientService.AddClientWithRentals(request);
            return CreatedAtAction(nameof(GetClientWithRentals), new { clientId = request.Client.ClientId }, null);
        }
        catch (ArgumentException e)
        {
            return StatusCode(500, "Error");
        }
    }

}