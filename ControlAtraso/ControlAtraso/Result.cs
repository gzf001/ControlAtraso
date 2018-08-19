using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlAtraso
{
    public class Result<T>
    {
        public Status Status
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public T Entity
        {
            get;
            set;
        }
    }
}