using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;

namespace Audela.CelestialBody.Planet.Terrain.Mods
{
    class HeightColorMap : PQS
    {
        public override PQSEnum PQSType => PQSEnum.HeightColorMap;

        public double blend = 1;

        public ConfigNode LandClasses = new ConfigNode("LandClasses");

        public List<HeightColorMapLandClass> Classes = new List<HeightColorMapLandClass>();

        public HeightColorMapLandClass newClass(string Name, Palette Color, double AltitudeStart, double AltitudeEnd, bool LerpToNext, bool Delete)
        {  
            HeightColorMapLandClass h = new HeightColorMapLandClass()
            {
                name = Name,
                color = Color,
                altitudeStart = AltitudeStart,
                altitudeEnd = AltitudeEnd,
                lerpToNext = LerpToNext,
                delete = Delete
            };

            Classes.Add(h);

            return h;
        }

        public override List<Parameter> AddValuesToList()
        {
            AddDefaultValuesToList();

            foreach(HeightColorMapLandClass h in Classes)
            {
                LandClasses.AddConfigNode(h.ClassNode);
            }

            parameters.Add(AddParam("blend", blend));

            parameters.Add(AddParam(LandClasses, "LandClasses"));

            return base.AddValuesToList();
        }
    }

    class HeightColorMapLandClass
    {
        public List<Parameter> Class
        {
            get
            {
                List<Parameter> pl = new List<Parameter>();

                pl.Add(AddParam("name", name));
                pl.Add(AddParam("color", color));
                pl.Add(AddParam("altitudeStart", altitudeStart));
                pl.Add(AddParam("altitudeEnd", altitudeEnd));
                pl.Add(AddParam("lerpToNext", lerpToNext));
                pl.Add(AddParam("delete", delete));

                return pl;
            }
        }

        public ConfigNode ClassNode
        {
            get
            {
                ConfigNode cn = new ConfigNode("Class");

                foreach(Parameter p in Class)
                {
                    cn.AddValue(p.key, p.svalue);
                }

                return cn;
            }
        }

        public string name = "Class";

        public Palette color = new Palette();

        public double altitudeStart = 0;

        public double altitudeEnd = 1;

        public bool lerpToNext = false;

        public bool delete = false;

        /// <summary>
        /// Adds a non node parameter
        /// </summary>
        public Parameter AddParam(string key, object value)
        {
            return new Parameter() { key = key, value = value };
        }
    }
}
