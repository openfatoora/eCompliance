using efatoora.service.Contracts;
using efatoora.service.Services;
using Microsoft.AspNetCore.Mvc;

namespace efatoora.Server.Controllers;

[ApiController]
public class DashboardController(IDeviceServices deviceServices) : ControllerBase
{
    [Route("Dashboard/ViewModel")]
    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult<DashboardViewModel>> GetViewModelAsync()
    {
        var keys = await deviceServices.GetKeysAsync();

        return new DashboardViewModel
        {
            IsOnBoarded = keys?.ProdBinaryToken != null,
            OnboardingResult = new OnboardingResult
            {
                BinaryToken = keys?.ProdBinaryToken,
                PrivateKey = keys?.PrivateKey,
                Secret = keys?.Secret,
                UpdatedAt = keys?.UpdatedAt
            }
        };

    }
}
