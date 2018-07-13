using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}