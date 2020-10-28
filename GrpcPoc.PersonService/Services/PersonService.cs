using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcPoc.PersonService
{
    public class PersonService : Person.PersonBase
    {
        private readonly ILogger<PersonService> _logger;
        private readonly GrpcPocDbContext _context;

        public PersonService(
            GrpcPocDbContext context,
            ILogger<PersonService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public override async Task<PeopleResponse> GetPeople(PeopleRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"GetPeople, max records {request.MaxRecords}");

            var entities = await _context.People
                .Take(request.MaxRecords)
                .Select(p => new PersonGrpc
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth.ToTimestamp()
                })
                .OrderBy(p => p.LastName)
                .ToListAsync();

            var response = new PeopleResponse();
            response.People.AddRange(entities);

            return response;
        }
    }
}
