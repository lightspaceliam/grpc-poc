using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using GrpcPoc.Api.Extensions;
using GrpcPoc.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcPoc.Api.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly Proto.GrpcPerson.GrpcPersonClient _client;
        
        public PersonController(
            Proto.GrpcPerson.GrpcPersonClient client,
            ILogger<PersonController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Index(int maxRecords = 10)
        {
            var request = new Proto.PeopleRequest
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
            var request = new Proto.PersonRequest
            {
                Person = new Proto.Person
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
            var request = new Proto.PersonByIdRequest
            {
                Id = id
            };

            var response = await _client.FindAsync(request);

            if (response.Person == null) return NotFound();

            return Ok(response.Person.ToDto());
        }
    }
}
