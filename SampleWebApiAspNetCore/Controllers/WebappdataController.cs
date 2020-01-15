using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleWebApiAspNetCore.Entities;
using SampleWebApiAspNetCore.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SampleWebApiAspNetCore.v2.Controllers
{
    [ApiController]
    [Route("api/")]
    public class WebappdataController : ControllerBase
    {

        private readonly IWebAppDataRepository webAppDataRepository;
        public WebappdataController(IConfiguration configuration)
        {
            webAppDataRepository = new WebAppDataRepository(configuration);
        }


        [HttpGet]
        [HttpPatch("webappdata/{guid}", Name = nameof(GetData))]
        public ActionResult GetData(string guid = null)
        {
            var data = webAppDataRepository.Get(guid);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpPut]
        [HttpPatch("webappdata2/{guid2}", Name = nameof(SetData))]
        public ActionResult SetData([FromBody] WebAppDataEntity body, string guid2 = null)
        {
            body.guid = guid2;
            webAppDataRepository.Set(body);
            return Ok("Saved");
        }


    }
}
