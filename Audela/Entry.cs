using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using ConfigNodeParser;

namespace Audela
{
    class Entry
    {
        static void Main(string[] args)
        {
            /*UI.Drawer Drawer = new UI.Drawer()
            {
                WindowSizeX = 100,
                WindowSizeY = 40
            };

            Drawer.Draw();*/


            System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;


            Console.WriteLine("Enter the seed you want:");
            string seedInput = Console.ReadLine();
            GenerationSettings.Seed = seedInput;

            Console.WriteLine("\nEnter the number of stars you want:");
            Int32.TryParse(Console.ReadLine(), out GenerationSettings.StarNumber);

            Console.WriteLine("\nEnter the game root path:");
            GenerationSettings.rootPath = Console.ReadLine();
            GenerationSettings.path = GenerationSettings.rootPath + "/GameData/Audela/";

            Console.WriteLine("\nEnter the size of the galaxy in lightyears");
            Double.TryParse(Console.ReadLine(), out GenerationSettings.GalaxySize);

            Console.WriteLine("\nGenerating stars..\n");

            FileManagement.Clean(new DirectoryInfo (GenerationSettings.path + "/Galaxy"));
            FileManagement.Create();

            GenerationSettings.WriteSettings();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            int seed = seedInput.GetHashCode();

            RandomBySeed.SetRandom(seed);

            //Galaxy.Generate();
            CelestialBody.Galaxy.Systems.Generate();

            sw.Stop();
            Console.WriteLine("\nGalaxy was created with {0} stars!\nTime elapsed: {1:hh\\:mm\\:ss}", GenerationSettings.StarNumber, sw.Elapsed);
            Console.ReadKey();
        }
    }
}
