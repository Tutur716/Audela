using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Terrain.Mods
{
    class VertexSimplexNoiseColor : PQS
    {
        public override PQSEnum PQSType => PQSEnum.VertexSimplexNoiseColor;

        /// <summary>
        /// Color blending from 0 to 1
        /// </summary>
        public double blend = 1;
        public double frequency = 1;
        public int octaves = 1;
        public double persistence = 1;
        public int seed = 0;

        public Palette colorStart;
        public Palette colorEnd;

        public override List<Parameter> AddValuesToList()
        {
            AddDefaultValuesToList();

            parameters.Add(AddParam("blend", blend));
            parameters.Add(AddParam("frequency", frequency));
            parameters.Add(AddParam("octaves", octaves));
            parameters.Add(AddParam("persistence", persistence));
            parameters.Add(AddParam("seed", seed));

            parameters.Add(AddParam("colorStart", colorStart));
            parameters.Add(AddParam("colorEnd", colorEnd));

            return base.AddValuesToList();
        }
    }
}
