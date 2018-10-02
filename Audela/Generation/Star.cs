using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Audela.Generation
{
    /// <summary>
    /// Basic componants a star has
    /// </summary>
    class Star : Body
    {
        public override BodyType BodyType => BodyType.Star;

        public Data.LuminosityTypes luminosityClass = Data.LuminosityTypes.V;

        public Star Random()
        {
            #region General Properties
            //luminosityType = Data.Fraction.LuminosityTypesDistribution.RandomType();
            SpectralType = Data.Fraction.SpectralTypeDistribution.RandomType(luminosityClass);
            Temperature = Data.SpectralTypesData.RandomTemperature(SpectralType);
            Radius = Data.SpectralTypesData.RandomRadius(SpectralType, luminosityClass);
            Mass = Data.SpectralTypesData.RandomMass(SpectralType, luminosityClass);
            Color = ColorGenerator(Temperature);
            Luminosity = Math.Pow(Radius/1000, 2) * Math.Pow(Temperature, 4);
            #endregion

            #region Orbital Properties
            SemiMajorAxis = RandomBySeed.GetRandom().NextDouble() * Galaxy.Size;
            MeanAnomalyAtEpoch = RandomBySeed.GetRandom().NextDouble() * (Math.PI * 2);
            #endregion
            return this;
        }

        /// <summary>
        /// Calculates star's color by its temperature
        /// </summary>
        double[] ColorGenerator(double Temperature)
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

            return rgba;
        }
    }
}
