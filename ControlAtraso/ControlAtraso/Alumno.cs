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
    public class Alumno
    {
        public static List<ControlAtraso.Entity.Alumno> GetAll(ControlAtraso.Entity.Curso curso)
        {
            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["targetUrl"];

            string rbdCuerpo = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            url = string.Format("{0}/Alumnos?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&cursoId={4}", url, DateTime.Today.Year, rbdCuerpo.Substring(0, rbdCuerpo.Length - 1), rbdCuerpo.Substring(rbdCuerpo.Length - 1, 1), (curso == null ? default(Guid) : curso.Id));

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

            List<ControlAtraso.Entity.Alumno> alumnos = javaScriptSerializer.Deserialize<List<ControlAtraso.Entity.Alumno>>(resp);

            return alumnos.Count > 1 ? alumnos.OrderBy(x => x.Persona.Nombre).ToList<ControlAtraso.Entity.Alumno>() : alumnos;
        }

        public static List<ControlAtraso.Entity.Alumno> GetAll(int runCuero, char runDigito)
        {
            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["targetUrl"];

            string rbdCuerpo = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            url = string.Format("{0}/Alumno?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&runCuerpo={4}&runDigito={5}", url, DateTime.Today.Year, rbdCuerpo.Substring(0, rbdCuerpo.Length - 1), rbdCuerpo.Substring(rbdCuerpo.Length - 1, 1), runCuero, runDigito);

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

            ControlAtraso.Entity.Alumno alumno = javaScriptSerializer.Deserialize<ControlAtraso.Entity.Alumno>(resp);

            List<ControlAtraso.Entity.Alumno> alumnos = new List<ControlAtraso.Entity.Alumno>();

            alumnos.Add(alumno);

            return alumnos;
        }

        public static List<ControlAtraso.Entity.Alumno> GetAll(string nombre)
        {
            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["targetUrl"];

            string rbdCuerpo = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            url = string.Format("{0}/AlumnosFindName?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&nombre={4}", url, DateTime.Today.Year, rbdCuerpo.Substring(0, rbdCuerpo.Length - 1), rbdCuerpo.Substring(rbdCuerpo.Length - 1, 1), nombre);

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

            List<ControlAtraso.Entity.Alumno> alumnos = javaScriptSerializer.Deserialize<List<ControlAtraso.Entity.Alumno>>(resp);

            return alumnos.Count > 1 ? alumnos.OrderBy(x => x.Persona.Nombre).ToList<ControlAtraso.Entity.Alumno>() : alumnos;
        }
    }
}