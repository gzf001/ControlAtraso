﻿using System;
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
using DPFP;
using DPFP.Capture;

namespace ControlAtraso.UI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, DPFP.Capture.EventHandler
    {
        public static DPFP.Capture.Capture Capturer
        {
            get;
            set;
        }

        delegate void DelegadoEstado(bool conected);

        public static ControlAtraso.UI.MainWindow Main
        {
            get;
            set;
        }

        public MainWindow()
        {
            InitializeComponent();

            ControlAtraso.UI.MainWindow.Main = this;

            this.Frame.Navigate(new ControlAtraso.UI.Configuracion.Configuracion());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Capturer = new DPFP.Capture.Capture();

            if (Capturer != null)
            {
                Capturer.EventHandler = this;

                Capturer.StartCapture();
            }
        }

        public void OnComplete(object capture, string readerSerialNumber, Sample sample)
        {
        }

        public void OnFingerGone(object capture, string readerSerialNumber)
        {
        }

        public void OnFingerTouch(object capture, string readerSerialNumber)
        {
        }

        public void OnReaderConnect(object capture, string readerSerialNumber)
        {
            DelegadoEstado delegado = new DelegadoEstado(this.Estado);

            this.Dispatcher.Invoke(delegado, true);
        }

        public void OnReaderDisconnect(object capture, string readerSerialNumber)
        {
            DelegadoEstado delegado = new DelegadoEstado(this.Estado);

            this.Dispatcher.Invoke(delegado, false);
        }

        public void OnSampleQuality(object capture, string readerSerialNumber, CaptureFeedback captureFeedback)
        {
        }

        private void Estado(bool conected)
        {
            if (conected)
            {
                this.Status.Fill = new System.Windows.Media.SolidColorBrush(new System.Windows.Media.Color
                {
                    A = 255,
                    R = 0,
                    G = 255,
                    B = 0
                });

                this.StatusText.Text = "Lector conectado";
            }
            else
            {
                this.Status.Fill = new System.Windows.Media.SolidColorBrush(new System.Windows.Media.Color
                {
                    A = 255,
                    R = 255,
                    G = 0,
                    B = 0
                });

                this.StatusText.Text = "Lector desconectado";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (Capturer != null)
            {
                Capturer.EventHandler = this;

                Capturer.StopCapture();
            }
        }
    }
}