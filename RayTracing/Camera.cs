using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Camera
    {
        private Vector lookFrom; 
        private Vector lookAt;
        private Vector up;
        private double fov;

        private Vector u, v, w;

        public Camera(double LookFromX, double LookFromY, double LookFromZ,
               double LookAtX, double LookAtY, double LookAtZ,
               double UpX, double UpY, double UpZ, double fov)
        {
            this.lookFrom = new Vector(LookFromX, LookFromY, LookFromZ);
            this.lookAt = new Vector(LookAtX, LookAtY, LookAtZ);
            this.up = new Vector(UpX, UpY, UpZ);
        }

        public void CalculateUVW()
        {

        }
    }
}
