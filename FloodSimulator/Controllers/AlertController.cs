using FloodSimulator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FloodSimulator.Controllers
{
    [ApiController]
    [Route("Alerts")]
    public class AlertController : ControllerBase
    {
        private readonly IAlertServices _alertService;
        public AlertController(IAlertServices alertService)
        {
            _alertService = alertService;
        }

        [HttpGet]
        [Route("GetAllalerts")]
        public async Task<IActionResult> GetAllAlerts()
        {
            try
            {
                var alerts = await _alertService.GetAllAlerts();
                return Ok(alerts);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAlertByLocation")]
        public async Task<IActionResult> GetAlertByLocation(string area)
        {
            try
            {

                var alert = await _alertService.GetAlertByLocation(area);
                if(alert != null)
                {
                    return Ok(alert);
                }
                return NotFound("alert was not found");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
