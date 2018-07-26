﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ControlAtraso
{
    public class Grado
    {
        public static List<ControlAtraso.Entity.Grado> GetAll(ControlAtraso.Entity.TipoEducacion tipoEducacion)
        {
            if (tipoEducacion.Codigo < 0)
            {
                return new List<ControlAtraso.Entity.Grado>();
            }

            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Environment.GetCommandLineArgs()[0]);

            string url = configuration.AppSettings.Settings["targetUrl"].Value;

            string rbdCuerpo = configuration.AppSettings.Settings["rbd"].Value; ;

            url = string.Format("{0}/Grados?anioNumero={1}&tipoEducacionCodigo={2}&rbdCuerpo={3}&rbdDigito={4}", url, DateTime.Today.Year, (tipoEducacion == null ? "-1" : tipoEducacion.Codigo.ToString()), rbdCuerpo.Substring(0, rbdCuerpo.Length - 1), rbdCuerpo.Substring(rbdCuerpo.Length - 1, 1));

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

            List<ControlAtraso.Entity.Grado> grados = javaScriptSerializer.Deserialize<List<ControlAtraso.Entity.Grado>>(resp);

            return grados;
        }
    }
}