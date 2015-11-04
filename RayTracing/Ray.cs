using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RayTracing
{
    public class Ray
    {
        private Vector position;

        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector direction;

        public Vector Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }



        private double intersectDistance;

        public double IntersectDistance
        {
            get { return intersectDistance; }
            set { intersectDistance = value; }
        }

        private Object intersectWith;

        public Object IntersectWith
        {
            get { return intersectWith; }
            set { intersectWith = value; }
        }


        public Ray()
        {
            this.intersectDistance = double.MaxValue;
            this.IntersectWith = null;
            this.color = Color.FromArgb(0, 0, 0);
        }

        public Ray(Vector position, Vector direction)
        {
            this.position = position;
            this.direction = direction;
            this.intersectDistance = double.MaxValue;
            this.IntersectWith = null;
            this.color = Color.FromArgb(0, 0, 0);
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
         
        public void GetInformation()
        {
            Console.WriteLine("===================================================");
            Console.Write("Ray position:  "); this.GetPosition().PrintVector();
            Console.Write("Ray Direction: "); this.GetDirection().PrintVector();
            Console.WriteLine("Ray Intersect Distance: "+this.IntersectDistance);
            Console.WriteLine("Ray Intersect With: " + this.IntersectWith);
            Console.WriteLine("===================================================");
            Console.WriteLine();
        }
    }
}
