using System;
using System.Linq;


namespace AdministracionSanatorio
{
    class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- Menú Principal ---");
                Console.WriteLine("1. Dar de alta un nuevo paciente");
                Console.WriteLine("2. Listar pacientes");
                Console.WriteLine("3. Asignar nueva intervención a un paciente");
                Console.WriteLine("4. Calcular costo total de intervenciones por DNI");
                Console.WriteLine("5. Reporte de liquidaciones pendientes");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        AltaPaciente(hospital);
                        break;
                    case "2":
                        ListarPacientes(hospital);
                        break;
                    case "3":
                        AsignarIntervencion(hospital);
                        break;
                    case "4":
                        CalcularCostoPorDni(hospital);
                        break;
                    case "5":
                        ReportePendientes(hospital);
                        break;
                    case "6":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }

        static void AltaPaciente(Hospital hospital)
        {
            Console.Write("DNI: ");
            string dni = Console.ReadLine();

            if (hospital.Pacientes.Any(p => p.Dni == dni))
            {
                Console.WriteLine("El paciente ya existe.");
                return;
            }

            Console.Write("Nombre y apellido: ");
            string nombre = Console.ReadLine();

            Console.Write("Teléfono: ");
            string tel = Console.ReadLine();

            Console.Write("¿Tiene obra social? (s/n): ");
            string tieneOS = Console.ReadLine().ToLower();

            string obra = "-";
            double cobertura = 0;

            if (tieneOS == "s")
            {
                Console.Write("Nombre de la obra social: ");
                obra = Console.ReadLine();
                Console.Write("Porcentaje de cobertura: ");
                cobertura = double.Parse(Console.ReadLine());
            }

            var nuevoPaciente = new Paciente(dni, nombre, tel, obra, cobertura);
            hospital.Pacientes.Add(nuevoPaciente);

            Console.WriteLine("Paciente agregado correctamente.");
        }

        static void ListarPacientes(Hospital hospital)
        {
            foreach (var paciente in hospital.Pacientes)
            {
                Console.WriteLine($"DNI: {paciente.Dni} - {paciente.NombreApellido} - Tel: {paciente.Telefono} - Obra Social: {paciente.NombreObraSocial}");
            }
        }

        static void AsignarIntervencion(Hospital hospital)
        {
            Console.Write("DNI del paciente: ");
            string dni = Console.ReadLine();
            var paciente = hospital.Pacientes.FirstOrDefault(p => p.Dni == dni);

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado. Dándolo de alta.");
                AltaPaciente(hospital);
                paciente = hospital.Pacientes.First(p => p.Dni == dni);
            }

            Console.WriteLine("Intervenciones disponibles:");
            foreach (var i in hospital.Intervenciones)
            {
                Console.WriteLine($"{i.Codigo}: {i.Descripcion} ({i.Especialidad}) - ${i.CalcularCosto()}");
            }

            Console.Write("Ingrese código de intervención: ");
            string cod = Console.ReadLine();
            var intervencion = hospital.Intervenciones.FirstOrDefault(i => i.Codigo == cod);

            if (intervencion == null)
            {
                Console.WriteLine("Código inválido.");
                return;
            }

            var medicosDisponibles = hospital.Doctores.Where(m => m.Disponible && m.Especialidad == intervencion.Especialidad).ToList();

            if (medicosDisponibles.Count == 0)
            {
                Console.WriteLine("No hay médicos disponibles con esa especialidad.");
                return;
            }

            Console.WriteLine("Médicos disponibles:");
            for (int i = 0; i < medicosDisponibles.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {medicosDisponibles[i].NombreApellido} - Matrícula: {medicosDisponibles[i].Matricula}");
            }

            Console.Write("Seleccione médico (número): ");
            int seleccion = int.Parse(Console.ReadLine()) - 1;
            if (seleccion < 0 || seleccion >= medicosDisponibles.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            var medicoSeleccionado = medicosDisponibles[seleccion];

            Console.Write("Fecha de intervención (YYYY-MM-DD): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            try
            {
                paciente.AgregarIntervencion(intervencion, medicoSeleccionado, fecha);
                Console.WriteLine("Intervención registrada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void CalcularCostoPorDni(Hospital hospital)
        {
            Console.Write("DNI del paciente: ");
            string dni = Console.ReadLine();

            var paciente = hospital.Pacientes.FirstOrDefault(p => p.Dni == dni);
            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            double costoTotal = paciente.CalcularCostoTotal();
            Console.WriteLine($"Costo total de intervenciones: ${costoTotal}");
        }

        static void ReportePendientes(Hospital hospital)
        {
            Console.WriteLine("=== Reporte de Intervenciones Pendientes ===");

            foreach (var p in hospital.Pacientes)
            {
                foreach (var i in p.Intervenciones)
                {
                    if (!i.Pagado)
                    {
                        Console.WriteLine($"ID: {i.Id} | Fecha: {i.Fecha.ToShortDateString()} | Desc: {i.Intervencion.Descripcion} | " +
                                          $"Paciente: {p.NombreApellido} | Médico: {i.Medico.NombreApellido} (Mat: {i.Medico.Matricula}) | " +
                                          $"Obra Social: {p.NombreObraSocial} | Total a Pagar: ${i.CalcularCostoPaciente(p)}");
                    }
                }
            }
        }
    }
}
