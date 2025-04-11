using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModel
{
    public class CommandeProduits
    {
        [Required]
        public int NumCommande { get; set; }

        [Required]
        public int ProduitID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey(nameof(NumCommande))]
        [JsonIgnore]
        public Commande? Commande { get; set; }

        [ForeignKey(nameof(ProduitID))]
        public Produit? Produit { get; set; }
    }
}
