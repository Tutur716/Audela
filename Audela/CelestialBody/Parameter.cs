using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody
{
    class Parameter
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string key;
        /// <summary>
        /// The value of the parameter
        /// </summary>
        public object value;
        /// <summary>
        /// The value of the parameter (string)
        /// </summary>
        public string svalue
        {
            get
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// Check if the parameter is a node
        /// </summary>
        public bool isNode = false;
    }
}
