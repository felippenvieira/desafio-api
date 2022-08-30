using System.Threading.Tasks;
using desafio_api.Data;
using desafio_api.DTO;
using desafio_api.Models;
using desafio_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace desafio_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly AppDbContext _context;

        public AccountController(TokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Post([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Verifique os campos e tente novamente." });

            var passwordHashed = PasswordHasher.Hash(registerDTO.Password);

            var user = new User
            {
                Nome = registerDTO.Nome,
                Email = registerDTO.Email,
                PasswordHash = passwordHashed
            };

            try
            {
                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = $"Utilize o e-mail {user.Email} e a senha {registerDTO.Password} para fazer o login." });
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new { Message = "Este e-mail já está cadastrado." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Desculpe, verifique os campos e tente novamente." });

            var user = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user is null)
                return StatusCode(401, new { Message = "Usuário ou senha inválidos." });

            if (!PasswordHasher.Verify(user.PasswordHash, loginDTO.Senha))
                return StatusCode(401, new { Message = "Usuário ou senha inválidos." });

            try
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(new { token = token });
            }
            catch
            {
                return StatusCode(500, new { Message = "Falha interna no servidor." });
            }
        }
    }
}