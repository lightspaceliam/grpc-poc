using GrpcPoc.Api.Models;

namespace GrpcPoc.Api.Extensions
{
    public static class ToDtoExtentions
    {
        public static PersonDto ToDto(this Proto.Person person)
        {     
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                MiddleName = person.MiddleName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth.ToDateTime()
            };
        }
    }
}
