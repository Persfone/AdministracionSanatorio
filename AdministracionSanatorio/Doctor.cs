using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    public class Doctor
    {
        public string NombreApellido { get; private set; }
        public string Matricula { get; private set; }
        public string Especialidad { get; private set; }
        public bool Disponible { get; private set; }
    
        public Doctor(string nombreApellido, string matricula, string especialidad, bool disponible)
        {
            NombreApellido = nombreApellido;
            Matricula = matricula;
            Especialidad = especialidad;
            Disponible = disponible;
        }
    
        public void AsignarDisponible(bool disponible)
        {
            Disponible = disponible;
        }
        
        public bool EstaDisponible() 
        {
            return Disponible;
        }
     }
}
