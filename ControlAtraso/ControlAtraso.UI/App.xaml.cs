using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using DPFP;
using DPFP.Capture;

namespace ControlAtraso.UI
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;

            string body;

            body = "<html>";
            body += "<head>";
            body += "<style>";
            body += "body {font-family:\"Verdana\";font-weight:normal;font-size: .7em;color:black;} ";
            body += "p {font-family:\"Verdana\";font-weight:normal;color:black;margin-top: -5px}";
            body += "b {font-family:\"Verdana\";font-weight:bold;color:black;margin-top: -5px}";
            body += "H1 { font-family:\"Verdana\";font-weight:normal;font-size:18pt;color:red }";
            body += "H2 { font-family:\"Verdana\";font-weight:normal;font-size:14pt;color:maroon }";
            body += "pre {font-family:\"Lucida Console\";font-size: .9em}";
            body += ".marker {font-weight: bold; color: black;text-decoration: none;}";
            body += ".version {color: gray;}";
            body += ".error {margin-bottom: 10px;}";
            body += ".expandable { text-decoration:underline; font-weight:bold; color:navy; cursor:hand; }";
            body += "</style>";
            body += "</head>";
            body += "<body bgcolor=\"white\">";
            body += "<span><H1>Error de servidor en la aplicación Control de atrasos.<hr width=100% size=1 color=silver></H1>";
            body += "<h2><i>" + ex.Message + ".</i> </h2></span>";
            body += "<font face=\"Arial, Helvetica, Geneva, SunSans-Regular, sans-serif \">";
            body += "<b> Descripción: </b>Excepción no controlada al ejecutar la solicitud Web actual. Revise el seguimiento de la pila para obtener más información acerca del error y dónde se originó en el código.";
            body += "<br><br>";
            body += "<b>Detalles de la excepción: </b>" + ex.ToString().Replace("\r\n", "<br>") + ".<br><br>";

            if (ex.InnerException != null)
            {
                body += "<b>Error de código fuente:</b> <br><br>";
                body += "<table width=100% bgcolor=\"#ffffcc\">";
                body += "<tr>";
                body += "<td>";
                body += "<code><pre>";
                body += ex.InnerException.ToString();
                body += "</pre>";
                body += "</code>";
                body += "</td>";
                body += "</tr>";
                body += "</table>";
            }

            body += "<br>";
            //body += @"<b> Archivo de origen: </b> " + ex. this.Request.ServerVariables["PATH_TRANSLATED"] + "";
            body += "<br><br>";
            body += "<b>Seguimiento de la pila:</b> <br><br>";
            body += "<table width=100% bgcolor=\"#ffffcc\">";
            body += "<tr>";
            body += "<td>";
            body += "<code><pre>";
            body += ex.StackTrace;
            body += "</pre></code>";
            body += "</td>";
            body += "</tr>";
            body += "</table>";
            body += "<br>";
            body += "<hr width=100% size=1 color=silver>";
            body += "<b>información de versión:</b>&nbsp;Versión de Microsoft .NET Framework:2.0.50727.1378; Versión ASP.NET:2.0.50727.1378";
            body += "</font>";
            body += "</body>";
            body += "</html>";

            /*---------------------------------------------------------------------------------------------------------------------*/

            List<MailAddress> destinatarios = new List<MailAddress>();

            destinatarios.Add(new MailAddress("gzuleta@netcore.cl", "Guillermo Zuleta Flores"));

            foreach (MailAddress destinatario in destinatarios)
            {
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.EnableSsl = true;

                MailAddress remitente = new MailAddress("applicationserver@icastellano.cl", "Sistema Insignia Control de Atrasos");

                MailMessage message = new MailMessage(remitente, destinatario);

                message.IsBodyHtml = true;

                message.Body = body;

                message.Subject = "Se ha generado una excepción en Control de Atrasos";

                NetworkCredential credencial = new NetworkCredential("applicationserver@icastellano.cl", "insignia", "");

                smtp.Credentials = credencial;

                try
                {
                    smtp.Send(message);
                }
                finally
                {
                }
            }
        }
    }
}