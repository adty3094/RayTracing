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
            Scene scene = new Scene(new Camera(0, 0, 4,
                                               0, 0, 0,
                                               0, 1, 0,
                                               45));
            scene.Camera.GetVectorU().PrintVector();
            scene.Camera.GetVectorV().PrintVector();
            scene.Camera.GetVectorW().PrintVector();
            Console.WriteLine(scene.Camera.GetFOVX());
            Console.WriteLine(scene.Camera.GetFOVY());
            Console.ReadLine();
        }
    }
}
