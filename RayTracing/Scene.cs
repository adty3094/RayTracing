using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    public class Scene
    {
        private List<Object> objectList;

        public List<Object> ObjectList
        {
            get { return objectList; }
            set { objectList = value; }
        }

        private Camera camera;

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }



        public Scene(Camera cam)
        {
            this.camera = cam;
        }

        public void AddObject(Object obj)
        {
            this.objectList.Add(obj);
        }
    }
}
