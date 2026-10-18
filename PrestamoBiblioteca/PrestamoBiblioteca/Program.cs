using PrestamoBiblioteca.Aplicacion;
using PrestamoBiblioteca.Persistencia;
using System;

namespace PrestamoBiblioteca
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var iniciar = new InicializadorBaseDatos();
                iniciar.Inicializar();

                var repo = new RepositorioBiblioteca(iniciar.CadenaConexion);
                var menu = new MenuBiblioteca(repo);

                menu.Ejecutar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se puede iniciar: {ex.Message}");
            }
        }
    }
}