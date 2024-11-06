using System;
using ASP.NET_web_api_learning.models.DbModels;
using ASP.NET_web_api_learning.models.ProjectModels;
using ASP.NET_web_api_learning.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET_web_api_learning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : ControllerBase
    {
        readonly AppDbContext _context;
        readonly HashingService hashingService;
        public CredentialsController(AppDbContext context, HashingService hashingService) =>
            (_context, hashingService) = (context, hashingService);

        [HttpPost]
        public IActionResult CreateCredential([FromBody] Credentials newCredential)
        {
            if (newCredential == null)
            {
                return BadRequest("Invalid credential data.");
            }
            try
            {
                Credentials HashedCredentials = new(newCredential) 
                {
                    password = hashingService.HashPassword(newCredential.password) 
                };
                _context.Credentials.Add(HashedCredentials);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetCredentialById), new { id = newCredential.id }, newCredential);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCredentialById(int id)
        {
            var credential = _context.Credentials.Find(id);
            if (credential == null)
            {
                return NotFound();
            }
            return Ok(credential);
        }
        [HttpPost("Verify")]
        public IActionResult VerifyPassword([FromBody] Credentials credentials)
        {
            return hashingService.VerifyPassword(credentials) ? ;
        }
    }
}
