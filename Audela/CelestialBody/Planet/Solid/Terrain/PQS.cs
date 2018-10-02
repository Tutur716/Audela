using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Terrain
{
    class PQS
    {
        public List<Parameter> parameters = new List<Parameter>();

        public virtual PQSEnum PQSType => PQSEnum.VertexSimplexHeightAbsolute;

        public string name = "Name of the PQS is not defined !";

        public bool enabled = true;

        public int index = 0;

        public int order
        {
            get
            {
                return index;
            }
        }

        /// <summary>
        /// Adds a parameter in the parameter list
        /// </summary>
        public List<Parameter> AddDefaultValuesToList()
        {
            parameters.Add(AddParam("name", name));

            parameters.Add(AddParam("enabled", enabled));

            parameters.Add(AddParam("index", index));

            parameters.Add(AddParam("order", order));

            return parameters;
        }
        /// <summary>
        /// Adds a non node parameter
        /// </summary>
        public Parameter AddParam(string key, object value)
        {
            return new Parameter() { key = key, value = value };
        }
        /// <summary>
        /// Adds a node paramater
        /// </summary>
        public Parameter AddParam(ConfigNodeParser.ConfigNode ConfigNode)
        {
            return new Parameter() { key = "Generic Config Node", value = ConfigNode, isNode = true };
        }
        /// <summary>
        /// Adds a node paramater with a name
        /// </summary>
        public Parameter AddParam(ConfigNodeParser.ConfigNode ConfigNode, string Name)
        {
            return new Parameter() { key = Name, value = ConfigNode, isNode = true };
        }

        public virtual List<Parameter> AddValuesToList()
        {
            return parameters;
        }
    }
}
