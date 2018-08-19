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
    public class Helper
    {
        public static char GetDigito(int cuerpo)
        {
            int contador = 0;
            int multiplo = 2;

            while (cuerpo != 0)
            {
                contador += (cuerpo % 10) * multiplo;

                cuerpo /= 10;

                multiplo++;

                if (multiplo == 8)
                {
                    multiplo = 2;
                }
            }

            contador = 11 - (contador % 11);

            if (contador == 10)
            {
                return 'K';
            }
            else if (contador == 11)
            {
                return '0';
            }
            else
            {
                return char.Parse(contador.ToString());
            }
        }

        public static bool AccesoInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.insignia.cl");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public ControlAtraso.Result<T> Call<T>(ControlAtraso.CallType callType, string url, object obj = null)
        {
            if (!ControlAtraso.Helper.AccesoInternet())
            {
                ControlAtraso.Result<T> result = new Result<T>
                {
                    Status = Status.Error,
                    Message = "Por favor compruebe su conexión a Internet\n(no se puede establer conexión con el servidor Insignia)"
                };

                return result;
            }

            string output = string.Empty;

            Random random = new Random();

            int indice = random.Next(0, 4);

            string startupPath = System.Environment.GetCommandLineArgs()[0];

            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            string[] claves = configuration.AppSettings.Settings["PalabrasClave"].Value.ToString().Split(',');

            string token = claves[indice];

            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(token);

            token = Convert.ToBase64String(encryted);

            try
            {
                bool get = ControlAtraso.CallType.CallTypeGet.Equals(callType);

                if (obj != null)
                {
                    output = JsonConvert.SerializeObject(obj, Formatting.Indented);
                }

                byte[] data = UTF8Encoding.UTF8.GetBytes(output);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Timeout = 10 * 1000;
                request.Method = get ? "GET" : "POST";

                if (!get)
                {
                    request.ContentLength = data.Length;
                }

                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("token", token);

                if (!get)
                {
                    Stream postStream = request.GetRequestStream();

                    postStream.Write(data, 0, data.Length);

                    postStream.Close();
                }

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string resp = reader.ReadToEnd();

                // Cerramos los streams
                reader.Close();

                response.Close();

                ControlAtraso.Result<T> result;

                if (get)
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

                    result = new Result<T>
                    {
                        Status = Status.Ok,
                        Message = string.Empty,
                        Entity = javaScriptSerializer.Deserialize<T>(resp)
                    };
                }
                else
                {
                    result = new Result<T>
                    {
                        Status = Status.Ok,
                        Message = string.Empty
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                ControlAtraso.Result<T> result = new Result<T>
                {
                    Status = Status.Error,
                    Message = ex.Message
                };

                return result;
            }
        }
    }
}