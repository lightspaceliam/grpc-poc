using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcPoc.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GrpcPoc.PersonService
{
    public class PersonService : Proto.GrpcPerson.GrpcPersonBase
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

        public override async Task<Proto.PeopleResponse> GetPeople(Proto.PeopleRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"GetPeople, max records {request.MaxRecords}");

            var entities = await _context.People
                .Take(request.MaxRecords)
                .Select(p => new Proto.Person
                {
                    Id = p.Id,
                    FirstName = p.FirstName,

                    // Because gRPC Protobuf PersonGrpc.MiddleName is of type google.protobuf.StringValue, I no longer have to write custom logic.
                    MiddleName = p.MiddleName,
                    
                    LastName = p.LastName,
                    DateOfBirth = DateTime.SpecifyKind(p.DateOfBirth, DateTimeKind.Utc).ToTimestamp()
                })
                .OrderBy(p => p.LastName)
                .ToListAsync();

            var response = new Proto.PeopleResponse();
            response.People.AddRange(entities);

            return response;
        }

        public override async Task<Proto.PersonResponse> Insert(Proto.PersonRequest request, ServerCallContext context)
        {
            var dbEntry = new Entities.Person
            {
                FirstName = request.Person.FirstName,
                MiddleName = request.Person.MiddleName,
                LastName = request.Person.LastName,
                DateOfBirth = request.Person.DateOfBirth.ToDateTime(),
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

            await _context.People.AddAsync(dbEntry);
            await _context.SaveChangesAsync();

            return new Proto.PersonResponse
            {
                Person = new Proto.Person
                {
                    Id = dbEntry.Id,
                    FirstName = dbEntry.FirstName,
                    MiddleName = dbEntry.MiddleName,
                    LastName = dbEntry.LastName,
                    DateOfBirth = DateTime.SpecifyKind(dbEntry.DateOfBirth, DateTimeKind.Utc).ToTimestamp()
                }
            };
        }

        public override async Task<Proto.PersonByIdResponse> Find(Proto.PersonByIdRequest request, ServerCallContext context)
        {
            var response = new Proto.PersonByIdResponse();

            var person = await _context.People
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if(person != null)
            {
                response.Person = new Proto.Person
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    LastName = person.LastName,
                    DateOfBirth = DateTime.SpecifyKind(person.DateOfBirth, DateTimeKind.Utc).ToTimestamp()
                };
            }

            return response;
        }
    }
}
