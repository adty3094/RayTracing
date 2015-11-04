using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
            Vector direction = temp / temp.Distance();
            return new Ray(position, direction);
        }

        public static Ray Intersection(Ray ray, Scene scene)
        {
            double mindist = double.MaxValue;
            foreach(Object obj in scene.ObjectList)
            {
                if (obj.IsIntersect(ref ray) && ray.IntersectDistance < mindist)
                {
                    mindist = ray.IntersectDistance;
                    ray.IntersectWith = obj;
                }
            }
            ray.IntersectDistance = mindist;
            return ray;
        }

        public static Bitmap RayTrace(Scene scene)
        {
            ViewPlane viewPlane = new ViewPlane(Screen.width, Screen.height, scene.Camera);
            Bitmap bmp = new Bitmap(viewPlane.PixelWidth, viewPlane.PixelHeight);
            for(int i = 0; i < viewPlane.PixelHeight ; i++)
            {
                for (int j = 0; j < viewPlane.PixelWidth ; j++)
                {
                    Ray ray = new Ray(); //RayThruPixels(scene.Camera, i, j);
                    ray.Position = scene.Camera.GetLookFrom();
                    ray.Direction = (ray.Position - viewPlane.GetNewLocation(i, j));
                    ray.Direction /= ray.Direction.Distance();
                    Intersection(ray, scene);
                    //ray.GetInformation();
                    if (ray.IntersectWith != null)
                    {
                        bmp.SetPixel(j, i, ray.IntersectWith.Color);
                    }
                    else
                        bmp.SetPixel(j, i, Color.Black);
                }
            }
            return bmp;
        }
    }
}   
