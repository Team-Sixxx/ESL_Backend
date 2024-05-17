using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ESLBackend.Models;

namespace ESLBackend.Controllers
{
    [ApiController]
    [Route("api/templates")]
    public class TemplatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TemplatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTemplates()
        {
            var templates = _context.Templates.ToList();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public IActionResult GetTemplate(string id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplate(Templates template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTemplate), new { id = template.Id }, template);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate(string id, Templates template)
        {
            if (id != template.Id)
            {
                return BadRequest();
            }

            var existingTemplate = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (existingTemplate == null)
            {
                return NotFound();
            }

            existingTemplate.CreatedBy = template.CreatedBy;
            existingTemplate.CreatedTime = template.CreatedTime;
            existingTemplate.LastUpdatedBy = template.LastUpdatedBy;
            existingTemplate.LastUpdatedTime = template.LastUpdatedTime;
            existingTemplate.ShopCode = template.ShopCode;
            existingTemplate.GoodsCode = template.GoodsCode;
            existingTemplate.GoodsName = template.GoodsName;
            existingTemplate.TemplateType = template.TemplateType;
            existingTemplate.Upc = template.Upc;
            existingTemplate.Items = template.Items;
            existingTemplate.Version = template.Version;
            existingTemplate.HashCode = template.HashCode;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(string id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
