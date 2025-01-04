using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stshadowbackend.Data;
using stshadowbackend.DTO;
using stshadowbackend.Models;

namespace stshadowbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SectionContentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SectionContentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/sections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectionContent>>> GetSections()
        {
            return await _context.SectionContents.OrderBy(s => s.Order).ToListAsync();
        }

        // POST: /api/sections
        [HttpPost]
        public async Task<ActionResult<SectionContent>> CreateSection(SectionContentDTO dto)
        {
            var section = new SectionContent
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Order = dto.Order
            };

            _context.SectionContents.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSections), new { id = section.Id }, section);
        }

        // PUT: /api/sections/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(int id, SectionContentDTO dto)
        {
            var section = await _context.SectionContents.FindAsync(id);
            if (section == null) return NotFound();

            section.Title = dto.Title;
            section.Description = dto.Description;
            section.ImageUrl = dto.ImageUrl;
            section.Order = dto.Order;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/sections/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.SectionContents.FindAsync(id);
            if (section == null) return NotFound();

            _context.SectionContents.Remove(section);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
