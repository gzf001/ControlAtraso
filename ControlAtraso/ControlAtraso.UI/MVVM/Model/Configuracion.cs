using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ControlAtraso.UI.MVVM.Model
{
    public class Configuracion : ControlAtraso.UI.MVVM.NotifyBase
    {
        private string rbdCuerpo;
        private string rbdDigito;

        public string RBDCuerpo
        {
            get
            {
                return rbdCuerpo;
            }
            set
            {
                rbdCuerpo = value;

                OnPropertyChanged("RBDCuerpo");
            }
        }

        public string RBDDigito
        {
            get
            {
                return rbdDigito;
            }
            set
            {
                rbdDigito = value;

                OnPropertyChanged("RBDDigito");
            }
        }
    }
}