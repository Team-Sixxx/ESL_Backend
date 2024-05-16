using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetTemplate(int id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }

        [HttpPost]
        public IActionResult CreateTemplate(Templates template)
        {
            _context.Templates.Add(template);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTemplate), new { id = template.Id }, template);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTemplate(int id, Templates template)
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

            existingTemplate.Type = template.Type;
            existingTemplate.StoreNumber = template.StoreNumber;
            existingTemplate.ShopCode = template.ShopCode;
            existingTemplate.Name = template.Name;
            existingTemplate.Price1 = template.Price1;
            existingTemplate.Price2 = template.Price2;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTemplate(int id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
