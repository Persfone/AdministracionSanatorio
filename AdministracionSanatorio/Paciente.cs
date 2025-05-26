using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    public class Paciente
    {
        
        public string Dni { get; private set; }
        public string NombreApellido { get; private set; }
        public string Telefono { get; private set; }
        public bool TieneObraSocial { get; private set; }
        public string NombreObraSocial { get; private set; }
        public double MontoCobertura { get; private set; }

        /// <summary>
        /// decid
        /// </summary>
        public List<IntervencionRealizada> Intervenciones { get; private set; }

        // Constructor
        public Paciente(string dni, string nombreApellido, string telefono, string nombreObraSocial, double montoCobertura)
        {
            Dni = dni;
            NombreApellido = nombreApellido;
            Telefono = telefono;
            Intervenciones = new List<IntervencionRealizada>();

            // Manejo de obra social
            if (!string.IsNullOrEmpty(nombreObraSocial))
            {
                TieneObraSocial = true;
                NombreObraSocial = nombreObraSocial;
                MontoCobertura = montoCobertura;
            }
            else
            {
                TieneObraSocial = false;
                NombreObraSocial = "-";
                MontoCobertura = 0;
            }
        }

        // Método para agregar una intervención
        public void AgregarIntervencion(Intervencion intervencion, Doctor medico, DateTime fecha)
        {
            if (medico.Especialidad != intervencion.Especialidad)
            {
                throw new InvalidOperationException("La especialidad del médico no coincide con la intervención");
            }

            if (!medico.Disponible)
            {
                throw new InvalidOperationException("El médico no está disponible");
            }

            var nuevaIntervencion = new IntervencionRealizada(
                intervencion,
                medico,
                fecha,
                false // Por defecto no está pagado
            );

            Intervenciones.Add(nuevaIntervencion);
        }

        // Método para calcular el costo total de las intervenciones
        public double CalcularCostoTotal()
        {
            double total = 0;
            foreach (var intervencion in Intervenciones)
            {
                total += intervencion.CalcularCostoPaciente(this);
            }
            return total;
        }

        // Método para calcular el costo pendiente de pago
        public double CalcularCostoPendiente()
        {
            double pendiente = 0;
            foreach (var intervencion in Intervenciones)
            {
                if (!intervencion.Pagado)
                {
                    pendiente += intervencion.CalcularCostoPaciente(this);
                }
            }
            return pendiente;
        }

        // Método para marcar una intervención como pagada
        public void MarcarComoPagado(int idIntervencion)
        {
            var intervencion = Intervenciones.Find(i => i.Id == idIntervencion);
            if (intervencion != null)
            {
                intervencion.Pagado = true;
            }
        }
    }
}