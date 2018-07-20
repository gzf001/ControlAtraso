using System;
using System.Collections.Generic;
using System.Drawing;
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
using DPFP;
using DPFP.Capture;

namespace ControlAtraso.UI.Enrolamiento
{
    /// <summary>
    /// Lógica de interacción para Enrolamiento.xaml
    /// </summary>
    public partial class Enrolamiento : Page, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture capturer;

        private DPFP.Processing.Enrollment enroller;

        delegate void DelegadoEstado(string message, string text, System.Windows.MessageBoxButton messageBoxButtons, System.Windows.MessageBoxImage messageBoxImage, Bitmap image);

        public Enrolamiento(ControlAtraso.Entity.Alumno alumno)
        {
            InitializeComponent();

            this.Run.Content = alumno.Persona.Run;

            this.Nombre.Content = alumno.Persona.Nombre;

            this.Curso.Content = alumno.Matricula.Curso;

            capturer = new DPFP.Capture.Capture();

            if (capturer != null)
            {
                capturer.EventHandler = this;

                capturer.StartCapture();

                enroller = new DPFP.Processing.Enrollment();
            }
        }

        void DPFP.Capture.EventHandler.OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
            DelegadoEstado delegado = new DelegadoEstado(this.Estado);

            DPFP.Capture.SampleConversion convertor = new DPFP.Capture.SampleConversion();

            Bitmap bitmap = null;

            convertor.ConvertToPicture(sample, ref bitmap);

            this.Dispatcher.Invoke(delegado,"La huella fue leida de forma correcta", "Insignia", MessageBoxButton.OK, MessageBoxImage.Information, bitmap);
        }

        void DPFP.Capture.EventHandler.OnFingerGone(object Capture, string ReaderSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnFingerTouch(object Capture, string ReaderSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnReaderConnect(object Capture, string ReaderSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnSampleQuality(object Capture, string ReaderSerialNumber, CaptureFeedback CaptureFeedback)
        {
        }

        private void Estado(string message, string text, System.Windows.MessageBoxButton messageBoxButtons, System.Windows.MessageBoxImage messageBoxImage, Bitmap src)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            (src as System.Drawing.Bitmap).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();

            image.BeginInit();

            ms.Seek(0, System.IO.SeekOrigin.Begin);

            image.StreamSource = ms;

            image.EndInit();

            HuellaPicture.Source = image;

            MessageBox.Show(message, text, messageBoxButtons, messageBoxImage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (capturer != null)
            {
                capturer.StopCapture();
            }

            NavigationService.Navigate(new ControlAtraso.UI.Enrolamiento.HomeEnrolamiento());
        }
    }
}