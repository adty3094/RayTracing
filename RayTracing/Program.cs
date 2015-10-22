using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera c = new Camera(0, 0, 4,
                                  0, 0, 0,
                                  0, 1, 0,
                                  0);
            c.GetVectorU().PrintVector();
            c.GetVectorV().PrintVector();
            c.GetVectorW().PrintVector();
            Console.ReadLine();
        }
    }
}
