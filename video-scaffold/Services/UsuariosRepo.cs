using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using video_scaffold.Data;
using video_scaffold.Dto;
using video_scaffold.Entities;

namespace video_scaffold.Services
{
    public class UsuariosRepo(EFCoreDBFirstContext context)
    {
        public async Task<IEnumerable<UsuariosDto>> ListarUsuariosAsync()
        {
            var usuarios = await context.Usuarios.ToListAsync();

            return usuarios.Adapt<List<UsuariosDto>>();

        }
        public async Task<UsuariosDto?> ObtenerUltimoUsuarioInsertadoAsync()
        {
            var ultimoUsuario = await context.Usuarios
                .OrderByDescending(u => u.UsuarioId)
                .FirstOrDefaultAsync();
            return ultimoUsuario.Adapt<UsuariosDto>();
        }
        public async Task<UsuariosDto?> ObtenerUsuarioPorIdAsync(int usuarioId)
        {
            var usuario = await context.Usuarios.FindAsync(usuarioId);
            return usuario.Adapt<UsuariosDto>();
        }
        public async Task<UsuariosDto> CrearUsuarioAsync(UsuariosDto nuevoUsuarioDto)
        {
            var nuevoUsuario = nuevoUsuarioDto.Adapt<Usuarios>();

            context.Usuarios.Add(nuevoUsuario);
            await context.SaveChangesAsync();
            return nuevoUsuario.Adapt<UsuariosDto>();
        }
        public async Task<bool> EliminarUsuarioAsync(int usuarioId)
        {
            var usuario = await context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return false;
            }
            context.Usuarios.Remove(usuario);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<UsuariosDto?> ActualizarUsuarioAsync(UsuariosDto usuarioActualizadoDto)
        {
            var usuarioExistente = await context.Usuarios.FindAsync(usuarioActualizadoDto.UsuarioId);
            if (usuarioExistente == null)
            {
                return null;
            }
            usuarioExistente.Username = usuarioActualizadoDto.Username;
            usuarioExistente.PasswordHash = usuarioActualizadoDto.PasswordHash;
            usuarioExistente.Email = usuarioActualizadoDto.Email;
            usuarioExistente.NombreCompleto = usuarioActualizadoDto.NombreCompleto;
            usuarioExistente.RolId = usuarioActualizadoDto.RolId;
            usuarioExistente.Activo = usuarioActualizadoDto.Activo;
            usuarioExistente.UltimoLogin = usuarioActualizadoDto.UltimoLogin;
            await context.SaveChangesAsync();
            return usuarioExistente.Adapt<UsuariosDto>();
        }
    }
}
