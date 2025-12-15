using System;
using System.Collections.Generic;

namespace video_scaffold.Dto;

public partial class RolesDto
{
    public int RolId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual List<UsuariosDto> Usuarios { get; set; } = new List<UsuariosDto>();
}
