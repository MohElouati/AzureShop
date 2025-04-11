using DomainModel;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdresseController : ControllerBase
    {
        private readonly AzureShopContext _context;
        public AdresseController(AzureShopContext context) => _context = context;

        // Lister toutes les adresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
            => await _context.Adresses.ToListAsync();

        // Voir une adresse par ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse == null) return NotFound();
            return Ok(adresse);
        }

        // Créer une adresse
        [HttpPost]
        public async Task<ActionResult<Adresse>> CreateAdresse(Adresse adresse)
        {
            _context.Adresses.Add(adresse);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAdresse), new { id = adresse.AdresseID }, adresse);
        }

        // Modifier une adresse
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdresse(int id, Adresse updatedAdresse)
        {
            if (id != updatedAdresse.AdresseID) return BadRequest();

            var existingAdresse = await _context.Adresses
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AdresseID == id);

            if (existingAdresse == null) return NotFound();

            var newAdresse = new Adresse
            {
                AdresseID = existingAdresse.AdresseID,
                Street = updatedAdresse.Street,
                City = updatedAdresse.City,
                Country = updatedAdresse.Country,
                ZipCode = updatedAdresse.ZipCode,
                ClientID = updatedAdresse.ClientID
            };

            _context.Adresses.Update(newAdresse);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Supprimer une adresse
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse == null) return NotFound();

            _context.Adresses.Remove(adresse);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
