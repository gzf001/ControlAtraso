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

            string url = configuration.AppSettings.Settings["Url"].Value;

            url = string.Format("{0}/Establecimiento?rbdCuerpo={1}&rbdDigito={2}", url, rbdCuerpo, rbdDigito);

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<ControlAtraso.Entity.Establecimiento> result = h.Call<ControlAtraso.Entity.Establecimiento>(CallType.CallTypeGet, url);

            if (result.Status.Equals(ControlAtraso.Status.Ok))
            {
                if (result.Entity.Estado.Equals("Válido"))
                {
                    configuration.AppSettings.Settings.Add("establecimiento", result.Entity.Nombre);
                    configuration.AppSettings.Settings.Add("rbd", string.Format("{0}{1}", rbdCuerpo, rbdDigito));
                    configuration.AppSettings.Settings.Add("targetUrl", result.Entity.Url);

                    configuration.Save();
                }

                return result.Entity;
            }
            else
            {
                ControlAtraso.Entity.Establecimiento establecimiento = new ControlAtraso.Entity.Establecimiento
                {
                    Estado = "No Válido",
                    Mensaje = "Se ha producido un error de comunicación con el servidor Insignia, por favor contacte a soporte@netcore.cl"
                };

                return establecimiento;
            }
        }
    }
}