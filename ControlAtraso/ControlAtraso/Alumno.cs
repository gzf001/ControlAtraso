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
        public static ControlAtraso.Result<string> Enrolar(ControlAtraso.Entity.Persona persona)
        {
            string startupPath = System.Environment.GetCommandLineArgs()[0];

            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            string url = configuration.AppSettings.Settings["targetUrl"].Value;

            url = string.Format("{0}/Enrolar", url);

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<string> result = h.Call<string>(CallType.CallTypePost, url, persona);

            return result;
        }

        public static ControlAtraso.Entity.Alumno Registrar(Sample sample)
        {
            string output = string.Empty;

            Random random = new Random();

            int indice = random.Next(0, 4);

            string startupPath = System.Environment.GetCommandLineArgs()[0];

            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            string[] claves = configuration.AppSettings.Settings["PalabrasClave"].Value.Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            string url = configuration.AppSettings.Settings["targetUrl"].Value;

            url = string.Format("{0}/Registrar", url);

            string rbdCuerpo = configuration.AppSettings.Settings["rbd"].Value;

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

        public static Result<ControlAtraso.Entity.Alumno> Get(int runCuero, char runDigito)
        {
            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = string.Format("{0}/Alumno?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&runCuerpo={4}&runDigito={5}", parameters.Url, DateTime.Today.Year, parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1), runCuero, runDigito);

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<ControlAtraso.Entity.Alumno> result = h.Call<ControlAtraso.Entity.Alumno>(CallType.CallTypeGet, url);

            return result;
        }

        public static Result<List<ControlAtraso.Entity.Alumno>> GetAll(ControlAtraso.Entity.Curso curso)
        {
            if (curso == null || curso.Id.Equals(default(Guid)))
            {
                return null;
            }

            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = string.Format("{0}/Alumnos?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&cursoId={4}", parameters.Url, DateTime.Today.Year, parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1), (curso == null ? default(Guid) : curso.Id));

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<List<ControlAtraso.Entity.Alumno>> result = h.Call<List<ControlAtraso.Entity.Alumno>>(CallType.CallTypeGet, url);

            if (result.Entity.Count > 1)
            {
                result.Entity = result.Entity.OrderBy(x => x.Persona.Nombre).ToList<ControlAtraso.Entity.Alumno>();
            }

            return result;
        }

        public static Result<List<ControlAtraso.Entity.Alumno>> GetAll(string nombre)
        {
            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = url = string.Format("{0}/AlumnosFindName?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&nombre={4}", parameters.Url, DateTime.Today.Year, parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1), nombre);

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<List<ControlAtraso.Entity.Alumno>> result = h.Call<List<ControlAtraso.Entity.Alumno>>(CallType.CallTypeGet, url);

            if (result.Entity.Count > 1)
            {
                result.Entity = result.Entity.OrderBy(x => x.Persona.Nombre).ToList<ControlAtraso.Entity.Alumno>();
            }

            return result;
        }
    }
}