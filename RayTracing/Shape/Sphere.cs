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

        private Matrices transform;

        public Matrices Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        

        public Sphere(Vector center, double radius,Matrices transform, Color color)
            : base(transform,color)
        {
            this.center = center;
            this.radius = radius;
        }

        public override bool IsIntersect(Ray ray, Matrices transform)
        {
            double a = Vector.DotProduct(ray.GetDirection(), ray.GetDirection());
            double b = 2 * Vector.DotProduct((ray.GetPosition() - this.center), ray.GetDirection());
            double c = Vector.DotProduct((ray.GetPosition() - this.center), (ray.GetPosition() - this.center)) - radius * radius;
            double D = b*b - 4*a*c;
            if (a == 0)
            {
                ray.IntersectDistance = -c / b;
                return true;
            }
            else if (D < 0)
                return false;
            else
            {
                double[] t = new double[2];
                t[0] = -b + Math.Sqrt(D) / (2 * a);
                t[1] = -b - Math.Sqrt(D) / (2 * a);
                if (t[0] > 0 && t[1] > 0)
                {
                    Vector temp1 = ray.Direction * Math.Min(t[0], t[1]);
                    Matrices temp2 = new Matrices(temp1.x, temp1.y, temp1.z, 0);
                    temp2 = transform.Matrix * temp2;
                    temp1.x = temp2.Matrix[0, 0];
                    temp1.y = temp2.Matrix[1, 0];
                    temp1.z = temp2.Matrix[2, 0];
                    ray.IntersectDistance = temp1.Distance();
                    //ray.IntersectDistance = Math.Min(t[0], t[1]);
                    return true;
                }
                else if (t[0] < 0 && t[1] > 0)
                {
                    ray.IntersectDistance = t[1];
                    return true;
                }
                else if (t[0] > 0 && t[1] < 0)
                {
                    ray.IntersectDistance = t[0];
                    return true;
                }
                else return false;
            }
            
        }

        public override void GetObjectType()
        {
            Console.WriteLine("This is Sphere");
        }
    }
}
