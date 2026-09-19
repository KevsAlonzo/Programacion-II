using System;
using System.Collections.Generic;

namespace SistemaVeterinaria
{
    // ================
    // 1. CLASES BASE 
    // ================

    public class Cliente
    {
        public string strNombreCompleto { get; set; }
        public string strTelefono { get; set; }

        public Cliente(string nombre, string telefono)
        {
            this.strNombreCompleto = nombre;
            this.strTelefono = telefono;
        }
    }

    public class Carnet
    {
        public string strUltimaVacuna { get; set; }
        public bool blnTieneCollarRabia { get; set; }

        public Carnet()
        {
            this.strUltimaVacuna = "Ninguna";
            this.blnTieneCollarRabia = false;
        }

        public void ActualizarCarnet(string vacuna, bool collar)
        {
            this.strUltimaVacuna = vacuna;
            this.blnTieneCollarRabia = collar;
        }
    }

    public class Mascota
    {
        public string strNombre { get; set; }
        public string strRaza { get; set; }
        public double dblPeso { get; set; }
        public Cliente objDueno { get; set; }
        public Carnet objCarnet { get; set; } // Composición

        public Mascota(string nombre, string raza, Cliente dueno)
        {
            this.strNombre = nombre;
            this.strRaza = raza;
            this.dblPeso = 0.0;
            this.objDueno = dueno;
            this.objCarnet = new Carnet();
        }
    }

    public class Veterinario
    {
        public string strNombre { get; set; }
        public string strEspecialidad { get; set; }

        public Veterinario(string nombre, string especialidad)
        {
            this.strNombre = nombre;
            this.strEspecialidad = especialidad;
        }
    }

    public class Consulta
    {
        public Mascota objPaciente { get; set; }
        public Veterinario objDoctor { get; set; }
        public string strMotivo { get; set; }
        public string strSintomas { get; set; }
        public string strDiagnostico { get; set; }

        public Consulta(Mascota paciente, Veterinario doctor, string motivo)
        {
            this.objPaciente = paciente;
            this.objDoctor = doctor;
            this.strMotivo = motivo;
            this.strSintomas = "Ninguno";
            this.strDiagnostico = "Pendiente";
        }
    }

    // ===================
    // 2. GESTOR CENTRAL 
    // ===================

    public class GestorVeterinaria
    {
        public List<Cliente> lstClientes { get; set; }
        public List<Mascota> lstMascotas { get; set; }
        public List<Veterinario> lstVeterinarios { get; set; }
        public List<Consulta> lstConsultas { get; set; }

        public GestorVeterinaria()
        {
            lstClientes = new List<Cliente>();
            lstMascotas = new List<Mascota>();
            lstVeterinarios = new List<Veterinario>();
            lstConsultas = new List<Consulta>();
        }

        // Método buscar 
        public Mascota BuscarMascota(string nombre)
        {
            return lstMascotas.Find(m => m.strNombre.ToLower() == nombre.ToLower());
        }
    }

    // ==========================================
    // 3. PROGRAMA PRINCIPAL (UI de Consola)
    // ==========================================

    class Program
    {
        static void Main(string[] args)
        {
            GestorVeterinaria gestor = new GestorVeterinaria();

            Veterinario vetTurno = new Veterinario("Dr. López", "Medicina General");
            gestor.lstVeterinarios.Add(vetTurno);

            int opcion = -1;

            while (opcion != 0)
            {
                Console.WriteLine("\n=== SISTEMA DE CONTROL CLÍNICO VETERINARIO ===");
                Console.WriteLine("1. Registrar Ingreso (Nuevo Paciente)");
                Console.WriteLine("2. Realizar Triaje (Peso y Motivo)");
                Console.WriteLine("3. Atender Consulta (Enfermedad / Preventivo)");
                Console.WriteLine("4. Actualizar Carnet");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1: // CU1: Registrar Ingreso
                        Console.Write("Nombre del Dueño: ");
                        string nomDueno = Console.ReadLine();
                        Console.Write("Teléfono: ");
                        string tel = Console.ReadLine();
                        Cliente nuevoCliente = new Cliente(nomDueno, tel);
                        gestor.lstClientes.Add(nuevoCliente);

                        Console.Write("Nombre de la Mascota: ");
                        string nomMascota = Console.ReadLine();
                        Console.Write("Raza: ");
                        string raza = Console.ReadLine();

                        Mascota nuevaMascota = new Mascota(nomMascota, raza, nuevoCliente);
                        gestor.lstMascotas.Add(nuevaMascota);
                        Console.WriteLine("¡Ingreso registrado! Se ha creado un nuevo carnet y entregado collar antirrábico.");
                        break;

                    case 2: // CU2: Realizar Triaje
                        Console.Write("Ingrese nombre de la mascota para triaje: ");
                        Mascota mascotaTriaje = gestor.BuscarMascota(Console.ReadLine());

                        if (mascotaTriaje != null)
                        {
                            Console.Write("Ingrese peso actual (kg): ");
                            if (double.TryParse(Console.ReadLine(), out double peso))
                            {
                                mascotaTriaje.dblPeso = peso;
                            }
                            Console.Write("Motivo de visita (Ej. Rutina, Gastritis, Vómitos): ");
                            string motivo = Console.ReadLine();

                            Consulta nuevaConsulta = new Consulta(mascotaTriaje, vetTurno, motivo);
                            gestor.lstConsultas.Add(nuevaConsulta);
                            Console.WriteLine("Triaje completado. Mascota lista para pasar con el veterinario.");
                        }
                        else
                        {
                            Console.WriteLine("Mascota no encontrada. Debe registrarla primero.");
                        }
                        break;

                    case 3: // CU3 y CU4: Atender (Preventivo o Enfermedad)
                        if (gestor.lstConsultas.Count == 0)
                        {
                            Console.WriteLine("No hay consultas en cola tras el triaje.");
                            break;
                        }
                        // Atendemos la última consulta ingresada para el ejemplo
                        Consulta consultaActual = gestor.lstConsultas[gestor.lstConsultas.Count - 1];
                        Console.WriteLine($"\nAtendiendo a: {consultaActual.objPaciente.strNombre} | Motivo: {consultaActual.strMotivo}");

                        Console.Write("Ingrese los síntomas observados: ");
                        consultaActual.strSintomas = Console.ReadLine();

                        Console.Write("Ingrese el diagnóstico/tratamiento aplicado: ");
                        consultaActual.strDiagnostico = Console.ReadLine();

                        Console.WriteLine("¡Consulta finalizada exitosamente!");
                        break;

                    case 4: // CU5: Actualizar Carnet
                        Console.Write("Ingrese nombre de la mascota para actualizar carnet: ");
                        Mascota mascotaCarnet = gestor.BuscarMascota(Console.ReadLine());

                        if (mascotaCarnet != null)
                        {
                            Console.Write("Nombre de la vacuna aplicada hoy: ");
                            string vacuna = Console.ReadLine();
                            mascotaCarnet.objCarnet.ActualizarCarnet(vacuna, true);
                            Console.WriteLine($"Carnet actualizado. Última vacuna: {mascotaCarnet.objCarnet.strUltimaVacuna}");
                        }
                        else
                        {
                            Console.WriteLine("Mascota no encontrada.");
                        }
                        break;
                }
            }
        }
    }
}
