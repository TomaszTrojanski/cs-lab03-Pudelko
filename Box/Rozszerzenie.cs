using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public static class MyExtenshionMethods
    {
        public static Pudelko Kompresuj(this Pudelko p)
        {
            double a = Math.Cbrt(p.Objetosc);
            return new Pudelko(a, a, a);
        }
    }
}