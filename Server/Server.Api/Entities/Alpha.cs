using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Api.Entities
{
    public class Alpha
    {
        public int Id { get; init; }

        [Required]
        public String Name { get; init; }

    }
}