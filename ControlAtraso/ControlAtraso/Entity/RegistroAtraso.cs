using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso.Entity
{
    public class RegistroAtraso
    {
        public int RbdCuerpo
        {
            get;
            set;
        }

        public int RbdDigito
        {
            get;
            set;
        }

        public Guid SostenedorId
        {
            get;
            set;
        }

        public Guid EstablecimientoId
        {
            get;
            set;
        }

        public Int32 AnoNumero
        {
            get;
            set;
        }

        public Int32 TipoEducacionCodigo
        {
            get;
            set;
        }

        public Int32 GradoCodigo
        {
            get;
            set;
        }

        public Guid CursoId
        {
            get;
            set;
        }

        public Guid LibroClaseId
        {
            get;
            set;
        }

        public Guid AlumnoId
        {
            get;
            set;
        }

        public DateTime Fecha
        {
            get;
            set;
        }
        
        public DateTime FechaHora
        {
            get;
            set;
        }

        public System.Data.Linq.Binary Huella
        {
            get;
            set;
        }
    }
}