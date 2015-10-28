using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    public class Triangle : Object
    {
        private Vector pointA;
        private Vector pointB;
        private Vector pointC;

        public Triangle(Vector pointA, Vector pointB, Vector pointC, Color color) : base(color)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.pointC = pointC;
        }

        public override bool IsIntersect(ref Ray ray)
        {
            Vector temp = Vector.CrossProduct((this.pointC - this.pointA),(this.pointB - this.pointA));
            Vector n = temp / temp.Normalize();
            double a = Vector.DotProduct(this.pointA, n);
            double b = Vector.DotProduct(ray.GetPosition(), n);
            double c = Vector.DotProduct(ray.GetDirection(), n);
            double t = (a - b) / c;
            Vector pointT = ray.GetPosition() + (ray.GetDirection() * t);
            if (PointInTriangle(pointT))
            {
                ray.IntersectDistance = t;
                ray.IntersectWith = this;
                return true;
            }
            else return false;
        }

        private bool PointInTriangle(Vector pointT)
        {
            Vector v0 = this.pointC - this.pointA;
            Vector v1 = this.pointB - this.pointA;
            Vector v2 = pointT - this.pointA;

            double dot00 = Vector.DotProduct(v0, v0);
            double dot01 = Vector.DotProduct(v0, v1);
            double dot02 = Vector.DotProduct(v0, v2);
            double dot11 = Vector.DotProduct(v1, v1);
            double dot12 = Vector.DotProduct(v1, v2);

            double invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            double u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            double v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return (u >= 0) && (v >= 0) && (u + v < 1);
        }
    }
}
