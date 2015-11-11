using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Camera
    {
        private Vector lookFrom; 
        private Vector lookAt;
        private Vector up;
        private double fovy;
        private double fovx;

        private Vector u, v, w;

        public Camera(double LookFromX, double LookFromY, double LookFromZ,
               double LookAtX, double LookAtY, double LookAtZ,
               double UpX, double UpY, double UpZ, 
               double fovy)
        {
            this.lookFrom = new Vector(LookFromX, LookFromY, LookFromZ);
            this.lookAt = new Vector(LookAtX, LookAtY, LookAtZ);
            this.up = new Vector(UpX, UpY, UpZ);
            this.fovy = fovy;
            CalculateUVW();
            this.fovx = Func.RadianToDegree(Math.Atan(CalculateFOVX()))*2;
        }
        public Camera(double[] param)
        {
            this.lookFrom = new Vector(param[0], param[1], param[2]);
            this.lookAt = new Vector(param[3], param[4], param[5]);
            this.up = new Vector(param[6], param[7], param[8]);
            this.fovy = param[9];
            CalculateUVW();
            this.fovx = Func.RadianToDegree(Math.Atan(CalculateFOVX())) * 2;
        }

        public Vector GetLookFrom()
        {
            return this.lookFrom;
        }

        public Vector GetLookAt()
        {
            return this.lookAt;
        }

        public Vector GetUp()
        {
            return this.up;
        }

        private void CalculateUVW()
        {
            Vector a = lookFrom - lookAt;
            Vector b = up;
            this.w = a / a.Distance();
            this.u = Vector.CrossProduct(b, this.w) / Vector.CrossProduct(b, this.w).Distance();
            this.v = Vector.CrossProduct(this.w, this.u);
        }

        private double CalculateFOVX()
        {
            Console.WriteLine(Screen.width + " " + Screen.height);
            return ((double)Screen.width / (double)Screen.height) * (Math.Tan(Func.DegreeToRadian(this.fovy)/2)); 
        }

        public Vector GetVectorU()
        {
            return this.u;
        }

        public Vector GetVectorV()
        {
            return this.v;
        }

        public Vector GetVectorW()
        {
            return this.w;
        }

        public double GetFOVX()
        {
            return this.fovx;
        }

        public double GetFOVY()
        {
            return this.fovy;
        }
    }
}
