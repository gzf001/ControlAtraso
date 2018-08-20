using System;
using System.Deployment.Application;
using System.Reflection;
using System.Windows;

namespace ControlAtraso.UI
{
    /// <summary>
    /// Lógica de interacción para About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            Version ver = Environment.Version;

            this.TecnicalData.Text = string.Format("Versión del Framework: {0}\n\n", ver);

            try
            {
                Version applicationDeployment = ApplicationDeployment.CurrentDeployment.CurrentVersion;

                this.TecnicalData.Text += string.Format("Versión de la aplicación: {0}\n", 1);
            }
            catch
            {

            }
        }
    }
}
