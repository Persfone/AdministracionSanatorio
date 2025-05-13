using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    class Doctor
    {
        string nombreApellido;
        string matricula;
        string especialidad;
        bool disponible;

        public Doctor(string nombreApellido, string matricula, string especialidad) {
            this.nombreApellido = nombreApellido;
            this.matricula = matricula;
            this.especialidad=especialidad;
            }    
        
        public void AsignarDisponible(bool a) {
            disponible = a;
         }
        public bool EstaDisponible() {
            return disponible;
    }
}
