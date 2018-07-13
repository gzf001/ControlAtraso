﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlAtraso.UI.MVVM.ViewModel
{
    public class Configuracion : ObservableCollection<ControlAtraso.UI.ViewModel.Model.Configuracion>, INotifyPropertyChanged
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private ICommand forwardCommand;

        private int selectedIndex;
        private string rbdCuerpo;
        private string rbdDigito;

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

                OnPropertyChanged("RBDCuerpo");
                OnPropertyChanged("RBDDigito");
            }
        }

        public string RBDCuerpo
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].RBDCuerpo;
                }
                else
                {
                    return rbdCuerpo;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].RBDCuerpo = value;
                }
                else
                {
                    rbdCuerpo = value;
                }

                OnPropertyChanged("RBDCuerpo");
            }
        }

        //[Required(ErrorMessage = "Debes ingresar el dígito verificador")]
        public string RBDDigito
        {
            get
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    return this.Items[this.SelectedIndexOfCollection].RBDDigito;
                }
                else
                {
                    return rbdDigito;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > 0)
                {
                    this.Items[this.SelectedIndexOfCollection].RBDDigito = value;
                }
                else
                {
                    rbdDigito = value;
                }

                OnPropertyChanged("RBDDigito");
            }
        }

        public ICommand ForwardCommand
        {
            get
            {
                return forwardCommand;
            }
            set
            {
                forwardCommand = value;
            }
        }

        public Configuracion()
        {
            //ControlAtraso.UI.ViewModel.Model.Configuracion c = new UI.ViewModel.Model.Configuracion
            //{
            //    RBDCuerpo = string.Empty,
            //    RBDDigito = string.Empty
            //};

            this.ForwardCommand = new CommandBase(x => this.Forward());
        }

        private void Forward()
        {
            
            string cuerpo = this.RBDCuerpo;
            string digito = this.RBDDigito;
        }
    }
}