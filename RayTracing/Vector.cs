using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Vector
    {
        double x, y, z;

        public Vector()
        {

        }
        
        public Vector(double x, double y, double z)
        {
            SetVector(x, y, z);
        }

        public void SetVector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Normalize()
        {
            return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public void PrintVector()
        {
            Console.WriteLine(string.Format("{0} {1} {2}", this.x, this.y, this.z));
        }

        public static double DotProduct(Vector u, Vector v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }

        public static Vector CrossProduct(Vector u, Vector v)
        {
            Vector crossProduct = new Vector(u.y * v.z - u.z * v.y,
                                             u.z * v.x - u.x * v.z,
                                             u.x * v.y - u.y * v.x);
            return crossProduct;
        }

        public static Vector operator+(Vector u, Vector v)
        {
            return new Vector(u.x + v.x, u.y + v.y, u.z + v.z);
        }

        public static Vector operator -(Vector u, Vector v)
        {
            return new Vector(u.x - v.x, u.y - v.y, u.z - v.z);
        }

        public static Vector operator*(Vector u, double n)
        {
            return new Vector(u.x * n, u.y * n, u.z * n);
        }

        public static Vector operator/(Vector u, double n)
        {
            return new Vector(u.x / n, u.y / n, u.z / n);
        }
    }
}
