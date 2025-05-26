using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// use herencia para hacer dos tipos de intervencion
namespace AdministracionSanatorio
{
    /// <summary>
    /// Clase abstracta q funciona como plantilla para las demas clases de intervencion (alta y comun)
    /// </summary>
    public abstract class Intervencion
    {
        public string Codigo { get; protected set; }
        public string Descripcion { get; protected set; }
        public string Especialidad { get; protected set; }
        public double Arancel { get; protected set; }


        public abstract double CalcularCosto();
    }

    /// <summary>
    /// clase intervencion comun, sin agregado de costo adicional
    /// </summary>
    class IntervencionComun : Intervencion
    {
        public IntervencionComun(string codigo, string descripcion, string especialidad, double arancel)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Especialidad = especialidad;
            Arancel = arancel;
        }

        public override double CalcularCosto()
        {
            return Arancel;
        }
    }

    /// <summary>
    /// Clase intervencion alta complejidad, con agregado de costo adicional
    /// </summary>
    class IntervencionAltaComplejidad : Intervencion
    {
        public double PorcentajeAdicional { get; private set; }

        public IntervencionAltaComplejidad(string codigo, string descripcion, string especialidad, double arancel)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Especialidad = especialidad;
            Arancel = arancel;
            PorcentajeAdicional = 0.25;  
        }

        public override double CalcularCosto()
        {
            return Arancel + (Arancel * PorcentajeAdicional);
        }
    }
    
}

