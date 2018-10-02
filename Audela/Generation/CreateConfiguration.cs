using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;
using System.IO;
namespace Audela.Generation
{
    static class CreateConfiguration
    {
        public static void CreateAndSave(Body B)
        {
            ConfigNode conf = new ConfigNode();

            #region General

            #region Initialization
            ConfigNode ModuleManagerNode = new ConfigNode("@Kopernicus:FOR[Audela]");
            ConfigNode BodyNode = new ConfigNode("Body");
            ConfigNode TemplateNode = new ConfigNode("Template");
            ConfigNode PropertiesNode = new ConfigNode("Properties");
            ConfigNode OrbitNode = new ConfigNode("Orbit");
            ConfigNode ScaledVersionNode = new ConfigNode("ScaledVersion");
            ConfigNode ScaledVersionMaterialNode = new ConfigNode("Material");

            conf.AddConfigNode(ModuleManagerNode);
            ModuleManagerNode.AddConfigNode(BodyNode);
            BodyNode.AddConfigNode(TemplateNode);
            BodyNode.AddConfigNode(PropertiesNode);
            BodyNode.AddConfigNode(OrbitNode);
            BodyNode.AddConfigNode(ScaledVersionNode);
            ScaledVersionNode.AddConfigNode(ScaledVersionMaterialNode);

            string colorString = "RGBA(" + B.Color[0] + ", " + B.Color[1] + ", " + B.Color[2] + ", " + B.Color[3] + ")";
            #endregion

            #region Body
            BodyNode.AddValue("name", B.Name);
            #endregion

            #region Properties
            PropertiesNode.AddValue("radius", B.Radius.ToString());
            PropertiesNode.AddValue("mass", (B.Mass * 1e-4).ToString());
            PropertiesNode.AddValue("description", B.Description);
            #endregion

            #region Orbit
            OrbitNode.AddValue("referenceBody", B.ReferenceBody);
            OrbitNode.AddValue("inclination", B.Inclination.ToString());
            OrbitNode.AddValue("eccentricity", B.Eccentricity.ToString());
            OrbitNode.AddValue("semiMajorAxis", (B.SemiMajorAxis * Galaxy.LightYear).ToString());
            OrbitNode.AddValue("meanAnomalyAtEpoch", B.MeanAnomalyAtEpoch.ToString());
            OrbitNode.AddValue("longitudeOfAscendingNode", B.LongitudeOfAscendingNode.ToString());
            OrbitNode.AddValue("epoch", B.Epoch.ToString());
            OrbitNode.AddValue("color", colorString);
            #endregion

            #endregion

            #region Star
            if (B.BodyType == BodyType.Star)
            {
                #region Template
                TemplateNode.AddValue("name", "Sun");
                #endregion

                #region Properties
                PropertiesNode.AddValue("starLuminosity", (B.Luminosity/Body.SunLuminosity).ToString());
                #endregion

                #region ScaledVersion Material
                ScaledVersionMaterialNode.AddValue("emitColor0", colorString);
                ScaledVersionMaterialNode.AddValue("emitColor1", colorString);
                ScaledVersionMaterialNode.AddValue("sunspotPower", "1.1");
                ScaledVersionMaterialNode.AddValue("sunspotColor", "0,0,0,1");
                ScaledVersionMaterialNode.AddValue("rimColor", colorString);
                ScaledVersionMaterialNode.AddValue("rimPower", "1");
                ScaledVersionMaterialNode.AddValue("rimBlend", "0.6");
                #endregion

                #region ScaledVersion Light
                ConfigNode SVLightNode = new ConfigNode("Light");
                ScaledVersionNode.AddConfigNode(SVLightNode);
                SVLightNode.AddValue("sunFlare", "Audela/Patching/stockwhite.unity3d:stock_white");
                SVLightNode.AddValue("sunLensFlareColor", colorString);
                SVLightNode.AddValue("givesOffLight", "true");
                SVLightNode.AddValue("sunlightColor", colorString);
                SVLightNode.AddValue("scaledSunlightColor", colorString);
                SVLightNode.AddValue("IVASunColor", colorString);
                SVLightNode.AddValue("luminosity", (1360 * (B.Luminosity / Body.SunLuminosity)).ToString());

                #region BrightnessCurve
                //BrightnessCurve (Sunflare size)
                //that's old generator's calculation, i am lazy to make a correct one, + this one works nice.
                double kRad;
                kRad = B.Radius * 6.957e+8; //Rad in meters
                kRad /= 3; //KSP Sized
                kRad /= 261600000; //Obtains percentage of kerbol's radius

                ConfigNode SVLightBCNode = new ConfigNode("brightnessCurve");
                SVLightNode.AddConfigNode(SVLightBCNode);
                SVLightBCNode.AddValue("key", "-0.01573471 0.04 1.706627 0.806627");
                SVLightBCNode.AddValue("key", (kRad /= 0.5).ToString() + " 0.5 0.56 1 1");
                SVLightBCNode.AddValue("key", (kRad /= 0.9).ToString() + " 0.9 1 1 1");
                SVLightBCNode.AddValue("key", (kRad /= 10).ToString() + " 10 9 0 0");
                #endregion


                #endregion

                #region ScaledVersion Coronas
                ConfigNode SVCoronasNode = new ConfigNode("Coronas");
                ScaledVersionNode.AddConfigNode(SVCoronasNode);

                #region Corona 1
                ConfigNode Corona1Node = new ConfigNode("Corona");
                SVCoronasNode.AddConfigNode(Corona1Node);

                Corona1Node.AddValue("scaledSpeed", "0.007");
                Corona1Node.AddValue("scaleLimitY", "1");
                Corona1Node.AddValue("scaleLimitX", "1");
                Corona1Node.AddValue("updateInterval", "5");
                Corona1Node.AddValue("speed", "-1");
                Corona1Node.AddValue("rotation", "0");
                #endregion

                #region Corona 2
                ConfigNode Corona2Node = new ConfigNode("Corona");
                SVCoronasNode.AddConfigNode(Corona2Node);

                Corona2Node.AddValue("scaledSpeed", "0.009");
                Corona2Node.AddValue("scaleLimitY", "1");
                Corona2Node.AddValue("scaleLimitX", "1");
                Corona2Node.AddValue("updateInterval", "5");
                Corona2Node.AddValue("speed", "1");
                Corona2Node.AddValue("rotation", "0");
                #endregion

                #region Coronas Material
                ConfigNode CoronasMaterialNode = new ConfigNode("Material");
                Corona1Node.AddConfigNode(CoronasMaterialNode);
                Corona2Node.AddConfigNode(CoronasMaterialNode);

                CoronasMaterialNode.AddValue("texture", "Audela/Textures/" + B.SpectralType);
                CoronasMaterialNode.AddValue("inverseFade", "2.553731");
                #endregion

                #endregion
            }
            #endregion

            //Saves
            conf.Save(GenerationSettings.path + "/Stars/" + B.Name + ".cfg");
        }
    }
}
