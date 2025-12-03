using Microsoft.AspNetCore.Mvc;

namespace WCFWebApi.Controllers;

public class AppointmentController : BaseController
{
    [HttpGet("Appointment/{appointmentId}")]
    public IActionResult Generate(long appointmentId)
    {
        return Ok(new
        {
            AppointmentId = appointmentId,
            Data = $"Data Generated For Appointment : {appointmentId}"
        });
    }
}
