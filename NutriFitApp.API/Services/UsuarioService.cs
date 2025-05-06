using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NutriFitApp.API.Data;
using NutriFitApp.API.Services.Interfaces;
using NutriFitApp.Shared.DTOs;
using NutriFitApp.Shared.Models;
using NutriFitApp.Shared.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NutriFitApp.API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly NutriFitDbContext _context;
    private readonly IConfiguration _config;

    public UsuarioService(NutriFitDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetUsuarioByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
    {
        var existing = await _context.Usuarios.FindAsync(usuario.Id);
        if (existing == null) return false;

        existing.Nombre = usuario.Nombre;
        existing.Email = usuario.Email;
        existing.PasswordHash = usuario.PasswordHash;
        existing.Rol = usuario.Rol;
        existing.EntrenadorId = usuario.EntrenadorId;
        existing.NutriologoId = usuario.NutriologoId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteUsuarioAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TokenDTO> RegisterAsync(Usuario usuario)
    {
        usuario.Rol = TipoUsuario.Usuario;
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return BuildToken(usuario);
    }

    public async Task<TokenDTO> LoginAsync(LoginDTO model)
    {
        var user = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.Password);

        if (user == null)
            throw new ApplicationException("Credenciales inválidas");

        return BuildToken(user);
    }

    private TokenDTO BuildToken(Usuario user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email!),
            new Claim(ClaimTypes.Role, user.Rol.ToString()),
            new Claim("Nombre", user.Nombre)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expires // ✅ Corrección aquí
        };

    }
}
