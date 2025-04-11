using System;
using DomainModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DomainModel
{
    public class Commande
    {
        [Key]
        public int NumCommande { get; init; }

        public DateTime DateCommande { get; set; } = DateTime.Now;

        [Required]
        public int ClientID { get; set; }

        [JsonIgnore]
        public Client? Client { get; set; }

        public List<CommandeProduits> CommandeProduits { get; set; } = new();
    }
}
