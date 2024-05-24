using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ESLBackend.Models;
using static ESLBackend.Controllers.UserController;
using System.Text.Json;
using System.Text;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> GetTemplate(int id)
        {
            var template = _context.Templates.FirstOrDefault(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }

        private static readonly HttpClient client = new HttpClient();




        [HttpPost("Goods")]
        public async Task<IActionResult> PostandPatchGoods([FromBody] Templates template)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ESLtoken = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(ESLtoken))
            {
                return Unauthorized("Token is missing");
            }

            Models.PostTemplates a = Models.Templates.MappedTemplate(template);
            
            // testing
            //string serializedJson = JsonSerializer.Serialize(a);
            //Console.WriteLine("JSON Content:");
            //Console.WriteLine(serializedJson);

            using StringContent jsonContent = new(
              JsonSerializer.Serialize(a),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ESLtoken);

            using HttpResponseMessage response = await client.PostAsync("http://162.62.125.25:5003/api/Goods/save", jsonContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var token = JsonSerializer.Deserialize<Token>(responseBody);

            if (token?.Message == "success")
            {
                return Ok(token.Message);
            }
            else
            {
                return BadRequest(token?.Message);
            }
        }



        [Authorize]
        [HttpPost("bind/esl")]
        public async Task<IActionResult> BindESL(Models.BindESL ESL)
        {
            var ESLtoken = HttpContext.Session.GetString("token");

            if (string.IsNullOrEmpty(ESLtoken))
            {
                return Unauthorized("Token is missing");
            }

            Models.BindESL2 a = Models.BindESL.BindESLMapper(ESL);

            string serializedJson = JsonSerializer.Serialize(a);

            using StringContent jsonContent = new(
              serializedJson,
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ESLtoken);

            using HttpResponseMessage response = await client.PostAsync("http://162.62.125.25:5003/api/esl/Tag/bind", jsonContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);

            var token = JsonSerializer.Deserialize<Token>(responseBody);

            if (token?.Message == "success")
            {
                return Ok(token.Message);
            }
            else
            {
                return BadRequest(token?.Message);
            }
        }

        public class Token
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }


    [HttpPost]
        public async Task<IActionResult> CreateTemplate(Templates template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTemplate), new { id = template.Id }, template);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate(int id, Templates template)
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
           // existingTemplate.Upc = template.Upc;
            existingTemplate.Items = template.Items;
            existingTemplate.Version = template.Version;
            existingTemplate.HashCode = template.HashCode;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(int id)
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
