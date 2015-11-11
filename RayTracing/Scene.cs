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

        private Matrices transformMatrix;
        private Color color;

        public Scene(String inputFile)
        {
            Console.WriteLine("Masuk constructor Scene");
            objectList = new List<Object>();
            Vertex = new List<Vector>();
            StreamReader file = new StreamReader(inputFile);
            String command;
            transformMatrix = new Matrices(4, 4);
            while ((command = file.ReadLine()) != null)
                ExecuteCommand(command);
            file.Close();
        }

        public void ExecuteCommand(String fullcommand)
        {
            if (fullcommand.Contains('#'))
                return;
            bool pushTransform = false;
            fullcommand = fullcommand.Trim();
            String[] words = fullcommand.Split(' ');
            String command = words[0];

            double[] param = new double[words.Length - 1];
            Console.WriteLine(words[0]);
            for (int i = 0; i < param.Length; i++)
            {
                Console.WriteLine(words[i + 1]);
                param[i] = double.Parse(words[i + 1]); 
            }

            if (command.Equals("size"))
            {
                //Console.WriteLine("Size Command");
                Screen.width = (int)param[0];
                Screen.height = (int)param[1];
            }

            else if (command.Equals("camera"))
            {
                //Console.WriteLine("Camera Command");
                this.Camera = new Camera(param);
                Console.WriteLine(camera.GetFOVX());
                Console.WriteLine(camera.GetFOVY());
            }

            else if (command.Equals("sphere"))
            {
                //Console.WriteLine("Sphere Command");
                Matrices temp = new Matrices(4, 4);
                temp.Matrix = (temp * this.transformMatrix).Matrix;
                this.ObjectList.Add((Object)new Sphere(new Vector(param[0], param[1], param[2]), param[3], temp, this.color));
            }

            else if (command.Equals("vertex"))
            {
                //Console.WriteLine("Vertex Command");
                this.Vertex.Add(new Vector(param[0], param[1], param[2]));
            }

            else if (command.Equals("tri"))
            {
                //Console.WriteLine("Triangle Command");
                Matrices temp = new Matrices(4, 4);
                temp.Matrix = (temp * this.transformMatrix).Matrix;
                this.ObjectList.Add((Object)new Triangle(Vertex[(int)param[0]], Vertex[(int)param[1]], Vertex[(int)param[2]], temp, this.color));
            }

            else if (command.Equals("pushTransform"))
            {
                pushTransform = true;
            }

            else if(command.Equals("popTransform"))
            {
                pushTransform = false;
                this.transformMatrix = new Matrices(4, 4);
            }

            else if (command.Equals("translate") || command.Equals("scale")) 
            {
                Matrices temp = new Matrices(words[0] ,param[0], param[1], param[2]);
                this.transformMatrix = temp * this.transformMatrix;
            }

            else if (command.Equals("rotate"))
            {
                Matrices temp = new Matrices((int)param[0], (int)param[1], (int)param[2], param[3]);
                this.transformMatrix = temp * this.transformMatrix;
            }
            
            else if(command.Equals("ambient"))
            {
                this.color = Color.FromArgb( (int)(255*param[0]), (int)(255*param[1]), (int)(255*param[2]));
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
