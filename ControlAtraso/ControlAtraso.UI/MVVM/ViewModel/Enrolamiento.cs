using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlAtraso.UI.MVVM.ViewModel
{
    public class Enrolamiento : ObservableCollection<ControlAtraso.UI.MVVM.Model.Enrolamiento>, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private ICommand saveCommand;

        private int selectedIndex;
        private string run;
        private string nombre;
        private string curso;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int SelectedIndexOfCollection
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;

                OnPropertyChanged("SelectedIndexOfCollection");

                OnPropertyChanged("Run");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("Curso");
            }
        }

        public string Run
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Run;
                }
                else
                {
                    return run;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Run = value;
                }
                else
                {
                    run = value;
                }

                OnPropertyChanged("Run");
            }
        }

        public string Nombre
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Nombre;
                }
                else
                {
                    return nombre;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Nombre = value;
                }
                else
                {
                    nombre = value;
                }

                OnPropertyChanged("Nombre");
            }
        }

        public string Curso
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Curso;
                }
                else
                {
                    return curso;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Curso = value;
                }
                else
                {
                    curso = value;
                }

                OnPropertyChanged("Curso");
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }
            set
            {
                saveCommand = value;
            }
        }

        public Enrolamiento()
        {
            ControlAtraso.UI.MVVM.Model.Enrolamiento e = new UI.MVVM.Model.Enrolamiento
            {
                Run = string.Empty,
                Nombre = string.Empty,
                Curso = string.Empty
            };

            this.SaveCommand = new CommandBase(x => this.Save());
        }

        public Enrolamiento(ControlAtraso.Entity.Alumno alumno)
        {
            ControlAtraso.UI.MVVM.Model.Enrolamiento e = new UI.MVVM.Model.Enrolamiento
            {
                Run = alumno.Persona.Run,
                Nombre = alumno.Persona.Nombre,
                Curso = alumno.Matricula.Curso
            };

            this.SaveCommand = new CommandBase(x => this.Save());
        }

        private void Save()
        {
            
        }
    }
}