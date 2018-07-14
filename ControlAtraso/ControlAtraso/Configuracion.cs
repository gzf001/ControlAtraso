using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso
{
    public class Configuracion
    {
        public static bool Configurar(string startupPath, int rbdCuerpo, char rbdDigito)
        {
            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            bool configurado = configuration.AppSettings.Settings.AllKeys.Any(x => x.Equals("rbd"));

            if (configurado)
            {
                return true;
            }

            configuration.AppSettings.Settings.Add("rbd", string.Format("{0}{1}", rbdCuerpo, rbdDigito));

            configuration.Save();

            return true;
        }
    }
}