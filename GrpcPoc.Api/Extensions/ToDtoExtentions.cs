using GrpcPoc.Api.Models;
using GrpcPoc.PersonService;

namespace GrpcPoc.Api.Extensions
{
    public static class ToDtoExtentions
    {
        public static PersonDto ToDto(this PersonGrpc personGrpc)
        {
            return new PersonDto
            {
                Id = personGrpc.Id,
                FirstName = personGrpc.FirstName,
                MiddleName = personGrpc.MiddleName,
                LastName = personGrpc.LastName,
                DateOfBirth = personGrpc.DateOfBirth.ToDateTime()
            };
        }
    }
}
