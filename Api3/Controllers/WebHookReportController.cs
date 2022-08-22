namespace Api3.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class WebHookReportController : ControllerBase
    {
        [HttpPost("outage", Name = nameof(ReportOutage))]
        public async Task<IActionResult> ReportOutage([FromBody] ReportModel model, [FromServices] ILogger<WebHookReportController> logger)
        {
            logger.LogInformation("Message Received");

            logger.LogInformation(model.Message);

            return this.Accepted();
        }
    }

    public class ReportModel
    {
        public string Message { get; set; } = string.Empty;
    }
}
