using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlAtraso.UI.Configuracion
{
    /// <summary>
    /// Lógica de interacción para Configuracion.xaml
    /// </summary>
    public partial class Configuracion : Page
    {
        public Configuracion()
        {
            InitializeComponent();

            string rbd = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            if (!string.IsNullOrEmpty(rbd))
            {
                NavigationService.Navigate(new ControlAtraso.UI.Home.Home());
            }
        }
    }
}
