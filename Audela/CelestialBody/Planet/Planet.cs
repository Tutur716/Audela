using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;
using Audela.CelestialBody.Planet.Terrain;
using Audela.CelestialBody.Planet.Terrain.Mods;

namespace Audela.CelestialBody.Planet
{
    class Planet : Body
    {
        Stepping.Step step;
        int id;
        

        public Body Generate(Stepping.Step Step, int ID)
        {
            orbit.Color = new Palette();
            orbit.Color.SetColorPalette(r.NextDouble(), r.NextDouble(), r.NextDouble(), 1);

            step = Step;
            id = ID;

            GenerateOrbit();
            GenerateProperties();

            //Terrain for solid planet case
            if(Type != BodyType.GasGiant)
            {
                GenerateTerrain();
                Terrain = new TerrainParser();
                Terrain.ModList = GenerateTerrain();
            }

            GenerateConfig();
            
            return this;
        }

        

        void GenerateProperties()
        {
            Type = Stepping.Steps.RandomBodyType(step);

            #region Name
            if(orbit.ReferenceBody.Type == BodyType.Star)
            {
                Name = orbit.ReferenceBody.Name + " Planet " + id;
            }
            else
            {
                Name = orbit.ReferenceBody.Name + " Moon " + id;
            }
            #endregion

            #region Template
            if (Type == BodyType.GasGiant)
            {
                template.Name = Templates.Jool;
            }
            else
            {
                template.Name = Templates.Laythe;
            }
            #endregion

            #region Radius
            //Planet case
            if (orbit.ReferenceBody.Type == BodyType.Star)
            {
                //Gas case
                if(Type == BodyType.GasGiant)
                {
                    properties.Radius = r.Next(2000000, 10000000);
                }
                else
                {
                    properties.Radius = r.Next(100000, 1100000);
                }
                
            }

            //Moon case
            else
            {
                if (orbit.ReferenceBody.properties.Radius > 1200000)
                {
                    properties.Radius = r.Next(50000, 800000);
                }
                else
                {
                    properties.Radius = r.Next(50000, (int)Math.Round(orbit.ReferenceBody.properties.Radius * 0.6));
                }
            }
            #endregion

            #region Mass
            //Gas case
            if(Type == BodyType.GasGiant)
            {
                properties.Mass = ((properties.Radius / 6000000) * 4.2332127e28) * (r.NextDouble() * (1.25 - 0.75) + 0.75);
                ManualMass = properties.Mass;
                ManualRoche = RocheLimit;
                ManualSOI = SphereOfInfluence;

                //Console.WriteLine("\n\nMMass = {0}\nMRoche = {1}\nMSOI = {2}", ManualMass, ManualRoche, ManualSOI);
                
            }
            else
            {
                ManualG = Math.Pow(properties.Radius / 600000, 1.8);
                ManualG *= r.NextDouble() * (1.05 - 0.95) + 0.95;

                ManualMass = ((ManualG * UsefulValues.OneGee) * properties.Radius) / UsefulValues.G;

                ManualSOI = orbit.SemiMajorAxis * Math.Pow(ManualMass / orbit.ReferenceBody.ManualMass, 0.4);
                ManualRoche = properties.Radius * Math.Pow(2 * (orbit.ReferenceBody.ManualMass / ManualMass), (float)1/3);
            }
            #endregion

            #region Description
            properties.Description = "SOI = " + Math.Round(ManualSOI)
                + " | Roche Limit = " + Math.Round(ManualRoche);
            #endregion


        }

        void GenerateOrbit()
        {
            orbit.Inclination = r.NextDouble() * (1.5 - -1.5) + -1.5;
            orbit.SemiMajorAxis = Stepping.Steps.GetStepDistance(step);
            orbit.SemiMajorAxis += r.NextDouble() * ((0.1 * orbit.SemiMajorAxis - 0.1 * orbit.SemiMajorAxis)) + (-0.1 * orbit.SemiMajorAxis);
            orbit.MeanAnomalyAtEpoch = r.NextDouble() * (Math.PI * 2);
            orbit.LongitudeOfAscendingNode = r.NextDouble() * 360;

            orbit.Eccentricity = r.NextDouble() * (0.05 - -0.05) + -0.05;
            orbit.Epoch = 2000;
            
        }

