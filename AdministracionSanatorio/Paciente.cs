using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    class Paciente
    {
        public string dni;
        public string nombreApellido;
        public string telefono;
        public bool obraSocial;
        public string nombreObraSocial;
        public double montoCobertura;
        public List<Intervencion> intervenciones;

        public Paciente(string dni, string nombreApellido, string telefono, bool obraSocial, string nombreObraSocial, double montoCobertura, Intervencion intervenciones){
            this.dni = dni;
            this.nombreApellido = nombreApellido;
            this.telefono = telefono;
            this.obraSocial = obraSocial;

            this.intervenciones = new List<Intervencion>();

            if(obraSocial){
                this.nombreObraSocial = nombreObraSocial;
                this.montoCobertura = montoCobertura;
            }else{
                this.nombreObraSocial = "sin obra social";
                this.montoCobertura = 0;
            }
        }
        public void AgregarIntervencion()   
    }
}
