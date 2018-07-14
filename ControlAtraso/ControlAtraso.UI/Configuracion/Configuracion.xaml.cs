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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string rbd = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            if (!string.IsNullOrEmpty(rbd))
            {
                NavigationService.Navigate(new ControlAtraso.UI.Home.Home());
            }
        }

        private void RbdDigito_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            long ascci = Convert.ToInt64(Convert.ToChar(e.Text));

            if ((ascci >= 48 && ascci <= 57) || e.Text.ToUpper().Equals("K"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.RbdCuerpo.Text) || string.IsNullOrEmpty(this.RbdDigito.Text))
            {
                MessageBox.Show("Por favor, ingresa el R.B.D. para continuar", "Insignia", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            else
            {
                string cuerpo = this.RbdCuerpo.Text;
                char digito = char.Parse(this.RbdDigito.Text.ToUpper());

                if (ControlAtraso.Helper.GetDigito(int.Parse(cuerpo)).Equals(digito))
                {
                    ControlAtraso.Configuracion.Configurar(System.Environment.GetCommandLineArgs()[0], int.Parse(cuerpo), char.Parse(this.RbdDigito.Text.ToUpper()));

                    NavigationService.Navigate(new ControlAtraso.UI.Home.Home());
                }
                else
                {
                    MessageBox.Show("Por favor, verifica el R.B.D. que ingresaste", "Insignia", MessageBoxButton.OK, MessageBoxImage.Stop); 
                }
            }
        }        
    }
}