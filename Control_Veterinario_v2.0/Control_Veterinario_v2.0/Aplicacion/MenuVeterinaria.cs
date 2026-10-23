using System;
using System.Data;
using SistemaVeterinaria.Dominio;
using SistemaVeterinaria.Persistencia;

namespace SistemaVeterinaria.Aplicacion
{
    public class MenuVeterinaria
    {
        private readonly RepositorioVeterinaria _repo;
        public MenuVeterinaria(RepositorioVeterinaria repo) => _repo = repo;

        public void Ejecutar()
        {
            int opcion = -1;
            while (opcion != 0)
            {
                Console.Clear();
                Console.WriteLine("\n=== SISTEMA CLÍNICO VETERINARIO (SQLite) ===");
                Console.WriteLine("\n=== Kevin Anderson Alonzo Bolvito 9941-25-356 ===");
                Console.WriteLine("1. Registrar Ingreso (Nuevo Paciente)");
                Console.WriteLine("2. Realizar Triaje (Peso y Motivo)");
                Console.WriteLine("3. Atender Consulta (Médico)");
                Console.WriteLine("4. Actualizar Carnet (Vacunas)");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("5. Listar Clientes");
                Console.WriteLine("6. Listar Mascotas");
                Console.WriteLine("7. Listar Consultas y Estado");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                // Implementación del try-catch
                try
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Write("Nombre Dueño: "); string nomD = Console.ReadLine();
                            Console.Write("Teléfono: "); string tel = Console.ReadLine();
                            Console.Write("Mascota: "); string nomM = Console.ReadLine();
                            Console.Write("Raza: "); string raza = Console.ReadLine();
                            _repo.RegistrarIngreso(nomD, tel, nomM, raza);
                            Console.WriteLine("Ingreso registrado exitosamente en la Base de Datos.");
                            break;
                        case 2:
                            Console.Write("Nombre de la mascota para triaje: ");
                            var mascota = _repo.BuscarMascota(Console.ReadLine());
                            if (mascota == null) throw new Exception("La mascota no existe. Debe registrarla primero."); // Forzamos el error al catch

                            Console.Write("Peso (kg): ");
                            if (!double.TryParse(Console.ReadLine(), out double peso)) throw new Exception("Formato de peso inválido.");

                            Console.Write("Motivo de visita: "); string motivo = Console.ReadLine();
                            _repo.GuardarTriaje(mascota.Id, peso, motivo);
                            Console.WriteLine("Triaje guardado. Consulta en cola.");
                            break;
                        case 3:
                            Console.Write("Síntomas detectados: "); string sin = Console.ReadLine();
                            Console.Write("Diagnóstico/Tratamiento: "); string diag = Console.ReadLine();
                            _repo.AtenderConsulta(sin, diag);
                            Console.WriteLine("Consulta finalizada correctamente.");
                            break;
                        case 4:
                            Console.Write("Nombre de la mascota: ");
                            var masc = _repo.BuscarMascota(Console.ReadLine());
                            if (masc == null) throw new Exception("Mascota no encontrada en el sistema.");

                            Console.Write("Vacuna aplicada: "); string vac = Console.ReadLine();
                            _repo.ActualizarCarnet(masc.Id, vac, true);
                            Console.WriteLine("Carnet actualizado con éxito.");
                            break;
                        case 5:
                            MostrarTabla(_repo.ObtenerClientes(), "LISTA DE CLIENTES REGISTRADOS");
                            break;
                        case 6:
                            MostrarTabla(_repo.ObtenerMascotas(), "LISTA DE MASCOTAS (PACIENTES)");
                            break;
                        case 7:
                            MostrarTabla(_repo.ObtenerConsultas(), "HISTORIAL DE CONSULTAS");
                            break;
                    }
                }
                catch (Exception error)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError controlado con try-catch: {error.Message}");
                    Console.ResetColor();
                }
                Console.WriteLine("\nPresione Enter para continuar...");
                Console.ReadLine();
            }
        }
        private void MostrarTabla(DataTable tabla, string titulo)
        {
            Console.WriteLine($"\n--- {titulo} ---");
            if (tabla.Rows.Count == 0)
            {
                Console.WriteLine("No hay registros para mostrar.");
                return;
            }

            foreach (DataRow fila in tabla.Rows)
            {
                var valores = fila.ItemArray;
                Console.WriteLine(string.Join(" | ", valores));
            }
            Console.WriteLine("-------------------------");
        }
    }
}