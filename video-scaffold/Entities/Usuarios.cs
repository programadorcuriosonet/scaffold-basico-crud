using System;
using System.Collections.Generic;

namespace video_scaffold.Entities;

public partial class Usuarios
{
    public int UsuarioId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public int RolId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimoLogin { get; set; }

    public virtual Roles Rol { get; set; } = null!;
}
