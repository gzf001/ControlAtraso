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
    public class HomeEnrolamiento : ObservableCollection<ControlAtraso.UI.MVVM.Model.HomeEnrolamiento>, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private ICommand seekCommand;

        private int selectedIndex;
        private string runCuerpo;
        private char runDigito;
        private string nombre;
        private ControlAtraso.Entity.TipoEducacion tipoEducacion;
        private ControlAtraso.Entity.Grado grado;
        private ControlAtraso.Entity.Curso curso;
        private List<ControlAtraso.Entity.Alumno> alumnos;

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

                OnPropertyChanged("RunCuerpo");
                OnPropertyChanged("RunDigito");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("TipoEducacion");
                OnPropertyChanged("Grado");
                OnPropertyChanged("Curso");
                OnPropertyChanged("Alumnos");
            }
        }

        public string RunCuerpo
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].RunCuerpo;
                }
                else
                {
                    return runCuerpo;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].RunCuerpo = value;
                }
                else
                {
                    runCuerpo = value;
                }

                OnPropertyChanged("RunCuerpo");
            }
        }

        public char RunDigito
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].RunDigito;
                }
                else
                {
                    return runDigito;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].RunDigito = value;
                }
                else
                {
                    runDigito = value;
                }

                OnPropertyChanged("RunDigito");
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

        public ControlAtraso.Entity.TipoEducacion TipoEducacion
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].TipoEducacion;
                }
                else
                {
                    return tipoEducacion;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].TipoEducacion = value;
                }
                else
                {
                    tipoEducacion = value;
                }

                OnPropertyChanged("TipoEducacion");

                OnPropertyChanged("Grados");

                OnPropertyChanged("Cursos");
            }
        }

        public List<ControlAtraso.Entity.TipoEducacion> TiposEducacion
        {
            get
            {
                ControlAtraso.Entity.TipoEducacion tipoEducacion = new ControlAtraso.Entity.TipoEducacion
                {
                    Codigo = -1,
                    Nombre = "[Seleccione]"
                };

                List<ControlAtraso.Entity.TipoEducacion> tiposEducacion = ControlAtraso.TipoEducacion.GetAll();

                tiposEducacion.Add(tipoEducacion);

                tiposEducacion = tiposEducacion.OrderBy(x => x.Codigo).ToList<ControlAtraso.Entity.TipoEducacion>();

                return tiposEducacion;
            }
        }

        public ControlAtraso.Entity.Grado Grado
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].Grado;
                }
                else
                {
                    return grado;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].Grado = value;
                }
                else
                {
                    grado = value;
                }

                OnPropertyChanged("Grado");

                OnPropertyChanged("Cursos");
            }
        }

        public List<ControlAtraso.Entity.Grado> Grados
        {
            get
            {
                ControlAtraso.Entity.Grado tipoEducacion = new ControlAtraso.Entity.Grado
                {
                    Codigo = -1,
                    Nombre = "[Seleccione]"
                };

                List<ControlAtraso.Entity.Grado> grados = ControlAtraso.Grado.GetAll(this.TipoEducacion);

                grados.Add(tipoEducacion);

                grados = grados.OrderBy(x => x.Codigo).ToList<ControlAtraso.Entity.Grado>();

                return grados;
            }
        }

        public ControlAtraso.Entity.Curso Curso
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

                this.Alumnos = ControlAtraso.Alumno.GetAll(this.Curso);

                OnPropertyChanged("Alumnos");
            }
        }

        public List<ControlAtraso.Entity.Curso> Cursos
        {
            get
            {
                ControlAtraso.Entity.Curso curso = new ControlAtraso.Entity.Curso
                {
                    Id = default(Guid),
                    Nombre = "[Seleccione]"
                };

                List<ControlAtraso.Entity.Curso> cursos = ControlAtraso.Curso.GetAll(this.Grado);

                cursos.Add(curso);

                cursos = cursos.OrderBy(x => x.GradoCodigo).ToList<ControlAtraso.Entity.Curso>();

                return cursos;
            }
        }

        public List<ControlAtraso.Entity.Alumno> Alumnos
        {
            get
            {
                return alumnos;
            }
            set
            {
                alumnos = value;
            }
        }

        public ICommand SeekCommand
        {
            get
            {
                return seekCommand;
            }
            set
            {
                seekCommand = value;
            }
        }

        public HomeEnrolamiento()
        {
            ControlAtraso.UI.MVVM.Model.HomeEnrolamiento c = new UI.MVVM.Model.HomeEnrolamiento
            {
                RunCuerpo = string.Empty,
                RunDigito = ' ',
                Nombre = string.Empty
            };

            this.SeekCommand = new CommandBase(x => this.Seek());
        }

        private void Seek()
        {
            if (!string.IsNullOrEmpty(this.RunCuerpo))
            {
                this.Alumnos = ControlAtraso.Alumno.GetAll(int.Parse(this.RunCuerpo), this.RunDigito);

                if (this.Alumnos[0].Persona == null)
                {
                    System.Windows.MessageBox.Show("La búsqueda no arrojo resultados", "Insignia", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                }

                OnPropertyChanged("Alumnos");
            }
            else if (!string.IsNullOrEmpty(this.Nombre))
            {
                if (this.Nombre.Length < 4)
                {
                    System.Windows.MessageBox.Show("El largo del texto buscado debe ser mayor a 4 caracteres", "Insignia", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                }
                else
                {
                    this.Alumnos = ControlAtraso.Alumno.GetAll(this.Nombre);

                    if (this.Alumnos.Count == 0)
                    {
                        System.Windows.MessageBox.Show("La búsqueda no arrojo resultados", "Insignia", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    }

                    OnPropertyChanged("Alumnos");
                }
            }
        }
    }
}