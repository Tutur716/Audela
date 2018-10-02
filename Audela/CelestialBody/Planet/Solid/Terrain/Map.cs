// Using PlanetaryProcessor, made by Thomas P. 
// Big thank to him for this tool !
//
// Also, this means that PlanetaryProcessor.dll is under his licensing,
// see his GitHub repo for more details.

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ConfigNodeParser;
using PlanetaryProcessor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Audela.CelestialBody.Planet.Terrain
{
    class Map
    {
        public static string mapPath = GenerationSettings.path + @"/Galaxy/Maps/";
        public static string cachePath = GenerationSettings.path + @"/Galaxy/Cache/";

        public static void Save(string[] args, Body Body, List<Parameter> GeneratorParameters)
        {
            FileManagement.CreateSpecific(mapPath + Body.Name);
                                                        Run(args, Body, GeneratorParameters).Wait();
        }

        public static async Task Run(string[] args, Body Body, List<Parameter> Parameters)
        {
            NodeTree config = new NodeTree();

            foreach (Parameter p in Parameters)
            {   
                config.SetValue(p.key, p.svalue);
            }

            NodeTree mods = config.AddNode("Mods");

            foreach (PQS mod in Body.Terrain.ModList)
            {
                NodeTree modNode = mods.AddNode(mod.PQSType.ToString());

                foreach(Parameter modParam in mod.parameters)
                {
                    if (!modParam.isNode)
                    {
                        modNode.SetValue(modParam.key, modParam.svalue);
                    }

                    else
                    {
                        modNode.SetNode(modParam.key, ConfigNodeToNodeTree(modParam.value as ConfigNode));
                    }
                }
            }

            /*Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[=====MAP GENERATOR AKA PP=====]");
            Console.WriteLine(mods.ToString());
            Console.WriteLine("[=====MAP GENERATOR AKA PP END=====]");
            Console.ResetColor();*/

            using (Processor processor = await Processor.Create(GenerationSettings.rootPath))
            {
                Processor.TransformPath(cachePath + Body.Name + ".bin");
                Processor.EncodedTextureData data = await processor.GenerateMapsEncoded(config);

                await SaveStream(mapPath + Body.Name + @"/" + "_Color.png", data.Color);
                await SaveStream(mapPath + Body.Name + @"/" + "_Height.png", data.Height);
                await SaveStream(mapPath + Body.Name + @"/" + "_Normal.png", data.Normal);
            }
        }
        public static async Task SaveStream(String file, Stream stream)
        {
            using (stream)
            {
                using (FileStream fs = File.OpenWrite(file))
                {
                    await stream.CopyToAsync(fs);
                }
            }
        }

        /// <summary>
        /// Converts a ConfigNode into a NodeTree Wrapper [BY TMSP]
        /// </summary>
        static public NodeTree ConfigNodeToNodeTree(ConfigNode node)
        {
            NodeTree tree = new NodeTree();

            // Add the values to the new node
            foreach (KeyValuePair<String, String> value in node.values)
            {
                tree.SetValue(value.Key, value.Value);
            }

            // Add the nodes to the new node
            foreach (ConfigNode subnode in node.nodes)
            {
                tree.SetNode(subnode.name, ConfigNodeToNodeTree(subnode));
            }

            return tree;
        }
    }
}
