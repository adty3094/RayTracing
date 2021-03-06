﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "testscenes/scene3.test";
            string  outputFile = "hore.bmp";

            if(File.Exists(inputFile))
            {
                Scene scene = new Scene(inputFile);
                //scene.PrintContent();
                Bitmap output = new Bitmap(Func.RayTrace(scene));
                output.Save(outputFile);
                output.Dispose();
            }
            Console.WriteLine("Udah slese bro");
            Console.ReadKey();
        }
    }
}
