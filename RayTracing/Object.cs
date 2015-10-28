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
        public virtual double IntersectObject();
    }
}
