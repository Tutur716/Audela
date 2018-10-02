using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;
using Audela.CelestialBody.Planet.Stepping;

namespace Audela.CelestialBody.Galaxy
{
    class Systems
    {
        static List<Body> Stars = new List<Body>();
        static List<Body> Planets = new List<Body>();

        public static void Generate()
        {
            ConfigNode StarParser = new ConfigNode();
            ConfigNode StarMM = new ConfigNode("@Kopernicus:FOR[Audela]");
            StarParser.AddConfigNode(StarMM);

            ConfigNode PlanetParser = new ConfigNode();
            ConfigNode PlanetMM = new ConfigNode("@Kopernicus:FOR[Audela]");
            PlanetParser.AddConfigNode(PlanetMM);

            ConfigNode MoonParser = new ConfigNode();
            ConfigNode MoonMM = new ConfigNode("@Kopernicus:FOR[Audela]");
            MoonParser.AddConfigNode(MoonMM);

            #region Stars
            for (int i = 0; i < GenerationSettings.StarNumber; i++)
            {
                Star.Star s = GenerateStar(i + 1);
                StarMM.AddConfigNode(s.Config);

                #region Planets
                int pID = 1;

                PlanetSetupStep(s.properties.Radius / (UsefulValues.SunRadius * 1e3));

                //bool stPass = false;
                foreach (Step st in Steps.PlanetList)
                {
                    //if (stPass == false)
                    //{
                        if (Steps.RandomSpawn(st))
                        {
                            Body p = GeneratePlanet(s, pID, st);
                            if (p.orbit.SemiMajorAxis <= Star.Data.SpectralTypesData.GetMaxSMA(s.SpectralType, s.LuminosityClass) && p.orbit.SemiMajorAxis <= s.SphereOfInfluence && p.orbit.SemiMajorAxis >= s.RocheLimit + s.properties.Radius)
                            {
                                PlanetMM.AddConfigNode(p.Config);
                                Planets.Add(p);
                                pID++;

                                int mID = 1;
                                MoonStepSetup(p.properties.Radius / (UsefulValues.KerbinRadius));
                                //bool stmPass = false;
                                foreach (Step stm in Steps.MoonList)
                                {
                                    //if (stmPass == false)
                                    //{
                                        if (Steps.RandomSpawn(stm))
                                        {
                                            Body m = GeneratePlanet(p, mID, stm);
                                            if ((m.orbit.SemiMajorAxis <= (p.ManualSOI + p.properties.Radius)) && (m.orbit.SemiMajorAxis >= (p.ManualRoche + p.properties.Radius)))
                                            {
                                                
                                                MoonMM.AddConfigNode(m.Config);
                                                Console.WriteLine("\n\n[=== {0} ===]\nSMA = {1}\nPARENT SOI = {2}", m.Name, m.orbit.SemiMajorAxis, p.ManualSOI);
                                                Planets.Add(m);
                                                mID++;
                                            }
                                        }
                                        //stmPass = true;
                                    //}
                                    //else
                                    //{
                                        //stmPass = false;
                                    //}
                                }
                            }
                        }
                        //stPass = true;
                    //}
                    //else
                    //{
                        //stPass = false;
                    //}
                }
            }
            StarParser.Save(GenerationSettings.path + "/Galaxy/Stars.cfg", GenerationSettings.mainstreamHeader);
            #endregion

            #region New Kerbol
            NewKerbol NK = new NewKerbol();
            NK.Generate();
            NK.Config.Save(GenerationSettings.path + "/Galaxy/Kerbol.cfg", GenerationSettings.mainstreamHeader);
            #endregion  
            
            PlanetParser.Save(GenerationSettings.path + "/Galaxy/Planets.cfg", GenerationSettings.mainstreamHeader);
            MoonParser.Save(GenerationSettings.path + "/Galaxy/Moons.cfg", GenerationSettings.mainstreamHeader);

            SaveMaps();
            #endregion
        }
        private static void SaveMaps()
        {
            foreach (Body b in Planets)
            {
                if (b.Type != BodyType.GasGiant)
                {
                    List<ConfigNode> modcn = b.Terrain.ParsedMods;

                    #region Generator Settings
                    List<Parameter> genParams = new List<Parameter>();

                    #region Resolution Selector
                    double resolution = 1024;
                    double lowRes = 512;
                    double lowResRad = 135000;
                    double midRes = 1024;
                    double midResRad = 300000;
                    double highRes = 2048;
                    double highResRad = 750000;
                    double ultraRes = 4096;

                    if (b.properties.Radius <= lowResRad) resolution = lowRes;
                    if (b.properties.Radius > lowResRad && b.properties.Radius <= midResRad) resolution = midRes;
                    if (b.properties.Radius > midResRad && b.properties.Radius <= highResRad) resolution = highRes;
                    if (b.properties.Radius > highResRad) resolution = ultraRes;
                    #endregion

                    #region Parameters Addition
                    genParams.Add(AddParam("__resolution", resolution));

                    genParams.Add(AddParam("__radius", b.properties.Radius));

                    genParams.Add(AddParam("__hasOcean", "true"));

                    genParams.Add(AddParam("__oceanHeight", "0"));

                    genParams.Add(AddParam("__oceanColor", /*((Planet.Planet)b).OceanColor)*/ "0, 0, 0 ,1"));

                    genParams.Add(AddParam("__normalStrength", "6"));

                    genParams.Add(AddParam("mapMaxHeight", "15000"));
                    #endregion

                    Planet.Terrain.Map.Save(new string[] { "" }, b, genParams);
                    #endregion
                }
            }
        }

