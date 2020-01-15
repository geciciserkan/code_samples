using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;

namespace SampleWebApiAspNetCore.v2.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WebappdataController : ControllerBase
    {
        private readonly IWebAppDataRepository _webAppDataRepository;
        public WebappdataController(IWebAppDataRepository webAppDataRepository)
        {
            _webAppDataRepository = webAppDataRepository;
        }

        [HttpGet("webappdata/{guid}", Name = nameof(GetData))]
        public ActionResult GetData(string guid = null)
        {
            var data = _webAppDataRepository.Get(guid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPut("webappdata/{guid}", Name = nameof(SetData))]
        public ActionResult SetData([FromBody] WebAppDataEntity body, string guid = null)
        {
            body.guid = guid;
            _webAppDataRepository.Set(body);
            return Ok("Saved");
        }

    }
}
