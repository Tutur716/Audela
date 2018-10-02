using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;

namespace Audela.CelestialBody
{
    class Body
    {
        public Random r = RandomBySeed.GetRandom();
        public BodyType Type = BodyType.GasGiant;
        public Planet.Terrain.TerrainParser Terrain;
        public ConfigNode Config = new ConfigNode("Body");

        /// <summary>
        /// The name of the body, must be unique
        /// </summary>
        public string Name { get; set; } = "Name of the Body is not set!";

        #region Template
        /// <summary>
        /// Template structure
        /// </summary>
        public struct Template
        {
            public Templates Name;
            public bool RemoveAllPQSMods, RemoveOcean, RemoveAtmosphere;
        }
        public Template template;
        #endregion

        #region Properties
        /// <summary>
        /// Properties structure
        /// </summary>
        public struct Properties
        {
            public double Radius, GeeASL, Mass;
            public string Description;
        }
        public Properties properties;
        #endregion

        #region Orbit
        /// <summary>
        /// Orbit structure
        /// </summary>
        public struct Orbit
        {
            public double SemiMajorAxis, Inclination, Eccentricity, MeanAnomalyAtEpoch, LongitudeOfAscendingNode, Epoch;
            public Body ReferenceBody;
            public Palette Color;
        }
        public Orbit orbit;
        #endregion

        public double RocheLimit
        {
            get
            {
                return properties.Radius * Math.Pow(2 * (orbit.ReferenceBody.properties.Mass / properties.Mass), 1 / 3);
            }
        }

        /// <summary>
        /// The Sphere of Influence in meters
        /// </summary>
        public double SphereOfInfluence
        {
            get
            {
                return orbit.SemiMajorAxis * Math.Pow(properties.Mass / orbit.ReferenceBody.properties.Mass, 0.4);
            }
        }

        public double ManualG;
        public double ManualSOI;
        public double ManualMass;
        public double ManualRoche;
    }
}
