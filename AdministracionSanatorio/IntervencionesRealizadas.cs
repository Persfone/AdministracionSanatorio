using System;

namespace AdministracionSanatorio
{
    public class IntervencionRealizada
    {
        private static int _contadorId = 1;

        public int Id { get; private set; }
        public Intervencion Intervencion { get; private set; }
        public Doctor Medico { get; private set; }
        public DateTime Fecha { get; private set; }
        public bool Pagado { get; set; }

        public IntervencionRealizada(Intervencion intervencion, Doctor medico, DateTime fecha, bool pagado)
        {
            Id = _contadorId++;
            Intervencion = intervencion;
            Medico = medico;
            Fecha = fecha;
            Pagado = pagado;
        }

        public double CalcularCostoPaciente(Paciente paciente)
        {
            double costo = Intervencion.CalcularCosto();

            if (paciente.TieneObraSocial)
            {
                double descuento = costo * (paciente.MontoCobertura / 100);
                return costo - descuento;
            }

            return costo;
        }
    }
}
