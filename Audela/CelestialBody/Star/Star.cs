using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigNodeParser;

namespace Audela.CelestialBody.Star
{
    class Star : Body
    {
        public Data.LuminosityTypes LuminosityClass = Data.LuminosityTypes.V;

        public Data.SpectralTypes SpectralType = Data.SpectralTypes.G;

        public double Temperature;

        public double Luminosity;

        public double SolarRadius;

        public double SolarLuminosity;

        public Body Generate(int ID)
        {
            GenerateProperties(ID);

            GenerateOrbit();

            GenerateConfig();

            return this;
        }

        /// <summary>
        /// Generates a ConfigNode with the form of Body { Blahblah { blah = bruh } } etc...
        /// </summary>
        ConfigNode GenerateConfig()
        {
            #region ConfigNodes
            ConfigNode NTemplate = new ConfigNode("Template");
            Config.AddConfigNode(NTemplate);

            ConfigNode NProperties = new ConfigNode("Properties");
            Config.AddConfigNode(NProperties);

            ConfigNode NOrbit = new ConfigNode("Orbit");
            Config.AddConfigNode(NOrbit);

            ConfigNode NScaledVerion = new ConfigNode("ScaledVersion");
            Config.AddConfigNode(NScaledVerion);

            //ScaledVersion Material
            ConfigNode NsvMaterial = new ConfigNode("Material");
            NScaledVerion.AddConfigNode(NsvMaterial);
        
            ConfigNode NsvLight = new ConfigNode("Light");
            NScaledVerion.AddConfigNode(NsvLight);

            #region ScaledVersion Light things
            ConfigNode NlgtBrightnessCurve = new ConfigNode("brightnessCurve");
            NsvLight.AddConfigNode(NlgtBrightnessCurve);

            ConfigNode NlgtIntensityCurve = new ConfigNode("IntensityCurve");
            NsvLight.AddConfigNode(NlgtIntensityCurve);

            ConfigNode NlgtScaledIntensityCurve = new ConfigNode("ScaledIntensityCurve");
            NsvLight.AddConfigNode(NlgtScaledIntensityCurve);

            ConfigNode NlgtIvaIntensityCurve = new ConfigNode("IvaIntensityCurve");
            NsvLight.AddConfigNode(NlgtIvaIntensityCurve);
            #endregion

            ConfigNode NsvCoronas = new ConfigNode("Coronas");
            NScaledVerion.AddConfigNode(NsvCoronas);

            #region Coronas
            ConfigNode NcorCorona1 = new ConfigNode("Corona");
            NsvCoronas.AddConfigNode(NcorCorona1);

            ConfigNode NcorCorona2 = new ConfigNode("Corona");
            NsvCoronas.AddConfigNode(NcorCorona2);

            ConfigNode NcorMat = new ConfigNode("Material");
            NcorCorona1.AddConfigNode(NcorMat);
            NcorCorona2.AddConfigNode(NcorMat);
            #endregion
            #endregion

            Config.AddValue("name", Name);

            NTemplate.AddValue("name", "Sun");

            #region Properties
            NProperties.AddValue("mass", properties.Mass.ToString());
            NProperties.AddValue("radius", properties.Radius.ToString());
            NProperties.AddValue("description", properties.Description);
            NProperties.AddValue("starLuminosity", (Luminosity / UsefulValues.SunLuminosity).ToString());
            #endregion

            #region Orbit
            NOrbit.AddValue("referenceBody", orbit.ReferenceBody.Name);
            NOrbit.AddValue("semiMajorAxis", orbit.SemiMajorAxis.ToString());
            NOrbit.AddValue("inclination", orbit.Inclination.ToString());
            NOrbit.AddValue("eccentricity", orbit.Eccentricity.ToString());
            NOrbit.AddValue("meanAnomalyAtEpoch", orbit.MeanAnomalyAtEpoch.ToString());
            NOrbit.AddValue("longitudeOfAscendingNode", orbit.LongitudeOfAscendingNode.ToString());
            NOrbit.AddValue("epoch", orbit.Epoch.ToString());
            NOrbit.AddValue("color", orbit.Color.ToString());
            #endregion

            #region ScaledVersion Material
            NsvMaterial.AddValue("emitColor0", orbit.Color.ToString());
            NsvMaterial.AddValue("emitColor1", GenerateBorderColor().ToString());
            NsvMaterial.AddValue("sunspotPower", "1.1");
            NsvMaterial.AddValue("sunspotColor", "0, 0, 0, 1");
            NsvMaterial.AddValue("rimColor", orbit.Color.ToString());
            NsvMaterial.AddValue("rimPower", "1.29");
            NsvMaterial.AddValue("rimBlend", "2.5");
            #endregion

            #region ScaledVersion Light
            NsvLight.AddValue("sunFlare", "Audela/Textures/Flares/Star Black and White - No Flare/stockwhite.unity3d:stock_white");
            NsvLight.AddValue("sunLensFlareColor", orbit.Color.ToString());
            NsvLight.AddValue("givesOffLight", "true");
            NsvLight.AddValue("sunlightColor", orbit.Color.ToString());
            NsvLight.AddValue("scaledSunlightColor", orbit.Color.ToString());
            NsvLight.AddValue("IVASunColor", orbit.Color.ToString());
            NsvLight.AddValue("luminosity", (1360 * (Luminosity / UsefulValues.SunLuminosity)).ToString());
           
            //BrightnessCurve (Sunflare size)
            //that's old generator's calculation, i am lazy to make a correct one, + this one works nice.
            NlgtBrightnessCurve.AddValue("key", "-0.01573471 0.04 1.706627 " + SolarRadius);
            NlgtBrightnessCurve.AddValue("key", "0.5 " + 0.56 * SolarRadius * 2.4 + " " + SolarRadius + " " + SolarRadius);
            NlgtBrightnessCurve.AddValue("key", "0.9 " + 1 * SolarRadius * 2.4 + " " + SolarRadius + " " + SolarRadius);
            NlgtBrightnessCurve.AddValue("key", "10  " + 9 * SolarRadius * 2.4 + " 0 0");

            NlgtIntensityCurve.AddValue("key", "0 0.9 0 0");
            NlgtIntensityCurve.AddValue("key", 1.35E+10 * SolarLuminosity + " 0.9 0 -1E-11");
            NlgtIntensityCurve.AddValue("key", 1E+11 * SolarLuminosity + " 0.3 -3E-12 -4E-12");
            NlgtIntensityCurve.AddValue("key", 2.82E+11 * SolarLuminosity + " 0 0 0");

            NlgtScaledIntensityCurve.AddValue("key", "0 0.9 0 0");
            NlgtScaledIntensityCurve.AddValue("key", 2E+07 * SolarLuminosity + " 0.225 0 -1E-11");
            NlgtScaledIntensityCurve.AddValue("key", 1E+9 * SolarLuminosity + " 0.3 -3E-12 -4E-12");
            NlgtScaledIntensityCurve.AddValue("key", 2.82E+9 * SolarLuminosity + " 0 0 0");

            NlgtIvaIntensityCurve.AddValue("key", "0 0.9 0 0");
            NlgtIvaIntensityCurve.AddValue("key", 1.359984E+10 * SolarLuminosity + " 0.9 0 -1E-11");
            NlgtIvaIntensityCurve.AddValue("key", 1E+11 * SolarLuminosity + " 0.3 -3E-12 -4E-12");
            NlgtIvaIntensityCurve.AddValue("key", 2.82E+11 * SolarLuminosity + " 0 0 0");
            #endregion

            #region ScaledVersion Coronas
            NcorCorona1.AddValue("scaledSpeed", "0.007");
            NcorCorona1.AddValue("scaleLimitX", "1");
            NcorCorona1.AddValue("scaleLimitY", "1");
            NcorCorona1.AddValue("updateInterval", "5");
            NcorCorona1.AddValue("speed", "-1");
            NcorCorona1.AddValue("rotation", "0");

            NcorCorona2.AddValue("scaledSpeed", "0.009");
            NcorCorona2.AddValue("scaleLimitX", "1");
            NcorCorona2.AddValue("scaleLimitY", "1");
            NcorCorona2.AddValue("updateInterval", "5");
            NcorCorona2.AddValue("speed", "1");
            NcorCorona2.AddValue("rotation", "0");

            NcorMat.AddValue("texture", "Audela/Textures/Coronas/" + SpectralType);
            NcorMat.AddValue("inverseFade", "2.553731");


            #endregion
            return Config;
        }

