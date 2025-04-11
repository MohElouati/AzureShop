using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DomainModel;
namespace DomainModel
{
    public class Produit
    {
        [Key]
        public int ProduitID { get; init; }

        [StringLength(50)]
        public required string Nom { get; set; }

        [StringLength(100)]
        public required string Description { get; set; }

        public decimal Prix { get; set; }
        public int UnitsInStock { get; set; }
        public int? Poids { get; set; }

        [JsonIgnore]
        public List<CommandeProduits>? CommandeProduits { get; set; }
    }
}
