using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    public abstract class Object
    {
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }


        public Object()
        {
            Color = Color.Red;
        }
        public abstract bool IsIntersect(ref Ray ray);
        public abstract void GetObjectType();
    }
}