        void GenerateProperties(int ID)
        {
            Name = "Star " + ID;
            Type = BodyType.Star;
            SpectralType = Data.Fraction.SpectralTypeDistribution.RandomType(LuminosityClass);
            properties.Mass = Data.SpectralTypesData.RandomMass(SpectralType, LuminosityClass);
            properties.Mass /= 1e4;
            Temperature = Data.SpectralTypesData.RandomTemperature(SpectralType);
            orbit.Color = GenerateColor(Temperature);
            properties.Radius = Data.SpectralTypesData.RandomRadius(SpectralType, LuminosityClass);
            //properties.Radius *= 1000;
            Luminosity = Math.Pow(properties.Radius / 1000, 2) * Math.Pow(Temperature, 4);
            orbit.ReferenceBody = new Body();
            orbit.ReferenceBody.Name = "Sun";
            properties.Description = "Lum Class=" + LuminosityClass + ", Spectral Type=" + SpectralType;
            SolarRadius = properties.Radius / (UsefulValues.SunRadius * 1000);
            SolarLuminosity = Luminosity / UsefulValues.SunLuminosity;

            //Unrealistic, but permits to have light everywhere
            if (SolarLuminosity < 1) SolarLuminosity = 1;

            ManualMass = properties.Mass;
            ManualRoche = RocheLimit;
            ManualSOI = SphereOfInfluence;
        }

