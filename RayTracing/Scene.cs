﻿using System;
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

        public Scene(String inputFile)
        {
            objectList = new List<Object>();
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
                param[i] = double.Parse(words[i + 1]);

            if (command.Equals("size"))
            {
                Screen.width = (int)param[0];
                Screen.height = (int)param[1];
            }

            else if (command.Equals("camera"))
            {
                Camera = new Camera(param);
            }

            else if (command.Equals("sphere"))
            {
                ObjectList.Add((Object)new Sphere(new Vector(param[0], param[1], param[2]), param[3]));
            }

            else
                return;
        }
    }
}
