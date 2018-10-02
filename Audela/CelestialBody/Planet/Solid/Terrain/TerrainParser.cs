using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;

namespace Audela.CelestialBody.Planet.Terrain
{
    class TerrainParser
    {
        private ConfigNode ModsCN = new ConfigNode("Mods"); 

        /// <summary>
        /// List of mods
        /// </summary>
        public List<PQS> ModList = new List<PQS>();

        /// <summary>
        /// List of parsed mods
        /// </summary>
        public List<ConfigNode> ParsedMods
        {
            get
            {
                List<ConfigNode> cnl = new List<ConfigNode>();

                //Console.ForegroundColor = ConsoleColor.Cyan;
                //Console.WriteLine("[=====TERRAIN PARSER=====]");
                foreach(PQS mod in ModList)
                {
                    //Console.WriteLine(mod.PQSType.ToString());
                    ConfigNode cn = new ConfigNode(mod.PQSType.ToString());
                    //Console.WriteLine("{");
                    foreach(Parameter p in mod.parameters)
                    {
                        //If it isn't a node
                        if (!p.isNode)
                        {
                            //If it isn't a palette
                            if(p.value.GetType() != typeof(Palette))
                            {
                               // Console.WriteLine("    {0} = {1}", p.key, p.value);
                                cn.AddValue(p.key, p.svalue);
                            }

                            else
                            {
                                cn.AddValue(p.key, ((Palette)p.value).ToString());
                            }
                        }

                        else
                        {
                            ConfigNode n = cn.AddConfigNode((ConfigNode)p.value);
                        }
                    }
                    //Console.WriteLine("}");
                    cnl.Add(cn);
                }
                //Console.WriteLine("[=====TERRAIN PARSER END=====]");
                //Console.ResetColor();

                return cnl;
            }
        }

        /// <summary>
        /// Adds a PQS Mod in the list
        /// </summary>
        /// <param name="PQSMod"></param>
        /// <returns></returns>
        public List<PQS> AddMod(PQS PQSMod)
        {
            ModList.Add(PQSMod);
            return ModList;
        }  

        /// <summary>
        /// Parse the terrain
        /// </summary>
        public ConfigNode ParseTerrain()
        {
            foreach(ConfigNode cn in ParsedMods)
            {
                ModsCN.AddConfigNode(cn);
            }

            return ModsCN;
        }
    }
}
