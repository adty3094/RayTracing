using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Ray
    {
        private Vector position;
        private Vector direction;

        public Ray()
        { }

        public Ray(Vector position, Vector direction)
        {

        }

        public void SetPosition(Vector position)
        {
            this.position = position;
        }

        public Vector GetPosition()
        {
            return this.position;
        }

        public void SetDirection(Vector direction)
        {
            this.direction = direction;
        }

        public Vector GetDirection()
        {
            return this.direction;
        }
    }
}
