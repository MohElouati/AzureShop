using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public record Client
    {
        [Key]
        public int ClientID { get; init; }

        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(100)]
        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public List<Adresse> Adresses { get; set; } = new();
        public List<Commande> Commandes { get; set; } = new();
    }
}
