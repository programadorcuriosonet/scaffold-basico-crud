
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using video_scaffold.Data;
using video_scaffold.Dto;
using video_scaffold.Services;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = "Server=.\\sql2017;Database=EFCoreDBFirst;User=sa;Password=ian.,123;TrustServerCertificate=True";

builder.Services.AddDbContext<EFCoreDBFirstContext>(options =>
    options.UseSqlServer(connectionString)
    );

builder.Services.AddScoped<UsuariosRepo>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;
    var usuariosRepo = servicios.GetRequiredService<UsuariosRepo>();
    var usuarios = await usuariosRepo.ListarUsuariosAsync();
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.UsuarioId}, Username: {usuario.Username}, Email: {usuario.Email}");
    }

    //Insertar un nuevo usuario de ejemplo
    await usuariosRepo.CrearUsuarioAsync(new UsuariosDto
        {
            Username = "nuevo_usuario",
            PasswordHash = "hashed_password",
            Email = "email1@email.cl",
            NombreCompleto = "Nuevo Usuario",
            RolId = 1,
            Activo = true,
            FechaCreacion = DateTime.Now
        }

    );

    // Listar nuevamente para ver el nuevo usuario
    usuarios = await usuariosRepo.ListarUsuariosAsync();
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.UsuarioId}, Username: {usuario.Username}, Email: {usuario.Email}");
    }

    //Actualizar el usuario insertado
    var usuarioDto = await usuariosRepo.ObtenerUltimoUsuarioInsertadoAsync();

    usuarioDto.NombreCompleto = "Nuevo Usuario Actualizado";
    usuarioDto.Email = "email2@email.cl";
    await usuariosRepo.ActualizarUsuarioAsync(usuarioDto);

    // Listar nuevamente para ver el usuario actualizado
    usuarios = await usuariosRepo.ListarUsuariosAsync();
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.UsuarioId}, Username: {usuario.Username}, Email: {usuario.Email}");
    }

    //Eliminar el usuario insertado
    await usuariosRepo.EliminarUsuarioAsync(usuarioDto.UsuarioId);

    // Listar nuevamente para ver el usuario eliminado
    usuarios = await usuariosRepo.ListarUsuariosAsync();
    foreach (var usuario in usuarios)
    {
        Console.WriteLine($"ID: {usuario.UsuarioId}, Username: {usuario.Username}, Email: {usuario.Email}");
    }



    await app.RunAsync();
}



