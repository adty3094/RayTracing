using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class ViewPlane
    {
        private Camera camera;

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        private double worldWidth;
        private double worldHeight;

        private int pixelWidth;

        public int PixelWidth
        {
            get { return pixelWidth; }
            set { pixelWidth = value; }
        }

        private int pixelHeight;

        public int PixelHeight
        {
            get { return pixelHeight; }
            set { pixelHeight = value; }
        }

        Vector position;
        Vector upperLeft;

        public ViewPlane(int width, int height, Camera camera)
        {
            this.pixelWidth = width;
            this.pixelHeight = height;
            this.camera = camera;

            this.worldHeight = 2 * Math.Tan(Func.DegreeToRadian(camera.GetFOVY())/2);
            this.worldWidth = worldHeight * (double)PixelWidth / (double)PixelHeight;

            CalculateUpperLeft();
        }

        public void CalculateUpperLeft()
        {
            Vector center = camera.GetLookFrom();
            center -= camera.GetVectorW();
            position = center;
            upperLeft = center - camera.GetVectorU() * (worldWidth / 2)
                        + camera.GetVectorV() * (worldHeight / 2);
        }

        public Vector GetNewLocation(int row, int col)
        {
            Vector newLocation =  this.upperLeft
            + camera.GetVectorU() * (col + 0.5) * (worldWidth / PixelWidth)
            - camera.GetVectorV() * (row + 0.5) * (worldHeight / PixelHeight);

            return newLocation;
        }
    }
}
