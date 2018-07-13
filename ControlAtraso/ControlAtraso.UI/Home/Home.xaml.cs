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

namespace ControlAtraso.UI.Home
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.CommandParameter.Equals("enrolamiento"))
            {
                NavigationService.Navigate(new ControlAtraso.UI.Enrolamiento.Enrolamiento());
            }
            else
            {
                NavigationService.Navigate(new ControlAtraso.UI.Registro.Registro());
            }
        }
    }
}