        private static Star.Star GenerateStar(int ID)
        {
            Star.Star s = new Star.Star();

            Stars.Add(s.Generate(ID));

            return s;
        }

        private static Body GeneratePlanet(Body ParentBody, int ID, Step Step)
        {
            /*if(ParentBody.Type == BodyType.Star)
            {
                PlanetSetupStep(ParentBody.properties.Radius / (UsefulValues.SunRadius * 1e3));
            }
            else
            {
                MoonStepSetup(ParentBody.properties.Radius / UsefulValues.KerbinRadius);
            }*/

            Planet.Planet p = new Planet.Planet();

            p.orbit.ReferenceBody = ParentBody;

            p.Generate(Step, ID);

            return p;
        }

        //Planet creation ""Curve""
        public static void PlanetSetupStep(double StarRadInSuns)
        {
            Steps.Clear(Steps.PlanetList);

            #region Steps
            Steps.CreateStep(1450000000 * StarRadInSuns, 0.115, 0.7, 0.3, Steps.PlanetList);
            Steps.CreateStep(2650000000 * StarRadInSuns, 0.12, 0.85, 0.15, Steps.PlanetList);
            Steps.CreateStep(3650000000 * StarRadInSuns, 0.15, 0.9, 0.1, Steps.PlanetList);
            Steps.CreateStep(4700000000 * StarRadInSuns, 0.175, 0.9, 0.1, Steps.PlanetList);
            Steps.CreateStep(6000000000 * StarRadInSuns, 0.179, 0.95, 0.05, Steps.PlanetList);
            Steps.CreateStep(8730000000 * StarRadInSuns, 0.181, 0.8, 0.2, Steps.PlanetList);
            Steps.CreateStep(13400000000 * StarRadInSuns, 0.177, 0.75, 0.25, Steps.PlanetList);
            Steps.CreateStep(17300000000 * StarRadInSuns, 0.184, 0.73, 0.27, Steps.PlanetList);
            Steps.CreateStep(26800000000 * StarRadInSuns, 0.178, 0.7, 0.3, Steps.PlanetList);
            Steps.CreateStep(34900000000 * StarRadInSuns, 0.179, 0.68, 0.32, Steps.PlanetList);
            Steps.CreateStep(57900000000 * StarRadInSuns, 0.18, 0.65, 0.35, Steps.PlanetList);
            Steps.CreateStep(87500000000 * StarRadInSuns, 0.16, 0.61, 0.39, Steps.PlanetList);
            Steps.CreateStep(1.2e+11 * StarRadInSuns, 0.17, 0.6, 0.4, Steps.PlanetList);
            Steps.CreateStep(2e+11 * StarRadInSuns, 0.15, 0.8, 0.2, Steps.PlanetList);
            Steps.CreateStep(3e+11 * StarRadInSuns, 0.19, 0.8, 0.2, Steps.PlanetList);
            Steps.CreateStep(4e+11 * StarRadInSuns, 0.17, 0.5, 0.5, Steps.PlanetList);
            Steps.CreateStep(6e+11 * StarRadInSuns, 0.16, 0.2, 0.8, Steps.PlanetList);
            Steps.CreateStep(8e+11 * StarRadInSuns, 0.19, 0.09, 0.91, Steps.PlanetList);
            Steps.CreateStep(1e+12 * StarRadInSuns, 0.17, 0.05, 0.95, Steps.PlanetList);
            Steps.CreateStep(1.4e+12 * StarRadInSuns, 0.18, 0.2, 0.8, Steps.PlanetList);
            Steps.CreateStep(1.7e+12 * StarRadInSuns, 0.15, 0.3, 0.7, Steps.PlanetList);
            Steps.CreateStep(2.1e+12 * StarRadInSuns, 0.15, 0.4, 0.6, Steps.PlanetList);
            Steps.CreateStep(2.7e+12 * StarRadInSuns, 0.14, 0.45, 0.55, Steps.PlanetList);
            Steps.CreateStep(3.1e+12 * StarRadInSuns, 0.14, 0.3, 0.7, Steps.PlanetList);
            Steps.CreateStep(3.7e+12 * StarRadInSuns, 0.17, 0.25, 0.75, Steps.PlanetList);
            Steps.CreateStep(4.2e+12 * StarRadInSuns, 0.16, 0.3, 0.7, Steps.PlanetList);
            Steps.CreateStep(5e+12 * StarRadInSuns, 0.15, 0.35, 0.65, Steps.PlanetList);
            Steps.CreateStep(5.5e+12 * StarRadInSuns, 0.14, 0.7, 0.3, Steps.PlanetList);
            Steps.CreateStep(6e+12 * StarRadInSuns, 0.17, 0.8, 0.2, Steps.PlanetList);
            Steps.CreateStep(6.5e+12 * StarRadInSuns, 0.16, 0.9, 0.1, Steps.PlanetList);
            #endregion
        }

