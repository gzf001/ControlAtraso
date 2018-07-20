using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlAtraso.Entity;

namespace ControlAtraso.UI.MVVM.Model
{
    public class Enrolamiento : ControlAtraso.UI.MVVM.NotifyBase
    {
        private string run;
        private string nombre;
        private string curso;
        
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

        public string Curso
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