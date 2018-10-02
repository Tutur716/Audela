using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Terrain.Mods
{
    class VertexSimplexHeightAbsolute : PQS
    {
        public override PQSEnum PQSType => PQSEnum.VertexSimplexHeightAbsolute;

        public int deformity = 500;
        public double frequency = 1;
        public int octaves = 1;
        public int persistence = 1;
        public int seed = 0;

        //ConfigNodeParser.ConfigNode cn = new ConfigNodeParser.ConfigNode("NODETEST");
        //ConfigNodeParser.ConfigNode cn1 = new ConfigNodeParser.ConfigNode("NODETEST2");

        public override List<Parameter> AddValuesToList()
        {
            AddDefaultValuesToList();

            parameters.Add(AddParam("deformity", deformity));
            parameters.Add(AddParam("frequency", frequency));
            parameters.Add(AddParam("octaves", octaves));
            parameters.Add(AddParam("persistence", persistence));
            parameters.Add(AddParam("seed", seed));

            //cn.AddValue("cn t val 1", "1");
            //cn.AddValue()
            //cn.AddConfigNode(cn1);

            return base.AddValuesToList();
        }
    }
}
