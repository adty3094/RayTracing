using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    abstract class Object
    {
        private Color color;

        public Object(Color color)
        {
            this.color = color;
        }
        public abstract bool IsIntersect(ref Ray ray);
    }
}
