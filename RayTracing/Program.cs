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
            Screen.width = 360;
            Screen.height = 360;
            Camera c = new Camera(0, 0, 4,
                                  0, 0, 0,
                                  0, 1, 0,
                                  45);
            c.GetVectorU().PrintVector();
            c.GetVectorV().PrintVector();
            c.GetVectorW().PrintVector();
            Console.WriteLine(c.GetFOVX());
            Console.WriteLine(c.GetFOVY());
            Console.ReadLine();
        }
    }
}
