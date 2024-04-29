using efatoora.Server.Contracts;
using efatoora.service.Services; 
using Microsoft.AspNetCore.Mvc;
using ZatcaCore.Contracts;

namespace efatoora.Server.Controllers;

[ApiController]
public class OnboardingController : ControllerBase
{
    private readonly ILogger<OnboardingController> _logger;
    private readonly IDeviceServices _deviceServices;

    public OnboardingController(ILogger<OnboardingController> logger, IDeviceServices deviceServices)
    {
        _logger = logger;
        _deviceServices = deviceServices;
    }

    [Route("OnBoard")]
    [HttpPost]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<ProdCsidOnboardingResponse>> OnBoardAsync([FromBody] OnBoardContract onBoardContract)
    {
        try
        {
            var response = await _deviceServices.OnBoard(onBoardContract);

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while generating invoice");
            return BadRequest(ex.Message);
        }

    }

}
