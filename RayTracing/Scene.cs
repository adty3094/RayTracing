using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Scene
    {
        private List<Object> listObject;
        
        public Scene()
        {

        }

        public void AddObject(Object obj)
        {
            this.listObject.Add(obj);
        }
    }
}
