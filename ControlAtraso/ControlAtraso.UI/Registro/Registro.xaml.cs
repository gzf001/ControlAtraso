using DPFP;
using DPFP.Capture;
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

namespace ControlAtraso.UI.Registro
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Page, DPFP.Capture.EventHandler
    {
        private DPFP.Capture.Capture capturer;

        delegate void DelegadoEstado(ControlAtraso.Entity.Alumno alumno);

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
                }
            };

            dispathcer.Start();

            capturer = new DPFP.Capture.Capture();

            if (capturer != null)
            {
                capturer.EventHandler = this;

                capturer.StartCapture();
            }
        }

        void DPFP.Capture.EventHandler.OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
            DelegadoEstado delegado = new DelegadoEstado(this.Estado);

            ControlAtraso.Entity.Alumno alumno = ControlAtraso.Alumno.Registrar(sample);

            this.Dispatcher.Invoke(delegado, alumno);
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

        void DPFP.Capture.EventHandler.OnFingerGone(object Capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnFingerTouch(object Capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnReaderConnect(object Capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnReaderDisconnect(object Capture, string readerSerialNumber)
        {
            MessageBox.Show("El lector se encuentra desconectado", "Insignia", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }

        void DPFP.Capture.EventHandler.OnSampleQuality(object Capture, string readerSerialNumber, CaptureFeedback captureFeedback)
        {
        }

        private void Estado(ControlAtraso.Entity.Alumno alumno)
        {
            if (alumno.Estado.Equals("Válido"))
            {
                this.Message.Content = string.Format("{0}\n{1}", alumno.Persona.Nombre, alumno.Matricula.Curso);
            }
            else
            {
                this.Message.Content = "Se ha generado un error con el registro del atraso, por favor contacte a soporte NetCore";
            }
        }
    }
}