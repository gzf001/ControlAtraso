using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.Entity
{
    public class Persona
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Nombres
        {
            get;
            set;
        }

        public string Nombre
        {
            get;
            set;
        }

        public string ApellidoPaterno
        {
            get;
            set;
        }

        public string ApellidoMaterno
        {
            get;
            set;
        }

        public int RunCuerpo
        {
            get;
            set;
        }

        public char RunDigito
        {
            get;
            set;
        }

        public string Run
        {
            get;
            set;
        }
    }
}