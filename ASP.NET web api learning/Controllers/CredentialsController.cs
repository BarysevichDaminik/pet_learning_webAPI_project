using System;
using ASP.NET_web_api_learning.models.DbModels;
using ASP.NET_web_api_learning.models.ProjectModels;
using ASP.NET_web_api_learning.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ASP.NET_web_api_learning.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialsController : ControllerBase
    {
        readonly AppDbContext _context;
        readonly HashingService _hashingService;
        public CredentialsController(AppDbContext context, HashingService hashingService) =>
            (_context, _hashingService) = (context, hashingService);

        [HttpPost("/signup")]
        public IActionResult Register([FromForm] Credentials newCredential)
        {
            if (newCredential == null || string.IsNullOrWhiteSpace(newCredential.username) || string.IsNullOrWhiteSpace(newCredential.password))
            {
                return BadRequest("Invalid credential data.");
            }
            try
            {
                var hashedPassword = _hashingService.HashPassword(newCredential.password);
                Credentials hashedCredentials = new(newCredential)
                {
                    password = hashedPassword
                };
                _context.Credentials.Add(hashedCredentials);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCredentialById(int id)
        {
            Credentials? credential = _context.Credentials.Find(id);
            if (credential == null)
            {
                return NotFound();
            }
            return Ok(credential);
        }
        [HttpPost("/signin")]
        public IActionResult Login([FromForm] Credentials credentials)
        {
            Credentials? hashedCredentials = _context.Credentials.FirstOrDefault(x => x.username == credentials.username);
            if (hashedCredentials == null) { return Unauthorized("Can not find such user in database"); }
            return _hashingService.VerifyPassword(hashedCredentials, credentials) ? Ok("Success") : Unauthorized("Password Not found");
        }
    }
}
