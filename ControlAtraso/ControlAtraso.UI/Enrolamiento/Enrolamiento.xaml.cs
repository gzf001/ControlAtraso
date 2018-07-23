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

        private static ControlAtraso.Entity.Alumno Alumno
        {
            get;
            set;
        }

        delegate void DelegadoEstado(string message, string estado, Bitmap src);

        public Enrolamiento(ControlAtraso.Entity.Alumno alumno)
        {
            InitializeComponent();

            this.Run.Content = alumno.Persona.Run;

            this.Nombre.Content = alumno.Persona.Nombre;

            this.Curso.Content = alumno.Matricula.Curso;

            ControlAtraso.UI.Enrolamiento.Enrolamiento.Alumno = alumno;

            if (alumno.Persona.Enrolado)
            {
                this.Message.Content = "El alumno se encuentra enrolado";

                this.HuellaPicture.Source = new BitmapImage(new Uri("/Assets/check.png", UriKind.Relative));
            }

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

            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();

            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;

            DPFP.FeatureSet feature = new DPFP.FeatureSet();

            Extractor.CreateFeatureSet(sample, DPFP.Processing.DataPurpose.Enrollment, ref feedback, ref feature);

            if (feedback == DPFP.Capture.CaptureFeedback.Good)
            {
                enroller.AddFeatures(feature);

                switch (enroller.TemplateStatus)
                {
                    case DPFP.Processing.Enrollment.Status.Failed:
                        {
                            this.Dispatcher.Invoke(delegado, "La captura de la huella no fue correcta.\nPor favor reintente", "error", bitmap);

                            break;
                        }
                    case DPFP.Processing.Enrollment.Status.Insufficient:
                        {
                            this.Dispatcher.Invoke(delegado, "La captura de la huella fue correcta.\nPor favor repita la lectura", "acierto", bitmap);

                            break;
                        }
                    case DPFP.Processing.Enrollment.Status.Ready:
                        {
                            this.Dispatcher.Invoke(delegado, "La captura de la huella fue correcta.\nPor favor grabe la lectura realizada", "valido", bitmap);

                            break;
                        }
                    default:
                        {
                            this.Dispatcher.Invoke(delegado, "La captura de la huella no fue correcta.\nPor favor reintente", "error", bitmap);

                            break;
                        }
                }
            }
            else
            {
                this.Dispatcher.Invoke(delegado, "La captura de la huella no fue correcta.\n Por favor reintente", bitmap);
            }
        }

        void DPFP.Capture.EventHandler.OnFingerGone(object Capture, string readerSerialNumber)
        {
        }

        void DPFP.Capture.EventHandler.OnFingerTouch(object Capture, string readerSerialNumber)
        {
            this.Dispatcher.Invoke(new DelegadoEstado(this.Estado), string.Empty, string.Empty, null);
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

        private void Estado(string message, string estado, Bitmap src)
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

            switch (estado)
            {
                case "acierto":
                    {
                        this.Message.Foreground = new System.Windows.Media.SolidColorBrush(new System.Windows.Media.Color
                        {
                            A = 255,
                            R = 66,
                            G = 85,
                            B = 103
                        });

                        break;
                    }
                case "error":
                    {
                        this.Message.Foreground = System.Windows.Media.Brushes.Red;

                        break;
                    }
                case "valido":
                    {
                        ControlAtraso.Entity.Persona persona = new ControlAtraso.Entity.Persona
                        {
                            Id = ControlAtraso.UI.Enrolamiento.Enrolamiento.Alumno.Persona.Id,
                            Huella = enroller.Template.Bytes,
                            ImagenHuella = ms.GetBuffer()
                        };

                        ControlAtraso.Alumno.Enrolar(persona);

                        if (capturer != null)
                        {
                            capturer.StopCapture();
                        }

                        MessageBox.Show("El alumno fue enrolado", "Insignia", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                        NavigationService.Navigate(new ControlAtraso.UI.Enrolamiento.HomeEnrolamiento());

                        break;
                    }
            }

            this.Message.Content = message;
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