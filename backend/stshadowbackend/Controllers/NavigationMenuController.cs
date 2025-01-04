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
    public class NavigationMenuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NavigationMenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/navigation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NavigationMenu>>> GetNavigationMenus()
        {
            return await _context.NavigationMenus.OrderBy(n => n.Order).ToListAsync();
        }

        // POST: /api/navigation
        [HttpPost]
        public async Task<ActionResult<NavigationMenu>> CreateNavigationMenu(NavigationMenuDTO dto)
        {
            var menu = new NavigationMenu
            {
                Name = dto.Name,
                Url = dto.Url,
                Order = dto.Order
            };

            _context.NavigationMenus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNavigationMenus), new { id = menu.Id }, menu);
        }

        // PUT: /api/navigation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNavigationMenu(int id, NavigationMenuDTO dto)
        {
            var menu = await _context.NavigationMenus.FindAsync(id);
            if (menu == null) return NotFound();

            menu.Name = dto.Name;
            menu.Url = dto.Url;
            menu.Order = dto.Order;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/navigation/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNavigationMenu(int id)
        {
            var menu = await _context.NavigationMenus.FindAsync(id);
            if (menu == null) return NotFound();

            _context.NavigationMenus.Remove(menu);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
