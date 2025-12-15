/* =========================================================
   1. CREAR BASE DE DATOS
   ========================================================= */
IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'EFCoreDBFirst')
BEGIN
    CREATE DATABASE EFCoreDBFirst;
END
GO

USE EFCoreDBFirst;
GO

/* =========================================================
   2. TABLA ROLES
   ========================================================= */
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL
    DROP TABLE dbo.Roles;
GO

CREATE TABLE dbo.Roles
(
    RolId        INT IDENTITY(1,1) PRIMARY KEY,
    Nombre       NVARCHAR(50) NOT NULL UNIQUE,
    Descripcion  NVARCHAR(150) NULL,
    Activo       BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

/* =========================================================
   3. TABLA USUARIOS
   ========================================================= */
IF OBJECT_ID('dbo.Usuarios', 'U') IS NOT NULL
    DROP TABLE dbo.Usuarios;
GO

CREATE TABLE dbo.Usuarios
(
    UsuarioId     INT IDENTITY(1,1) PRIMARY KEY,
    Username      NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash  NVARCHAR(255) NOT NULL,
    Email         NVARCHAR(100) NOT NULL,
    NombreCompleto NVARCHAR(100) NOT NULL,
    RolId         INT NOT NULL,
    Activo        BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    UltimoLogin   DATETIME NULL,

    CONSTRAINT FK_Usuarios_Roles
        FOREIGN KEY (RolId) REFERENCES dbo.Roles(RolId)
);
GO

/* =========================================================
   4. DATOS ESTÁNDAR - ROLES
   ========================================================= */
INSERT INTO dbo.Roles (Nombre, Descripcion, Activo, FechaCreacion)
VALUES
('Administrador', 'Acceso total al sistema',1,GETDATE()),
('Operador', 'Acceso operativo al sistema',1,GETDATE()),
('Consulta', 'Acceso solo lectura',1,GETDATE());
GO

/* =========================================================
   5. DATOS ESTÁNDAR - USUARIOS
   Nota: PasswordHash es solo un ejemplo (NO usar en producción)
   ========================================================= */
INSERT INTO dbo.Usuarios
(
    Username,
    PasswordHash,
    Email,
    NombreCompleto,
    RolId
)
VALUES
(
    'admin',
    'admin123_HASH',
    'admin@dominio.cl',
    'Administrador del Sistema',
    1
),
(
    'operador1',
    'operador123_HASH',
    'operador1@dominio.cl',
    'Operador Principal',
    2
),
(
    'consulta1',
    'consulta123_HASH',
    'consulta1@dominio.cl',
    'Usuario Consulta',
    3
);
GO

/* =========================================================
   6. CONSULTA DE VERIFICACIÓN
   ========================================================= */
SELECT 
    u.UsuarioId,
    u.Username,
    u.Email,
    u.NombreCompleto,
    r.Nombre AS Rol,
    u.Activo,
    u.FechaCreacion
FROM dbo.Usuarios u
INNER JOIN dbo.Roles r ON r.RolId = u.RolId;
GO
