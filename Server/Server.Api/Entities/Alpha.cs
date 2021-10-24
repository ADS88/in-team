using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    /// <summary>
    /// Table holding all Alphas
    /// </summary>
    public record Alpha
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

        [Required]
        public DateTimeOffset CreatedDate { get; init; }
        public virtual ICollection<State> States { get; set; }

    }
}