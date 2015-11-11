using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    public class Sphere : Object
    {
        private Vector center;
        private double radius;        

        public Sphere(Vector center, double radius,Matrices transform, Color color)
            : base(transform,color)
        {
            this.center = center;
            this.radius = radius;
        }

        public override bool IsIntersect(Ray ray, Matrices transform)
        {
           
            //if (a == 0)
            //{
            //    ray.IntersectDistance = -c / b;
            //    return true;
            //}
            //else if (D < 0)
            //    return false;
            //else
            //{

            Vector L = ray.Position - this.center;
            double a = Vector.DotProduct(ray.Direction, ray.Direction);
            double b = 2 * Vector.DotProduct(ray.Direction, L);
            double c = Vector.DotProduct(L, L) - this.radius * this.radius;
            
            double D = b * b - 4 * a * c;
            double q = (b > 0) ?
                -0.5 * (b + Math.Sqrt(D)) : -0.5 * (b - Math.Sqrt(D));
            double[] t = new double[2];
            t[0] = q / a;
            t[1] = c/q;
            //if (t[0] > 0 && t[1] > 0)
            //{
            Vector temp1 = ray.Direction * Math.Min(t[0], t[1]);
            Matrices temp2 = new Matrices(temp1.x, temp1.y, temp1.z, 1);
            temp2 = transform.Matrix * temp2;
            temp1.x = temp2.Matrix[0, 0];
            temp1.y = temp2.Matrix[1, 0];
            temp1.z = temp2.Matrix[2, 0];
            ray.IntersectDistance = temp1.Distance();
            //ray.IntersectDistance = Math.Min(t[0], t[1]);
            return true;
                //}
                //else if (t[0] < 0 && t[1] > 0)
                //{
                //    ray.IntersectDistance = t[1];
                //    return true;
                //}
                //else if (t[0] > 0 && t[1] < 0)
                //{
                //    ray.IntersectDistance = t[0];
                //    return true;
                //}
                //else return false;
            //}
            
        }

        public override void GetObjectType()
        {
            Console.WriteLine("This is Sphere");
        }
    }
}
