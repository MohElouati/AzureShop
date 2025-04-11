using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private readonly AzureShopContext _context;

        public ProduitController(AzureShopContext context) => _context = context;

        // Récupérer tous les produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
            => await _context.Produits.ToListAsync();

        // Récupérer un produit par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduitById(int id)
        {
            var produit = await _context.Produits.FindAsync(id);
            if (produit == null) return NotFound();

            return Ok(produit);
        }

        // Ajouter un produit
        [HttpPost]
        public async Task<IActionResult> CreateProduit(Produit produit)
        {
            _context.Produits.Add(produit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduitById), new { id = produit.ProduitID }, produit);
        }

        // Modifier un produit
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduit(int id, Produit updatedProduit)
        {
            var existingProduit = await _context.Produits
                .FirstOrDefaultAsync(p => p.ProduitID == id);

            if (existingProduit == null)
                return NotFound();

            existingProduit.Nom = updatedProduit.Nom;
            existingProduit.Description = updatedProduit.Description;
            existingProduit.Prix = updatedProduit.Prix;
            existingProduit.UnitsInStock = updatedProduit.UnitsInStock;
            existingProduit.Poids = updatedProduit.Poids;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Supprimer un produit
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.Produits
                .Include(p => p.CommandeProduits)
                .FirstOrDefaultAsync(p => p.ProduitID == id);

            if (produit == null) return NotFound();

            if (produit.CommandeProduits != null && produit.CommandeProduits.Any())
                return BadRequest("Ce produit est utilisé dans une commande et ne peut pas être supprimé.");

            _context.Produits.Remove(produit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
