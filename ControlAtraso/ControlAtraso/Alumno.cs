using DPFP;
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
    public class Alumno
    {
        public static string Enrolar(ControlAtraso.Entity.Persona persona)
        {
            string output = string.Empty;

            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["targetUrl"];

            url = string.Format("{0}/Enrolar", url);

            try
            {
                output = JsonConvert.SerializeObject(persona, Formatting.Indented);

                byte[] data = UTF8Encoding.UTF8.GetBytes(output);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Timeout = 10 * 1000;
                request.Method = "POST";
                request.ContentLength = data.Length;
                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("token", token);

                Stream postStream = request.GetRequestStream();

                postStream.Write(data, 0, data.Length);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string resp = reader.ReadToEnd();

                // Cerramos los streams
                reader.Close();

                postStream.Close();

                response.Close();

                return "Ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static ControlAtraso.Entity.Alumno Registrar(Sample sample)
        {
            string output = string.Empty;

            Random random = new Random();

            int indice = random.Next(0, 4);

            string[] claves = System.Configuration.ConfigurationManager.AppSettings["PalabrasClave"].Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = System.Configuration.ConfigurationManager.AppSettings["targetUrl"];

            url = string.Format("{0}/Registrar", url);

            string rbdCuerpo = System.Configuration.ConfigurationManager.AppSettings["rbd"];

            System.Data.Linq.Binary binary = new System.Data.Linq.Binary(sample.Bytes);

            ControlAtraso.Entity.RegistroAtraso registroAtraso = new ControlAtraso.Entity.RegistroAtraso
            {
                RbdCuerpo = int.Parse(rbdCuerpo.Substring(0, rbdCuerpo.Length - 1)),
                RbdDigito = int.Parse(rbdCuerpo.Substring(rbdCuerpo.Length - 1, 1)),
                SostenedorId = default(Guid),
                EstablecimientoId = default(Guid),
                AnoNumero = DateTime.Today.Year,
                TipoEducacionCodigo = default(int),
                GradoCodigo = default(int),
                CursoId = default(Guid),
                LibroClaseId = default(Guid),
                AlumnoId = default(Guid),
                Fecha = DateTime.Today,
                FechaHora = DateTime.Now,
                Huella = binary
            };

            output = JsonConvert.SerializeObject(registroAtraso, Formatting.Indented);

            byte[] data = UTF8Encoding.UTF8.GetBytes(output);

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

            request.Timeout = 10 * 1000;
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("token", token);

            Stream postStream = request.GetRequestStream();

            postStream.Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            StreamReader reader = new StreamReader(response.GetResponseStream());

            string resp = reader.ReadToEnd();

            // Cerramos los streams
            reader.Close();

            postStream.Close();

            response.Close();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            ControlAtraso.Entity.Alumno alumno = javaScriptSerializer.Deserialize<ControlAtraso.Entity.Alumno>(resp);

            return alumno;
        }

        public static List<ControlAtraso.Entity.Alumno> GetAll(ControlAtraso.Entity.Curso curso)
        {
            if (curso == null || curso.Id.Equals(default(Guid)))
            {
                return null;
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