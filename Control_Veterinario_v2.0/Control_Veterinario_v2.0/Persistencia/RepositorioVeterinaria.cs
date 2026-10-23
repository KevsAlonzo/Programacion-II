using Microsoft.Data.Sqlite;
using SistemaVeterinaria.Dominio;
using System;
using System.Data;

namespace SistemaVeterinaria.Persistencia
{
    public class RepositorioVeterinaria
    {
        private readonly string _cadenaConexion;
        public RepositorioVeterinaria(string cadena) => _cadenaConexion = cadena;

        public void RegistrarIngreso(string nombreCliente, string telefono, string nombreMascota, string raza)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();

            using var cmdCliente = conexion.CreateCommand();
            cmdCliente.CommandText = "INSERT INTO Clientes (Nombre, Telefono) VALUES ($nombre, $tel) RETURNING Id;";
            cmdCliente.Parameters.AddWithValue("$nombre", nombreCliente);
            cmdCliente.Parameters.AddWithValue("$tel", telefono);
            int clienteId = Convert.ToInt32(cmdCliente.ExecuteScalar());

            using var cmdMascota = conexion.CreateCommand();
            cmdMascota.CommandText = "INSERT INTO Mascotas (ClienteId, Nombre, Raza, Peso, UltimaVacuna, TieneCollar) VALUES ($cli, $nom, $raza, 0, 'Ninguna', 1);";
            cmdMascota.Parameters.AddWithValue("$cli", clienteId);
            cmdMascota.Parameters.AddWithValue("$nom", nombreMascota);
            cmdMascota.Parameters.AddWithValue("$raza", raza);
            cmdMascota.ExecuteNonQuery();
        }

        public Mascota BuscarMascota(string nombre)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "SELECT Id, ClienteId, Nombre, Raza, Peso, UltimaVacuna, TieneCollar FROM Mascotas WHERE LOWER(Nombre) = LOWER($nom) LIMIT 1;";
            cmd.Parameters.AddWithValue("$nom", nombre);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Mascota
                {
                    Id = reader.GetInt32(0),
                    ClienteId = reader.GetInt32(1),
                    Nombre = reader.GetString(2),
                    Raza = reader.GetString(3),
                    Peso = reader.GetDouble(4),
                    UltimaVacuna = reader.GetString(5),
                    TieneCollar = reader.GetInt32(6) == 1
                };
            }
            return null;
        }

        public void GuardarTriaje(int mascotaId, double peso, string motivo)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            using var cmdPeso = conexion.CreateCommand();
            cmdPeso.CommandText = "UPDATE Mascotas SET Peso = $peso WHERE Id = $id;";
            cmdPeso.Parameters.AddWithValue("$peso", peso);
            cmdPeso.Parameters.AddWithValue("$id", mascotaId);
            cmdPeso.ExecuteNonQuery();

            using var cmdConsulta = conexion.CreateCommand();
            cmdConsulta.CommandText = "INSERT INTO Consultas (MascotaId, VeterinarioId, Motivo, Sintomas, Diagnostico, Estado) VALUES ($masc, 1, $motivo, 'Pendiente', 'Pendiente', 'En Espera');";
            cmdConsulta.Parameters.AddWithValue("$masc", mascotaId);
            cmdConsulta.Parameters.AddWithValue("$motivo", motivo);
            cmdConsulta.ExecuteNonQuery();
        }

        public void AtenderConsulta(string sintomas, string diagnostico)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "UPDATE Consultas SET Sintomas = $sin, Diagnostico = $diag, Estado = 'Finalizada' WHERE Estado = 'En Espera' AND Id = (SELECT Id FROM Consultas WHERE Estado = 'En Espera' ORDER BY Id ASC LIMIT 1);";
            cmd.Parameters.AddWithValue("$sin", sintomas);
            cmd.Parameters.AddWithValue("$diag", diagnostico);
            if (cmd.ExecuteNonQuery() == 0) throw new Exception("No hay consultas pendientes en cola.");
        }

        public void ActualizarCarnet(int mascotaId, string vacuna, bool collar)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            using var cmd = conexion.CreateCommand();
            cmd.CommandText = "UPDATE Mascotas SET UltimaVacuna = $vacuna, TieneCollar = $collar WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$vacuna", vacuna);
            cmd.Parameters.AddWithValue("$collar", collar ? 1 : 0);
            cmd.Parameters.AddWithValue("$id", mascotaId);
            cmd.ExecuteNonQuery();
        }
        public DataTable ObtenerClientes() => Consultar(
            "SELECT Id, Nombre, Telefono FROM Clientes ORDER BY Id;");

        public DataTable ObtenerMascotas() => Consultar(@"
            SELECT m.Id, c.Nombre AS Dueño, m.Nombre AS Mascota, m.Raza, m.Peso, m.UltimaVacuna, 
                   CASE WHEN m.TieneCollar = 1 THEN 'Sí' ELSE 'No' END AS Collar
            FROM Mascotas m 
            INNER JOIN Clientes c ON m.ClienteId = c.Id 
            ORDER BY m.Id;");

        public DataTable ObtenerConsultas() => Consultar(@"
            SELECT c.Id, m.Nombre AS Mascota, v.Nombre AS Veterinario, c.Motivo, c.Sintomas, c.Diagnostico, c.Estado 
            FROM Consultas c 
            INNER JOIN Mascotas m ON c.MascotaId = m.Id 
            INNER JOIN Veterinarios v ON c.VeterinarioId = v.Id 
            ORDER BY c.Id DESC;");

        // Método auxiliar para convertir resultados SQL en Tablas
        private DataTable Consultar(string sql)
        {
            using var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            using var comando = conexion.CreateCommand();
            comando.CommandText = sql;
            using var lector = comando.ExecuteReader();
            var tabla = new DataTable();
            tabla.Load(lector);
            return tabla;
        }
    }
}