using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    class Sphere : Object
    {
        Vector center;
        double radius;

        public Sphere(Vector center, double radius, Color color) : base(color)
        {
            this.center = center;
            this.radius = radius;
        }

        public override bool IsIntersect(ref Ray ray)
        {
            double a = Vector.DotProduct(ray.GetDirection(), ray.GetDirection());
            double b = 2 * Vector.DotProduct((ray.GetPosition() - this.center), ray.GetDirection());
            double c = Vector.DotProduct((ray.GetPosition() - this.center), (ray.GetPosition() - this.center)) - radius * radius;
            double D = b*b - 4*a*c;
            if (a == 0)
            {
                ray.IntersectDistance = -c / b;
                ray.IntersectWith = this;
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
                    ray.IntersectDistance = Math.Min(t[0], t[1]);
                    ray.IntersectWith = this;
                    return true;
                }
                else if (t[0] < 0 && t[1] > 0)
                {
                    ray.IntersectDistance = t[1];
                    ray.IntersectWith = this;
                    return true;
                }
                else if (t[0] > 0 && t[1] < 0)
                {
                    ray.IntersectDistance = t[0];
                    ray.IntersectWith = this;
                    return true;
                }
                else return false;
            }
            
        }
    }
}