        void GenerateConfig()
        {
            #region ConfigNodes
            ConfigNode NTemplate = new ConfigNode("Template");
            Config.AddConfigNode(NTemplate);

            ConfigNode NProperties = new ConfigNode("Properties");
            Config.AddConfigNode(NProperties);

            ConfigNode NOrbit = new ConfigNode("Orbit");
            Config.AddConfigNode(NOrbit);

            ConfigNode NScaledVerion = new ConfigNode("ScaledVersion");
            Config.AddConfigNode(NScaledVerion);

            //ScaledVersion Material
            ConfigNode NsvMaterial = new ConfigNode("Material");
            NScaledVerion.AddConfigNode(NsvMaterial);
            #endregion

            #region General values
            Config.AddValue("name", Name);
            Config.AddValue("cacheFile", "Audela/Galaxy/Cache/" + Name + ".bin");

            NTemplate.AddValue("name", template.Name.ToString());

            #region Properties
            NProperties.AddValue("radius", properties.Radius.ToString());
            NProperties.AddValue("description", properties.Description);
            #endregion

            #region Orbit
            NOrbit.AddValue("referenceBody", orbit.ReferenceBody.Name);
            NOrbit.AddValue("semiMajorAxis", orbit.SemiMajorAxis.ToString());
            NOrbit.AddValue("inclination", orbit.Inclination.ToString());
            NOrbit.AddValue("eccentricity", orbit.Eccentricity.ToString());
            NOrbit.AddValue("meanAnomalyAtEpoch", orbit.MeanAnomalyAtEpoch.ToString());
            NOrbit.AddValue("longitudeOfAscendingNode", orbit.LongitudeOfAscendingNode.ToString());
            NOrbit.AddValue("epoch", orbit.Epoch.ToString());
            NOrbit.AddValue("color", orbit.Color.ToString());
            #endregion

            #endregion

            #region Gas Case
            if (Type == BodyType.GasGiant)
            {
                #region ScaledVersion Material
                NsvMaterial.AddValue("texture", "Audela/Textures/Planets/GasGiant.png");
                NsvMaterial.AddValue("color", orbit.Color.ToString());

                ConfigNode NgradSVMAt = new ConfigNode("Gradient");
                NsvMaterial.AddConfigNode(NgradSVMAt);
                NgradSVMAt.AddValue("0.0", orbit.Color.RGBAString);
                NgradSVMAt.AddValue("0.3", "RGBA(175,140,0,255)");
                NgradSVMAt.AddValue("0.75", "RGBA(0,0,0,255)");
                NgradSVMAt.AddValue("1.0", "RGBA(0,0,0,255)");
                #endregion

                #region Atmosphere
                ConfigNode NAtmosphere = new ConfigNode("Atmosphere");
                Config.AddConfigNode(NAtmosphere);

                Palette atmoColor = new Palette();
                atmoColor.SetColorPalette(1 - orbit.Color.Color.Item1 / 2, 1 - orbit.Color.Color.Item2 / 2, 1 - orbit.Color.Color.Item3 / 2, 1);

                NAtmosphere.AddValue("lightColor", atmoColor.ToString());
                #endregion
            }
            #endregion

            #region Solid Case
            else
            {
                NTemplate.AddValue("removeAllPQSMods", "true");

                #region Properties
                NProperties.AddValue("geeASL", ManualG.ToString());
                #endregion

                #region ScaledVersion
                NsvMaterial.AddValue("texture", "Audela/Galaxy/Maps/" + Name + @"/_Color.png");
                NsvMaterial.AddValue("normals", "Audela/Galaxy/Maps/" + Name + @"/_Normal.png");
                #endregion

                #region PQS/Terrain
                if (Terrain != null)
                {
                    ConfigNode PQSNode = new ConfigNode("PQS");
                    Config.AddConfigNode(PQSNode);
                    PQSNode.AddConfigNode(Terrain.ParseTerrain());
                }
                #endregion
            }
            #endregion
        }

        List<PQS> GenerateTerrain()
        {
            List<PQS> mods = new List<PQS>();
            int id = 0;
            double KerbinRadii = properties.Radius / 600000;

            PQS continents = new VertexSimplexHeightAbsolute()
            {
                name = "AUD_CONTINENTS_ELA",
                seed = RandomBySeed.GetSeed(),
                index = id,

                deformity = (int)Math.Round(r.Next(0, 1500) * (1 / KerbinRadii)),
                frequency = Math.Round((r.NextDouble() * (0.5 - 0.25) + 0.25) * 1 / KerbinRadii, 2),
                octaves = 1,
                persistence = 1
            };
            continents.AddValuesToList();
            mods.Add(continents);
            id++;


            Palette cs = new Palette();
            cs.SetColorPalette(r.NextDouble(), r.NextDouble(), r.NextDouble(), 1);

            PQS JoliesTaches = new VertexSimplexNoiseColor()
            {
                name = "AUD_TESTCOLORS_ELA",
                seed = RandomBySeed.GetSeed(),
                index = id,

                blend = 1,
                colorStart = cs,
                colorEnd = orbit.Color,

                enabled = true,

                frequency = 2.5 * KerbinRadii,
                octaves = 1,
                persistence = 1
            };
            JoliesTaches.AddValuesToList();
            mods.Add(JoliesTaches);
            id++;


            return mods;
        }
    }
}
