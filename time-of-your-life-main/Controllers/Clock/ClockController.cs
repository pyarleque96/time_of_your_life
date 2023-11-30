using Microsoft.AspNetCore.Mvc;
using time_of_your_life.Application.Contratcs.Clock;
using time_of_your_life.Application.Managers;
using time_of_your_life.Infrastructure.Transport.Clock.Result;
using time_of_your_life.Infrastructure.Transport.Core.Response;

namespace time_of_your_life.Controllers;

[Route("[controller]")]
public class ClockController : BaseApiController
{
    private readonly ILogger<ClockController> _logger;
    private readonly IClockManager _clockManager;

    public ClockController(IClockManager clockManager,
                           ILogger<ClockController> logger)
    {
        _clockManager = clockManager;
        _logger = logger;
    }

    [HttpGet, Route("{id}")]
    public async Task<BaseResponse<GetPresetByIdResult<ClockPropsDto>>> GetPreset(Guid id)
    {
        _logger.LogInformation($"Getting preset with ID: {id}");
        return await _clockManager.GetPresetByIdAsync(id);
    }

    [HttpGet, Route("presets")]
    public async Task<BaseResponse<GetAllPresetsResult<ClockPropsDto>>> GetPresets()
    {
        _logger.LogInformation("Getting all presets");
        return await _clockManager.GetAllPresetsAsync();
    }

    [HttpPost("presets")]
    public async Task<BaseResponse<AddPresetResult>> AddPreset([FromBody] ClockPropsDto preset)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

            _logger.LogWarning($"Invalid model state: {string.Join(", ", errors)}");
            return BaseResponse<AddPresetResult>.Failed(string.Join(", ", errors));
        }

        _logger.LogInformation("Adding a new preset");
        return await _clockManager.AddPresetAsync(preset);
    }

    [HttpPut("presets/{id}")]
    public async Task<BaseResponse> UpdatePreset([FromBody] ClockPropsDto updatedPreset)
    {
        _logger.LogInformation($"Updating preset with ID: {updatedPreset.Id}");
        return await _clockManager.UpdatePresetAsync(updatedPreset);
    }
}
