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
    public class Grado
    {
        public static Result<List<ControlAtraso.Entity.Grado>> GetAll(ControlAtraso.Entity.TipoEducacion tipoEducacion)
        {
            ControlAtraso.Result<List<ControlAtraso.Entity.Grado>> result;

            if (tipoEducacion.Codigo < 0)
            {
                result = new ControlAtraso.Result<List<ControlAtraso.Entity.Grado>>
                {
                    Status = ControlAtraso.Status.Ok,
                    Entity = new List<ControlAtraso.Entity.Grado>()
                };

                return result;
            }

            ControlAtraso.Parameters parameters = new ControlAtraso.Parameters();

            string url = string.Format("{0}/Grados?anioNumero={1}&tipoEducacionCodigo={2}&rbdCuerpo={3}&rbdDigito={4}", parameters.Url, DateTime.Today.Year, (tipoEducacion == null ? "-1" : tipoEducacion.Codigo.ToString()), parameters.Rbd.Substring(0, parameters.Rbd.Length - 1), parameters.Rbd.Substring(parameters.Rbd.Length - 1, 1));

            ControlAtraso.Helper h = new Helper();

            result = h.Call<List<ControlAtraso.Entity.Grado>>(CallType.CallTypeGet, url);

            return result;
        }
    }
}