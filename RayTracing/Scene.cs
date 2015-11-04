using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        private List<Vector> vertex;

        public List<Vector> Vertex
        {
            get { return vertex; }
            set { vertex = value; }
        }


        public Scene(String inputFile)
        {
            Console.WriteLine("Masuk constructor Scene");
            objectList = new List<Object>();
            Vertex = new List<Vector>();
            StreamReader file = new StreamReader(inputFile);
            String command;
            while ((command = file.ReadLine()) != null)
                ExecuteCommand(command);
            file.Close();
        }

        public void ExecuteCommand(String fullcommand)
        {
            if (fullcommand.Contains('#'))
                return;
            fullcommand = fullcommand.Trim();
            String[] words = fullcommand.Split(' ');
            String command = words[0];

            double[] param = new double[words.Length - 1];
            for (int i = 0; i < param.Length; i++)
            { param[i] = double.Parse(words[i + 1]); }

            if (command.Equals("size"))
            {
                Console.WriteLine("Size Command");
                Screen.width = (int)param[0];
                Screen.height = (int)param[1];
            }

            else if (command.Equals("camera"))
            {
                Console.WriteLine("Camera Command");
                this.Camera = new Camera(param);
            }

            else if (command.Equals("sphere"))
            {
                Console.WriteLine("Sphere Command");
                this.ObjectList.Add((Object)new Sphere(new Vector(param[0], param[1], param[2]), param[3]));
            }

            else if (command.Equals("vertex"))
            {
                Console.WriteLine("Vertex Command");
                this.Vertex.Add(new Vector(param[0], param[1], param[2]));
            }

            else if (command.Equals("tri"))
            {
                Console.WriteLine("Triangle Command");
                this.ObjectList.Add((Object)new Triangle(Vertex[(int)param[0]], Vertex[(int)param[1]], Vertex[(int)param[2]]));
            }

            else
                return;
        }

        public void PrintContent()
        {
            foreach (Object obj in this.ObjectList)
            {
                obj.GetObjectType();
            }
            this.Camera.GetVectorU().PrintVector();
            this.Camera.GetVectorV().PrintVector();
            this.Camera.GetVectorW().PrintVector();
        }
    }
}
