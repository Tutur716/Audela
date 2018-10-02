using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;

namespace Audela
{
    class GenerationSettings
    {
        public static string path = "Game path is not set!";
        public static string rootPath = "Root Path isn't set";
        public string test;
        public static int StarNumber = 1;

        public static string Seed = "Seed is not set!";

        public static double GalaxySize = 0.1;

        #region Headers
        #region Settings
        private static string SettingsHeader = "///////////////////////////////////////////////" + Environment.NewLine +
                                               "// THIS FILE CONTAINTS EVERYTHING FOR THE DEV//" + Environment.NewLine +
                                               "// TO DEBUG THINGS, OR THE USER TO RECREATE A//" + Environment.NewLine +
                                               "// GALAXY THEY LIKED (non licensed file)     //" + Environment.NewLine +
                                               "///////////////////////////////////////////////" + Environment.NewLine + Environment.NewLine;
        #endregion

        #region Mainstream generated files
        public static string mainstreamHeader = "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine +
                                                "// Copyright (c) Tutur (@Tutur on forum.kerbalspaceprogram.com), All Rights Reserved.                    				//" + Environment.NewLine +
                                                "//															                                                            //" + Environment.NewLine +
                                                "// Every softwares/programs (Source code included), (every files with the exception of \"Settings.txt\"),     			//" + Environment.NewLine +
                                                "// distributed or generated, and in general Audela, are under an All Right Reserved licensing which means:		        //" + Environment.NewLine +
                                                "//															                                                            //" + Environment.NewLine +
                                                "// You can edit them, but any distribution is formally forbidden, in separated files or with others files/mods/modpacks.//" + Environment.NewLine +
                                                "//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine + Environment.NewLine;
        #endregion
        #endregion

        public static void WriteSettings()
        {
            ConfigNode c = new ConfigNode();

            ConfigNode s = new ConfigNode("Audela - Generation Settings");
            c.AddConfigNode(s);
            s.AddValue("Seed", Seed);
            s.AddValue("Audela path", path);
            s.AddValue("Number of stars generated", StarNumber.ToString());
            s.AddValue("Galaxy size", GalaxySize.ToString());
            s.AddValue("Root KSP path", rootPath);

            c.Save(path + @"\Galaxy\Settings.txt", SettingsHeader);
        }
    }
}
