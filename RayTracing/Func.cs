using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Func
    {
        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static Ray RayThruPixels(Camera cam, int i, int j)
        {
            double alpha = Math.Tan(DegreeToRadian(cam.GetFOVX()) / 2) * (j - (Screen.width / 2)) / (Screen.width / 2);
            double beta = Math.Tan(DegreeToRadian(cam.GetFOVY()) / 2) * ((Screen.height / 2) - i) / (Screen.height / 2);
            Vector position = cam.GetLookFrom();
            Vector temp = (cam.GetVectorU() * alpha + cam.GetVectorV() * beta - cam.GetVectorW() );
            Vector direction = temp / temp.Normalize();
            return new Ray(position, direction);
        }
    }
}
