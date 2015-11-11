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

        private Matrices _transform;

        public Matrices Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }
        

        public Object(Matrices transform, Color color)
        {
            this.Color = color;
            this.Transform = new Matrices(4,4);
            this.Transform.Matrix = (transform * new Matrices(4, 4)).Matrix ;
            this.Transform.Invers = Matrices.InversCalculate(transform.Matrix);
//            this.Transform = temp
        }
        public abstract bool IsIntersect(Ray ray, Matrices transform);
        public abstract void GetObjectType();
    }
}
