using DomainModel;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AzureShopContext _context;
        public ClientController(AzureShopContext context) => _context = context;

        // Voir tous les clients avec leurs commandes, produits et adresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients
                .Include(c => c.Adresses)
                .Include(c => c.Commandes)
                    .ThenInclude(co => co.CommandeProduits)
                        .ThenInclude(cp => cp.Produit)
                .ToListAsync();
        }

        // Voir un client par ID avec ses commandes, produits et adresses
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Adresses)
                .Include(c => c.Commandes)
                    .ThenInclude(co => co.CommandeProduits)
                        .ThenInclude(cp => cp.Produit)
                .FirstOrDefaultAsync(c => c.ClientID == id);

            if (client == null) return NotFound();
            return Ok(client);
        }

        // Créer un client
        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            if (client.Adresses == null || !client.Adresses.Any())
                return BadRequest("Un client doit avoir au moins une adresse.");

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClient), new { id = client.ClientID }, client);
        }

        // Supprimer un client (seulement s'il n'a pas de commandes)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients
                .Include(c => c.Commandes)
                .Include(c => c.Adresses)
                .FirstOrDefaultAsync(c => c.ClientID == id);

            if (client == null) return NotFound();

            if (client.Commandes.Any())
                return BadRequest("Impossible de supprimer un client ayant des commandes.");

            _context.Adresses.RemoveRange(client.Adresses);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mettre à jour un client
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client updatedClient)
        {
            var existingClient = await _context.Clients
                .Include(c => c.Adresses)
                .FirstOrDefaultAsync(c => c.ClientID == id);

            if (existingClient == null) return NotFound();

            existingClient.FirstName = updatedClient.FirstName;
            existingClient.LastName = updatedClient.LastName;
            existingClient.DateOfBirth = updatedClient.DateOfBirth;

            _context.Adresses.RemoveRange(existingClient.Adresses);
            existingClient.Adresses = updatedClient.Adresses;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
