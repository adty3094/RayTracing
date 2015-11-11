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
                //for (int i = 0; i < obj.Transform.Matrix.GetLength(0); i++)
                //{
                //    for (int j = 0; j < obj.Transform.Matrix.GetLength(1); j++)
                //        Console.Write("\t" + obj.Transform.Matrix[i, j]);
                //    Console.WriteLine();
                //}
                //
                //Console.WriteLine();
                //
                //for(int i = 0 ; i < obj.Transform.Invers.GetLength(0) ; i++)
                //{
                //    for(int j = 0 ; j < obj.Transform.Invers.GetLength(1) ; j++)
                //        Console.Write("\t" + obj.Transform.Invers[i,j]);
                //    Console.WriteLine();
                //}
                //Console.ReadKey();

                Matrices pos = new Matrices(ray.Position.x, ray.Position.y, ray.Position.z, 1);

                //Console.WriteLine(pos.Matrix[0, 0] + " " + pos.Matrix[1, 0] + " " + pos.Matrix[2, 0] + " " + pos.Matrix[3, 0]);

                pos = obj.Transform.Invers * pos;

                //Console.WriteLine(pos.Matrix[0, 0] + " " + pos.Matrix[1, 0] + " " + pos.Matrix[2, 0] + " " + pos.Matrix[3, 0]);
                //Console.ReadKey();

                Matrices dir = new Matrices(ray.Direction.x, ray.Direction.y, ray.Direction.z, 0);
                dir = obj.Transform.Invers * dir;

                //Debug--------------------------------------------------
                //Console.WriteLine(ray.Position.x + " " + ray.Position.y + " " + ray.Position.z);
                //Console.WriteLine(pos.Matrix[0, 0] + " " + pos.Matrix[1, 0] + " " + pos.Matrix[2, 0]);
                //Console.ReadKey();
                //-------------------------------------------------------

                ray.Position.x = pos.Matrix[0,0];
                ray.Position.y = pos.Matrix[1,0];
                ray.Position.z = pos.Matrix[2,0];

                ray.Direction.x = dir.Matrix[0, 0];
                ray.Direction.y = dir.Matrix[1, 0];
                ray.Direction.z = dir.Matrix[2, 0];

                if (obj.IsIntersect(ray,obj.Transform) && ray.IntersectDistance < mindist)
                {
                    mindist = ray.IntersectDistance;
                    ray.IntersectWith = obj;
                }
                // kali transform biasa ray direction(1) + position(0)
                pos = obj.Transform.Matrix * pos;
                dir = obj.Transform.Matrix * dir;

                ray.Position.x = pos.Matrix[0, 0];
                ray.Position.y = pos.Matrix[1, 0];
                ray.Position.z = pos.Matrix[2, 0];

                ray.Direction.x = dir.Matrix[0, 0];
                ray.Direction.y = dir.Matrix[1, 0];
                ray.Direction.z = dir.Matrix[2, 0];
            }
            ray.IntersectDistance = mindist;
            return ray;
        }

        public static Bitmap RayTrace(Scene scene)
        {
            Bitmap bmp = new Bitmap(Screen.width, Screen.height);
            for(int i = 0; i < Screen.height ; i++)
            {
                for (int j = 0; j < Screen.width ; j++)
                {
                    Ray ray =  RayThruPixels(scene.Camera, i, j);
                    //ray.GetInformation();
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
