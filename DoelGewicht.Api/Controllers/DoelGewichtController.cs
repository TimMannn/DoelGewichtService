using DoelGewicht.Api.Models;
using DoelGewicht.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoelGewicht.Api.Controllers;

[ApiController]
[Route("api/doelgewicht")]
[Authorize]
public class DoelGewichtController : ControllerBase
{
    private readonly DoelGewichtService _gewichtService;

    public DoelGewichtController(DoelGewichtService gewichtService)
    {
        _gewichtService = gewichtService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoelGewichtDetails>>> GetDoelGewicht()
    {
        try
        {
            var doelGewicht = await _gewichtService.GetDoelGewicht();
            return Ok(doelGewicht);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting doelgewicht: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult> SetDoelGewicht([FromBody] AddDoelGewichtModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Ongeldige invoer.",
                errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            await _gewichtService.SetDoelGewicht(model.Doelgewicht);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting doelgewicht: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditDoelGewicht(int id, [FromBody] EditDoelGewichtModel model)
    {
        if (id != model.Id)
        {
            return BadRequest("EditDoelGewichtModel id mismatch in DoelGewichtController");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                message = "Ongeldige invoer.",
                errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
            });
        }

        try
        {
            await _gewichtService.EditDoelGewicht(model.Id, model.Doelgewicht, model.Datumbehaald);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error editing doelgewicht: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDoelGewicht(int id)
    {
        try
        {
            await _gewichtService.DeleteDoelGewicht(id);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting doelgewicht: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
