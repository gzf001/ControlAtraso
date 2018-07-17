using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.Entity
{
    public class Curso
    {
        public Guid Id
        {
            get;
            set;
        }

        public int GradoCodigo
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }
    }
}