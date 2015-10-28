using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Sphere : Object
    {
        Vector center;
        double radius;

        public Sphere()
        {

        }

        public Sphere(Vector center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public override double IntersectObject(Ray ray)
        {
            double a = Vector.DotProduct(ray.GetDirection(), ray.GetDirection());
            double b = 2 * Vector.DotProduct((ray.GetPosition() - this.center), ray.GetDirection());
            double c = Vector.DotProduct((ray.GetPosition() - this.center), (ray.GetPosition() - this.center)) - radius * radius;
            double D = b*b - 4*a*c;
            if (a == 0)
                return -c / b;
            else if (D < 0)
                return -1;
            else
            {
                double[] t = new double[2];
                t[0] = -b + Math.Sqrt(D) / (2*a);
                t[1] = -b - Math.Sqrt(D) / (2 * a);
                if (t[0] > 0 && t[1] > 0)
                {
                    if (t[0] < t[1]) return t[0];
                    else return t[1];
                }
                else if (t[0] < 0 && t[1] > 0) return t[1];
                else if (t[0] > 0 && t[1] < 0) return t[0];
                else return -1;
            }
            
        }
    }
}
