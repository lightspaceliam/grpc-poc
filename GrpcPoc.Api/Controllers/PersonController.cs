using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcPoc.PersonService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcPoc.Api.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly Person.PersonClient _client;
        public PersonController(
            Person.PersonClient client,
            ILogger<PersonController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new PeopleRequest
            {
                MaxRecords = 10
            };

            var response = await _client.GetPeopleAsync(request);

            return Ok(response);
        }
    }
}