        public static void MoonStepSetup(double PlanetRadInKerbins)
        {
            Steps.Clear(Steps.MoonList);

            #region Steps
            Steps.CreateStep(5400000 * PlanetRadInKerbins, 0.20, 1, 0, Steps.MoonList);
            Steps.CreateStep(11000000 * PlanetRadInKerbins, 0.225, 1, 0, Steps.MoonList);
            Steps.CreateStep(18000000 * PlanetRadInKerbins, 0.25, 1, 0, Steps.MoonList);
            Steps.CreateStep(24000000 * PlanetRadInKerbins, 0.275, 1, 0, Steps.MoonList);
            Steps.CreateStep(30000000 * PlanetRadInKerbins, 0.3, 1, 0, Steps.MoonList);
            Steps.CreateStep(35000000 * PlanetRadInKerbins, 0.325, 1, 0, Steps.MoonList);
            Steps.CreateStep(42000000 * PlanetRadInKerbins, 0.3, 1, 0, Steps.MoonList);
            Steps.CreateStep(48000000 * PlanetRadInKerbins, 0.275, 1, 0, Steps.MoonList);
            Steps.CreateStep(72000000 * PlanetRadInKerbins, 0.25, 1, 0, Steps.MoonList);
            Steps.CreateStep(98000000 * PlanetRadInKerbins, 0.225, 1, 0, Steps.MoonList);
            Steps.CreateStep(115000000 * PlanetRadInKerbins, 0.2, 1, 0, Steps.MoonList);
            Steps.CreateStep(720000000 * PlanetRadInKerbins, 0.15, 1, 0, Steps.MoonList);

            #endregion
        }

        /// <summary>
        /// Adds a non node parameter
        /// </summary>
        static Parameter AddParam(string key, object value)
        {
            return new Parameter() { key = key, value = value };
        }
        /// <summary>
        /// Adds a node paramater
        /// </summary>
        static Parameter AddParam(ConfigNode ConfigNode)
        {
            return new Parameter() { key = "Generic Config Node", value = ConfigNode, isNode = true };
        }
    }
}