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
    public class TipoEducacion
    {
        public static Result<List<ControlAtraso.Entity.TipoEducacion>> GetAll()
        {
            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = string.Format("{0}/TiposEducacion?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}", parameters.Url, DateTime.Today.Year, parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1));

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<List<ControlAtraso.Entity.TipoEducacion>> result = h.Call<List<ControlAtraso.Entity.TipoEducacion>>(CallType.CallTypeGet, url);

            return result;
        }
    }
}