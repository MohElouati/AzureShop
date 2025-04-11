using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly AzureShopContext _context;

        public CommandeController(AzureShopContext context) => _context = context;

        // Récupérer toutes les commandes avec les produits associés
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commande>>> GetOrders()
            => await _context.Commandes
                .Include(o => o.CommandeProduits)
                .ThenInclude(cp => cp.Produit)
                .ToListAsync();

        // Récupérer une commande par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Commande>> GetOrder(int id)
        {
            var order = await _context.Commandes
                .Include(o => o.CommandeProduits)
                .ThenInclude(cp => cp.Produit)
                .FirstOrDefaultAsync(o => o.NumCommande == id);

            if (order == null) return NotFound();

            return order;
        }

        // Créer une nouvelle commande
        [HttpPost]
        public async Task<ActionResult<Commande>> CreateOrder(Commande order)
        {
            if (order.CommandeProduits == null || order.CommandeProduits.Count == 0)
                return BadRequest("Une commande doit contenir au moins un produit.");

            var clientExists = await _context.Clients.AnyAsync(c => c.ClientID == order.ClientID);
            if (!clientExists)
                return NotFound("Le client spécifié n'existe pas.");

            order.DateCommande = DateTime.Now;

            foreach (var cp in order.CommandeProduits)
            {
                var produit = await _context.Produits.FindAsync(cp.ProduitID);
                if (produit == null)
                    return NotFound($"Le produit avec l'ID {cp.ProduitID} n'existe pas.");

                cp.Produit = produit;
                _context.Entry(cp).State = EntityState.Added;
            }

            _context.Commandes.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.NumCommande }, order);
        }

        // Mettre à jour une commande
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Commande updatedOrder)
        {
            var existingOrder = await _context.Commandes
                .Include(o => o.CommandeProduits)
                .FirstOrDefaultAsync(o => o.NumCommande == id);

            if (existingOrder == null) return NotFound();

            if (!await _context.Clients.AnyAsync(c => c.ClientID == updatedOrder.ClientID))
                return BadRequest("Client introuvable.");

            existingOrder.ClientID = updatedOrder.ClientID;
            existingOrder.DateCommande = DateTime.Now;

            _context.CommandeProduits.RemoveRange(existingOrder.CommandeProduits);

            existingOrder.CommandeProduits = new List<CommandeProduits>();
            foreach (var item in updatedOrder.CommandeProduits)
            {
                existingOrder.CommandeProduits.Add(new CommandeProduits
                {
                    ProduitID = item.ProduitID,
                    Quantity = item.Quantity
                });
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Supprimer une commande
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Commandes.FindAsync(id);
            if (order == null) return NotFound();

            _context.Commandes.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
