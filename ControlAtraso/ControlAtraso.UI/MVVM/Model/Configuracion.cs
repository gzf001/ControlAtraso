using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.UI.ViewModel.Model
{
    public class Configuracion : ControlAtraso.UI.MVVM.NotifyBase
    {
        private string rbdCuerpo;
        private string rbdDigito;

        [Required(ErrorMessage = "Debes ingresar el cuerpo")]
        public string RBDCuerpo
        {
            get
            {
                return rbdCuerpo;
            }
            set
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = "RBDCuerpo"
                });

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