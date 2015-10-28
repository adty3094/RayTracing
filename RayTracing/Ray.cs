﻿using System;
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
        private Vector direction;
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
        { }

        public Ray(Vector position, Vector direction)
        {
            this.position = position;
            this.direction = direction;
            this.intersectDistance = double.MaxValue;
            this.intersectWith = null;
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
    }
}
