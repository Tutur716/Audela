using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Terrain.Mods
{
    class VertexHeightOffset : PQS
    {
        public override PQSEnum PQSType => PQSEnum.VertexHeightOffset;

        public int offset;

        public override List<Parameter> AddValuesToList()
        {
            AddDefaultValuesToList();

            parameters.Add(AddParam("offset", offset));

            return base.AddValuesToList();
        }
    }
}

