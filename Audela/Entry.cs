using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using Audela.Generation;
using Audela.Generation.Data;

namespace Audela
{
    class Entry
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
            Console.WriteLine("Enter the seed you want:");
            string seedInput = Console.ReadLine();

            Console.WriteLine("\nEnter the number of stars you want:");
            double nbStar = 1;
            Double.TryParse(Console.ReadLine(), out nbStar);

            Console.WriteLine("\nEnter the game root path:");
            GenerationSettings.path = Console.ReadLine() + "/GameData/Audela/";

            Console.WriteLine("\nEnter the size of the galaxy in lightyears");
            Double.TryParse(Console.ReadLine(), out Galaxy.Size);

            Console.WriteLine("\nGenerating stars..\n");


            FileManagement.Clean(new DirectoryInfo (GenerationSettings.path + "/Stars"));
            FileManagement.Create();


            Stopwatch sw = new Stopwatch();
            sw.Start();
            int seed = seedInput.GetHashCode();

            Random r = RandomBySeed.SetRandom(seed);

            List<Star> starList = new List<Star>();

            //The number of star in the galaxy (low value)
            for (double i = 0; i < nbStar; i++)
            {
                Star s = new Star();
                s.Random();
                s.Name = "Star " + (i+1);
                CreateConfiguration.CreateAndSave(s);
                //starList.Add(s.Random());
            }

            //temp(starList);

            sw.Stop();
            Console.WriteLine("\nGalaxy was created with {0} stars!\nTime elapsed: {1:hh\\:mm\\:ss}", nbStar,sw.Elapsed);
            Console.ReadKey();
        }

        /*static void temp(List<Star> sl)
        {
            int m = 0;
            int k = 0;
            int g = 0;
            int f = 0;
            int a = 0;
            int b = 0;
            int o = 0;

            foreach(Star s in sl)
            {
                switch (s.spectralType)
                {
                    case SpectralTypes.M: m++; break;
                    case SpectralTypes.K: k++; break;
                    case SpectralTypes.G: g++; break;
                    case SpectralTypes.F: f++; break;
                    case SpectralTypes.A: a++; break;
                    case SpectralTypes.B: b++; break;
                    case SpectralTypes.O: o++; break;
                }
            }

            Console.WriteLine("M Class = {0}\nK Class = {1}\nG Class = {2}\nF Class = {3}\nA Class = {4}\nB Class = {5}\nO Class = {6}\n", m, k, g, f, a, b, o);
        }*/
    }
}
