using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Vector
    {
        double x, y, z;
        double normalize;

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

        public void NormalizeCalculation()
        {
            normalize = Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public double DotProduct(Vector u, Vector v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }

        public Vector CrossProduct(Vector u, Vector v)
        {
            Vector crossProduct = new Vector(u.y * v.z - u.z * v.y,
                                             u.z * v.x - u.x * v.z,
                                             u.x * v.y - u.y * v.x);
            return crossProduct;
        }
    }
}
