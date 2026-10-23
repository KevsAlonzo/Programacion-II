using System;
using SistemaVeterinaria.Aplicacion;
using SistemaVeterinaria.Persistencia;

namespace SistemaVeterinaria
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var inicializador = new InicializadorBaseDatos();
                inicializador.Inicializar();

                var repositorio = new RepositorioVeterinaria(inicializador.CadenaConexion);
                new MenuVeterinaria(repositorio).Ejecutar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico: {ex.Message}");
            }
        }
    }
}