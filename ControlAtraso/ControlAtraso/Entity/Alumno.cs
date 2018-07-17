using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.Entity
{
    public class Alumno
    {
        public Persona Persona
        {
            get;
            set;
        }

        public Matricula Matricula
        {
            get;
            set;
        }
    }
}