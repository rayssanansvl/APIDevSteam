using APIDevSteam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIDevSteamJau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        // Dependencias
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Metodo Construtor  com as a injeção de dependencias
        public UsuariosController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // [HttpPOST] : Criar uma Role (Perfil)
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return BadRequest("Nome da Role não pode ser vazio.");

            // Verifica se a Role já existe
            if (await _roleManager.RoleExistsAsync(roleName))
                return BadRequest("Role já existe.");

            // Cria a Role
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return Ok($"Role '{roleName}' criada com sucesso.");
            else
                return BadRequest(result.Errors);
        }

        // [HttpPOST] : Vincular um usuario a um papel (Role)
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Usuário não Cadastrado.");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
                return Ok($"Perfil '{roleName}' adicionado ao usuario '{user.UserName}'.");

            return BadRequest(result.Errors);
        }


        // [HttpGET] : Listar todos os usuarios
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userManager.Users.ToList();
            if (users == null)
                return NotFound("Nenhum usuario encontrado.");
            return Ok(users);
        }

        // [HttpGET] : Listar usuários por perfil
        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            if (users == null)
                return NotFound("Nenhum usuario encontrado.");
            return Ok(users);
        }
    }
}