        void GenerateOrbit()
        {
            orbit.Inclination = r.NextDouble() * 360;
            orbit.SemiMajorAxis = (r.NextDouble() * GenerationSettings.GalaxySize) * (UsefulValues.LightYear * 1000);
            orbit.MeanAnomalyAtEpoch = r.NextDouble() * (Math.PI * 2);
            orbit.LongitudeOfAscendingNode = r.NextDouble() * 360;

            orbit.Eccentricity = 0;
            orbit.Epoch = 2000;
        }

        /// <summary>
        /// Calculates star's color by its temperature
        /// </summary>
        Palette GenerateColor(double Temperature)
        //took and converted from http://www.tannerhelland.com/4435/convert-temperature-rgb-algorithm-code/
        {
            double Red;
            double Green;
            double Blue;

            double[] rgba;

            Temperature = Temperature / 100;

            if (Temperature <= 66)
            {
                Red = 255;
            }
            else
            {
                Red = Temperature - 60;
                Red = 329.698727466 * Math.Pow(Red, -0.1332047592);
                if (Red < 0)
                {
                    Red = 0;
                }
                if (Red > 255)
                {
                    Red = 255;
                }
            }

            if (Temperature <= 66)
            {
                Green = Temperature;
                Green = 99.4708025861 * Math.Log(Green) - 161.1195681661;
                if (Green < 0)
                {
                    Green = 0;
                }
                if (Green > 255)
                {
                    Green = 255;
                }
            }
            else
            {
                Green = Temperature - 60;
                Green = 288.1221695283 * Math.Log(Green, -0.0755148492);
                if (Green < 0)
                {
                    Green = 0;
                }
                if (Green > 255)
                {
                    Green = 255;
                }
            }

            if (Temperature >= 66)
            {
                Blue = 255;
            }
            else
            {
                if (Temperature <= 19)
                {
                    Blue = 0;
                }
                else
                {
                    Blue = Temperature - 10;
                    Blue = 138.5177312231 * Math.Log(Blue) - 305.0447927307;
                    if (Blue < 0)
                    {
                        Blue = 0;
                    }
                    if (Blue > 255)
                    {
                        Blue = 255;
                    }
                }
            }

            Red = Math.Round(Red);
            Green = Math.Round(Green);
            Blue = Math.Round(Blue);

            if (double.IsNaN(Red))
                Red = 255;

            if (double.IsNaN(Green))
                Green = 255;

            if (double.IsNaN(Blue))
                Blue = 255;

            rgba = new double[]
            {
                Red,
                Green,
                Blue,
                255
            };

            Palette rgbaPalette = new Palette();
            rgbaPalette.SetRGBAPalette((int)rgba[0], (int)rgba[1], (int)rgba[2], (int)rgba[3]);

            return rgbaPalette;
        }

        Palette GenerateBorderColor()
        {
            Palette starContours = orbit.Color;
            double contoursMult = 1.75;

            //Red
            double contourR = starContours.RGBAArray[0] * contoursMult;

            if (contourR > 255)
            {
                contourR = 255;
            }
            else
            {
                contourR = Math.Round(contourR);
            }

            starContours.RGBAArray[0] = contourR;

            //Green
            double contourG = starContours.RGBAArray[1] * contoursMult;

            if (contourG > 255)
            {
                contourG = 255;
            }
            else
            {
                contourG = Math.Round(contourG);
            }

            starContours.RGBAArray[1] = contourG;

            //Blue
            double contourB = starContours.RGBAArray[2] * contoursMult;

            if (contourB > 255)
            {
                contourB = 255;
            }
            else
            {
                contourB = Math.Round(contourB);
            }

            starContours.RGBAArray[2] = contourB;

            return starContours;
        }
    }
}
