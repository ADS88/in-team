using System;
using System.Collections.Generic;
using Server.Api.Entities;

namespace Server.Api.Dtos
{
    public record AlphaDto
    {
    
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public ICollection<StateDto> States { get; init; }
    
    }
}