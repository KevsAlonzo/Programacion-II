using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace PrestamoBiblioteca.Persistencia
{
    public class InicializadorBaseDatos
    {
        public string CadenaConexion { get; }

        public InicializadorBaseDatos()
        {
            // Creación de la base de datos en AppData local para que no se borre en cada compilación
            string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UMG", "PrestamoBiblioteca");
            Directory.CreateDirectory(carpeta);
            string rutaDb = Path.Combine(carpeta, "biblioteca.db");
            CadenaConexion = $"Data Source={rutaDb}";
        }

        public void Inicializar()
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open();

            using var comando = conexion.CreateCommand();
            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Lectores(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Codigo TEXT NOT NULL,
                    Nombre TEXT NOT NULL,
                    Correo TEXT NOT NULL,
                    Telefono TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Libros(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Codigo TEXT NOT NULL,
                    Titulo TEXT NOT NULL,
                    Autor TEXT NOT NULL,
                    Ejemplares INTEGER NOT NULL CHECK (Ejemplares >= 0)
                );

                CREATE TABLE IF NOT EXISTS Prestamos(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    LectorId INTEGER NOT NULL,
                    LibroId INTEGER NOT NULL,
                    FechaPrestamo TEXT NOT NULL, 
                    FechaLimite TEXT NOT NULL,
                    FechaDevolucion TEXT,
                    CHECK (date(FechaLimite) >= date(FechaPrestamo)),
                    FOREIGN KEY (LectorId) REFERENCES Lectores(Id) ON DELETE CASCADE,
                    FOREIGN KEY (LibroId) REFERENCES Libros(Id) ON DELETE CASCADE
                );";

            comando.ExecuteNonQuery();
        }
    }
}