using DPFP;
using DPFP.Capture;
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

namespace ControlAtraso.UI.Registro
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Page, DPFP.Capture.EventHandler
    {
        delegate void DelegadoEstado(ControlAtraso.Entity.Alumno alumno, Bitmap src);

        public Registro()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer dispathcer = new System.Windows.Threading.DispatcherTimer();

            dispathcer.Interval = new TimeSpan(0, 0, 1);

            dispathcer.Tick += (x, y) =>
            {
                this.Now.Content = string.Format("{0} {1}", DateTime.Today.ToString("D", new System.Globalization.CultureInfo("es-ES")), DateTime.Now.ToLongTimeString());

                if ((DateTime.Now.Second % 5) == 0)
                {
                    this.Message.Content = string.Empty;

                    this.HuellaPicture.Source = null;
                }
            };

            dispathcer.Start();

            if (ControlAtraso.UI.MainWindow.Capturer != null)
            {
                ControlAtraso.UI.MainWindow.Capturer.EventHandler = this;

                ControlAtraso.UI.MainWindow.Capturer.StartCapture();
            }
        }

        void DPFP.Capture.EventHandler.OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
            DelegadoEstado delegado = new DelegadoEstado(this.Estado);

            DPFP.Capture.SampleConversion convertor = new DPFP.Capture.SampleConversion();

            Bitmap bitmap = null;

            convertor.ConvertToPicture(sample, ref bitmap);

            ControlAtraso.Entity.Alumno alumno = ControlAtraso.Alumno.Registrar(sample);

            this.Dispatcher.Invoke(delegado, alumno, bitmap);
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample sample, DPFP.Processing.DataPurpose purpose)
        {
            DPFP.Processing.FeatureExtraction extractor = new DPFP.Processing.FeatureExtraction();

            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;

            DPFP.FeatureSet features = new DPFP.FeatureSet();

            extractor.CreateFeatureSet(sample, purpose, ref feedback, ref features);

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                return features;
            }
            else
            {
                return null;
            }
        }

        void DPFP.Capture.EventHandler.OnFingerGone(object capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnFingerTouch(object capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnReaderConnect(object capture, string readerSerialNumber)
        {
            ControlAtraso.UI.MainWindow.Main.OnReaderConnect(capture, readerSerialNumber);
        }

        void DPFP.Capture.EventHandler.OnReaderDisconnect(object capture, string readerSerialNumber)
        {
            ControlAtraso.UI.MainWindow.Main.OnReaderDisconnect(capture, readerSerialNumber);
        }

        void DPFP.Capture.EventHandler.OnSampleQuality(object capture, string readerSerialNumber, CaptureFeedback captureFeedback)
        {
        }

        private void Estado(ControlAtraso.Entity.Alumno alumno, Bitmap src)
        {
            System.IO.MemoryStream ms = null;

            if (src != null)
            {
                ms = new System.IO.MemoryStream();

                src.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                BitmapImage image = new BitmapImage();

                image.BeginInit();

                ms.Seek(0, System.IO.SeekOrigin.Begin);

                image.StreamSource = ms;

                image.EndInit();

                HuellaPicture.Source = image;
            }

            if (alumno.Estado.Equals("Válido"))
            {
                this.Message.Content = string.Format("{0}\n{1}", alumno.Persona.Nombre, alumno.Matricula.Curso);
            }
            else
            {
                this.Message.Content = "Se ha generado un error con el registro del atraso, por favor contacte a soporte NetCore";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (ControlAtraso.UI.MainWindow.Capturer != null)
            {
                ControlAtraso.UI.MainWindow.Capturer.EventHandler = ControlAtraso.UI.MainWindow.Main;
            }

            ControlAtraso.UI.Home.Home home = new ControlAtraso.UI.Home.Home();

            NavigationService.Navigate(home);
        }
    }
}