using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSanatorio
{
    class Intervencion
    {
        public string codigo;
        public string descripcion;
        public string especialidad;
        public double arancel;
        public string complejidad;
        public double adicional;

        public Intervencion (string codigo,string descripcion, string especialidad, double arancel, string complejidad, double adicional){
            this.codigo = codigo;
            this.descripcion = descripcion;
            this.especialidad = especialidad;
            this.arancel = arancel;
            this.complejidad = complejidad;
            this. adicional = adicional;
        }

        public double CalcularCosto(){
            if(string.Compare(complejidad, "alta") == 0){
                return arancel + (arancel * adicional/100);
            }else{
                return arancel;
                }
    }
}
