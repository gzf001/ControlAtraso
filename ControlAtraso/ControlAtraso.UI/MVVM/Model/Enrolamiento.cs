using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.UI.MVVM.Model
{
    public class Enrolamiento : ControlAtraso.UI.MVVM.NotifyBase
    {
        private string run;
        private string nombre;
        private ControlAtraso.Entity.TipoEducacion tipoEducacion;
        private ControlAtraso.Entity.Grado grado;
        private ControlAtraso.Entity.Curso curso;

        public string Run
        {
            get
            {
                return run;
            }
            set
            {
                run = value;

                OnPropertyChanged("Run");
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;

                OnPropertyChanged("Nombre");
            }
        }

        public ControlAtraso.Entity.TipoEducacion TipoEducacion
        {
            get
            {
                return tipoEducacion;
            }
            set
            {
                tipoEducacion = value;

                OnPropertyChanged("TipoEducacion");
            }
        }

        public ControlAtraso.Entity.Grado Grado
        {
            get
            {
                return grado;
            }
            set
            {
                grado = value;

                OnPropertyChanged("Grado");
            }
        }

        public ControlAtraso.Entity.Curso Curso
        {
            get
            {
                return curso;
            }
            set
            {
                curso = value;

                OnPropertyChanged("Curso");
            }
        }
    }
}