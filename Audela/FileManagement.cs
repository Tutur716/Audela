using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Audela
{
    class FileManagement
    {
        public static void Create()
        {
            if (!Directory.Exists(GenerationSettings.path))
            {
                Directory.CreateDirectory(GenerationSettings.path);
            }
            if (!Directory.Exists(GenerationSettings.path + "/Galaxy"))
            {
                Directory.CreateDirectory(GenerationSettings.path + "/Galaxy");
            }
            if (!Directory.Exists(CelestialBody.Planet.Terrain.Map.mapPath))
            {
                Directory.CreateDirectory(CelestialBody.Planet.Terrain.Map.mapPath);
            }

            if (!Directory.Exists(CelestialBody.Planet.Terrain.Map.cachePath))
            {
                Directory.CreateDirectory(CelestialBody.Planet.Terrain.Map.cachePath);
            }
        }

        public static void CreateSpecific(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void Clean(DirectoryInfo directoryInfo)
        {
            if (directoryInfo.Exists)
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                    
                }

                foreach (DirectoryInfo subfolder in directoryInfo.GetDirectories())
                {
                    Clean(subfolder);
                }
            }

            try
            {
                directoryInfo.Delete();
            }
            catch (Exception e)
            { }
        }
    }
}
