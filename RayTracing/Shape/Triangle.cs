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

        private Matrices transform;

        public Matrices Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        

        public Triangle(Vector pointA, Vector pointB, Vector pointC, Matrices transform, Color color)
            :base(transform,color)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.pointC = pointC;
        }

        public override bool IsIntersect(Ray ray, Matrices transform)
        {
            Vector temp = Vector.CrossProduct((this.pointC - this.pointA),(this.pointB - this.pointA));
            Vector n = temp / temp.Distance();
            double a = Vector.DotProduct(this.pointA, n);
            double b = Vector.DotProduct(ray.GetPosition(), n);
            double c = Vector.DotProduct(ray.GetDirection(), n);
            double t = (a - b) / c;
            Vector pointT = ray.GetPosition() + (ray.GetDirection() * t);
            if (PointInTriangle(pointT))
            {
                Vector temp1 = ray.Direction * t;
                Matrices temp2 = new Matrices(temp1.x, temp1.y, temp1.z, 0);
                temp2 = transform.Matrix * temp2;
                temp1.x = temp2.Matrix[0, 0];
                temp1.y = temp2.Matrix[1, 0];
                temp1.z = temp2.Matrix[2, 0];
                ray.IntersectDistance = temp1.Distance() ;
                //ray.IntersectDistance = t ;
                //ray.IntersectWith = this;
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

        public override void GetObjectType()
        {
            Console.WriteLine("This is Triangle");
        }
    }
}
