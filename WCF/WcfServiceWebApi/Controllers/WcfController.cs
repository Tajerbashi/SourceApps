using Microsoft.AspNetCore.Mvc;
using WCFApplicationService;

namespace WcfServiceWebApi.Controllers
{
    public class WcfController : BaseController
    {
        private readonly Service1Client _wcfClient;

        public WcfController()
        {
            _wcfClient = new Service1Client();
        }

        // GET api/wcf/getdata/123
        [HttpGet]
        [Route("getdata/{value}")]
        public IActionResult GetData(int value)
        {
            try
            {
                string result = _wcfClient.GetData(value);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GeneratePDF/{appointmentId}")]
        public IActionResult GeneratePDF(long appointmentId)
        {
            try
            {
                string result = _wcfClient.GeneratePDF(appointmentId);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/wcf/getdatacontract
        [HttpPost]
        [Route("getdatacontract")]
        public IActionResult GetDataUsingDataContract([FromBody] CompositeTypeRequest request)
        {
            try
            {
                var composite = new CompositeType
                {
                    BoolValue = request.BoolValue,
                    StringValue = request.StringValue
                };

                var result = _wcfClient.GetDataUsingDataContract(composite);

                return Ok(new
                {
                    success = true,
                    data = new
                    {
                        boolValue = result.BoolValue,
                        stringValue = result.StringValue
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

      
    }

}
