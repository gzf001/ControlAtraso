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
using System.Windows.Shapes;

namespace ControlAtraso.UI
{
    /// <summary>
    /// Lógica de interacción para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int contador = 0;

            ControlAtraso.UI.MainWindow mainWindow = new ControlAtraso.UI.MainWindow();

            System.Windows.Threading.DispatcherTimer dispathcer = new System.Windows.Threading.DispatcherTimer();

            dispathcer.Interval = new TimeSpan(0, 0, 1);

            dispathcer.Tick += (x, y) =>
            {
                if (contador.Equals(1))
                {
                    this.Close();

                    mainWindow.Show();
                }

                contador++;
            };

            dispathcer.Start();
        }
    }
}