using System;
using System.Collections.Generic;
using Server.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Dtos
{
    /// <summary>
    /// DTO containing a state of an Alpha
    /// </summary>
    public record StateDto
    {
        [Required]
        public int Id { get; init; }
        [Required]
        public String Name { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }
        public ICollection<Question> Questions { get; init; }

        [Required]
        public int AlphaId { get; init; }
    }
}