using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcPoc.PersonService
{
    public class PersonService : PeopleService.PeopleServiceBase
    {
        private readonly ILogger<PersonService> _logger;
        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        public override Task<PeopleResponse> GetPeople(PeopleRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"GetPeople, max records {request.MaxRecords}");
            return Task.FromResult(new PeopleResponse());
        }
    }
}
