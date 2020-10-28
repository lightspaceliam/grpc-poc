using System;

namespace GrpcPoc.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
