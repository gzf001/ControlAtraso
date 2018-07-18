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

                (this.Grid.DataContext as ControlAtraso.UI.MVVM.ViewModel.Enrolamiento).RunDigito = digito;

                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ControlAtraso.UI.Enrolamiento.Enrolamiento());
        }
    }
}