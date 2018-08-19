using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso
{
    public class Parameters
    {
        public string Url
        {
            get;
            set;
        }

        public string Rbd
        {
            get;
            set;
        }

        public Parameters()
        {
            string startupPath = System.Environment.GetCommandLineArgs()[0];

            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(startupPath);

            this.Url = configuration.AppSettings.Settings["targetUrl"].Value;

            this.Rbd = configuration.AppSettings.Settings["rbd"].Value;
        }
    }
}