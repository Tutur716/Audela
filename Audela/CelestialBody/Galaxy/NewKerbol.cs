using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigNodeParser;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Galaxy
{
    class NewKerbol : Body
    {
        public Body Generate()
        {
            GenerateOrbit();
            GenerateConfig();

            return this;
        }

        ConfigNode GenerateConfig()
        {
            ConfigNode NMM = new ConfigNode("@Kopernucus:AFTER[Audela]");
            Config.AddConfigNode(NMM);

            ConfigNode NBody = new ConfigNode("@Body[NewSun]");
            NMM.AddConfigNode(NBody);

            ConfigNode NOrbit = new ConfigNode("@Orbit");
            NBody.AddConfigNode(NOrbit);

            NOrbit.AddValue("@semiMajorAxis", orbit.SemiMajorAxis.ToString());
            NOrbit.AddValue("@inclination", orbit.Inclination.ToString());
            NOrbit.AddValue("@meanAnomalyAtEpoch", orbit.MeanAnomalyAtEpoch.ToString());
            NOrbit.AddValue("@longitudeOfAscendingNode", orbit.LongitudeOfAscendingNode.ToString());

            return Config;
        }

        void GenerateOrbit()
        {
            orbit.Inclination = r.NextDouble() * 360;
            orbit.SemiMajorAxis = (r.NextDouble() * GenerationSettings.GalaxySize) * (UsefulValues.LightYear * 1000);
            orbit.MeanAnomalyAtEpoch = r.NextDouble() * (Math.PI * 2);
            orbit.LongitudeOfAscendingNode = r.NextDouble() * 360;
        }
    }
}
