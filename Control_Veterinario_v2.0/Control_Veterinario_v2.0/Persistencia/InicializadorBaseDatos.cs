using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace SistemaVeterinaria.Persistencia
{
    public class InicializadorBaseDatos
    {
        public string CadenaConexion { get; }

        public InicializadorBaseDatos()
        {
            string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UMG", "Veterinaria");
            Directory.CreateDirectory(carpeta);
            CadenaConexion = $"Data Source={Path.Combine(carpeta, "clinica.db")}";
        }

        public void Inicializar()
        {
            using var conexion = new SqliteConnection(CadenaConexion);
            conexion.Open();
            using var comando = conexion.CreateCommand();
            comando.CommandText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseDatos", "crear_tablas.sql"));
            comando.ExecuteNonQuery();

            // Insertar veterinario por defecto si no existe
            comando.CommandText = "INSERT INTO Veterinarios (Nombre, Especialidad) SELECT 'Dr. López', 'Medicina General' WHERE NOT EXISTS (SELECT 1 FROM Veterinarios WHERE Nombre = 'Dr. López');";
            comando.ExecuteNonQuery();
        }
    }
}