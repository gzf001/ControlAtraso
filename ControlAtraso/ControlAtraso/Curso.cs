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
    public class Curso
    {
        public static Result<List<ControlAtraso.Entity.Curso>> GetAll(ControlAtraso.Entity.Grado grado)
        {
            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = string.Format("{0}/Cursos?anioNumero={1}&rbdCuerpo={2}&rbdDigito={3}&tipoEducacionCodigo={4}&gradoCodigo={5}", parameters.Url, DateTime.Today.Year, parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1), (grado == null ? "-1" : grado.TipoEducacionCodigo.ToString()), (grado == null ? "-1" : grado.Codigo.ToString()));

            ControlAtraso.Helper h = new Helper();

            ControlAtraso.Result<List<ControlAtraso.Entity.Curso>> result = h.Call<List<ControlAtraso.Entity.Curso>>(CallType.CallTypeGet, url);

            return result;
        }
    }
}