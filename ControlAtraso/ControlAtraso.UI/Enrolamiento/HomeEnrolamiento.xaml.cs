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

namespace ControlAtraso.UI.Enrolamiento
{
    /// <summary>
    /// Lógica de interacción para HomeEnrolamiento.xaml
    /// </summary>
    public partial class HomeEnrolamiento : Page
    {
        public HomeEnrolamiento()
        {
            InitializeComponent();
        }

        private void Run_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            long ascci = Convert.ToInt64(Convert.ToChar(e.Text));

            if (ascci >= 48 && ascci <= 57 && this.Run.Text.Length <= 8)
            {
                int run = int.Parse(string.Format("{0}{1}", this.Run.Text, e.Text));

                char digito = Helper.GetDigito(run);

                this.LabelDigito.Content = digito;

                (this.Grid.DataContext as ControlAtraso.UI.MVVM.ViewModel.HomeEnrolamiento).RunDigito = digito;

                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.DataGrid.Items.Count > 0)
            {
                ControlAtraso.Entity.Alumno alumno = this.DataGrid.SelectedItems[0] as ControlAtraso.Entity.Alumno;

                if (!alumno.Estado.Equals("No válido"))
                {
                    ControlAtraso.UI.Enrolamiento.Enrolamiento.HomeEnrolamiento = this;

                    ControlAtraso.UI.Enrolamiento.Enrolamiento enrolamiento = new ControlAtraso.UI.Enrolamiento.Enrolamiento(this.DataGrid.SelectedItems[0] as ControlAtraso.Entity.Alumno);

                    NavigationService.Navigate(enrolamiento);
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ControlAtraso.UI.Home.Home home = new ControlAtraso.UI.Home.Home();

            NavigationService.Navigate(home);
        }
    }
}