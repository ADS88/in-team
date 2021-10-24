using System;
using System.Collections.Generic;

namespace Server.Api.Dtos
{
    /// <summary>
    /// Information about a single Alpha
    /// </summary>
    public record AlphaDto
    {
    
        public int Id { get; init; }
        public String Name { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
        public ICollection<StateDto> States { get; init; }
    
    }
}