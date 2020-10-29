using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using GrpcPoc.Api.Extensions;
using GrpcPoc.Api.Models;
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
        public async Task<ActionResult<IEnumerable<PersonDto>>> Index(int maxRecords = 10)
        {
            var request = new PeopleRequest
            {
                MaxRecords = maxRecords
            };

            var response = await _client.GetPeopleAsync(request);

            var people = response.People
                .Select(p => p.ToDto())
                .ToArray();

            return Ok(people);
        }

        [HttpPost("new")]
        public async Task<ActionResult<PersonDto>> Insert(PersonDto person)
        {
            var request = new PersonRequest
            {
                Person = new PersonGrpc
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    LastName = person.LastName,
                    DateOfBirth = person.DateOfBirth.ToTimestamp()
                }
            };

            var response = await _client.InsertAsync(request);

            return CreatedAtAction(nameof(Find), new { id = response.Person.Id }, response.Person.ToDto());

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDto>> Find(int id)
        {
            var request = new PersonByIdRequest
            {
                Id = id
            };

            var response = await _client.FindAsync(request);

            if (response.Person == null) return NotFound();

            return Ok(response.Person.ToDto());
        }
    }
}
