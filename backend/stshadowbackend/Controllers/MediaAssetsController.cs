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
    public class MediaAssetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MediaAssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/media
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaAssets>>> GetMediaFiles()
        {
            return await _context.MediaAssets.ToListAsync();
        }

        // POST: /api/media
        [HttpPost]
        public async Task<ActionResult<MediaAssets>> UploadMedia(MediaAssetsDTO dto)
        {
            var media = new MediaAssets
            {
                FileName = dto.FileName,
                FilePath = dto.FilePath,
                UploadedDate = DateTime.UtcNow
            };

            _context.MediaAssets.Add(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMediaFiles), new { id = media.Id }, media);
        }

        // DELETE: /api/media/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            var media = await _context.MediaAssets.FindAsync(id);
            if (media == null) return NotFound();

            _context.MediaAssets.Remove(media);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
