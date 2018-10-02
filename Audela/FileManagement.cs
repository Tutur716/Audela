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
            if (!Directory.Exists(GenerationSettings.path + "/Stars"))
            {
                Directory.CreateDirectory(GenerationSettings.path + "/Stars");
            }
        }

        public static void Clean(DirectoryInfo directoryInfo)
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
    }
}
