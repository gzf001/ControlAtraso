using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ControlAtraso
{
    public class Configuracion
    {
        public static ControlAtraso.Entity.Establecimiento Configurar(string startupPath, int rbdCuerpo, char rbdDigito)
        {
            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            bool configurado = configuration.AppSettings.Settings.AllKeys.Any(x => x.Equals("rbd"));

            if (configurado)
            {
                return new ControlAtraso.Entity.Establecimiento();
            }

            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["Url"];

            url = string.Format("{0}/Establecimiento?rbdCuerpo={1}&rbdDigito={2}", url, rbdCuerpo, rbdDigito);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Timeout = 10 * 1000;
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("token", token);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string resp = reader.ReadToEnd();

            // Cerramos los streams
            reader.Close();

            response.Close();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            ControlAtraso.Entity.Establecimiento establecimiento = javaScriptSerializer.Deserialize<ControlAtraso.Entity.Establecimiento>(resp);

            if (establecimiento.Estado.Equals("Válido"))
            {
                configuration.AppSettings.Settings.Add("establecimiento", establecimiento.Nombre);
                configuration.AppSettings.Settings.Add("rbd", string.Format("{0}{1}", rbdCuerpo, rbdDigito));
                configuration.AppSettings.Settings.Add("targetUrl", establecimiento.Url);

                configuration.Save();
            }

            return establecimiento;
        }
    }
}