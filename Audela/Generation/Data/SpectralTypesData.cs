using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using st = Audela.Generation.Data.SpectralTypes;
using lt = Audela.Generation.Data.LuminosityTypes;

namespace Audela.Generation.Data
{
    /// <summary>
    /// Temperatures definining the Temperature Class of a star (O,G,M etc)
    /// </summary>
    static class SpectralTypesData
    {
        #region Temperature
        /// <summary>
        /// Get the minimal temperature of a Temperature Class (Kelvin)
        /// </summary>
        /// <param name="TemperatureClass"></param>
        /// <returns></returns>
        static public double GetMinTemperature(st SpectralType)
        {
            double d = 0;

            switch(SpectralType)
            {
                case st.O:
                    d = 30000;
                    break;

                case st.B:
                    d = 10000;
                    break;

                case st.A:
                    d = 7500;
                    break;

                case st.F:
                    d = 6000;
                    break;

                case st.G:
                    d = 5000;
                    break;

                case st.K:
                    d = 3500;
                    break;

                case st.M:
                    d = 2000;
                    break;

                case st.L:
                    d = 1200;
                    break;

                case st.T:
                    d = 750;
                    break;

                case st.Y:
                    d = 500;
                    break;
            }

            return d;
        }

        /// <summary>
        /// Get the maximal temperature of a Temperature Class (Kelvin)
        /// </summary>
        /// <param name="TemperatureClass"></param>
        /// <returns></returns>
        static public double GetMaxTemperature(st SpectralType)
        {
            double d = 0;

            switch (SpectralType)
            {
                case st.O:
                    d = 50000;
                    break;

                case st.B:
                    d = 24999;
                    break;

                case st.A:
                    d = 9999;
                    break;

                case st.F:
                    d = 7499;
                    break;

                case st.G:
                    d = 5999;
                    break;

                case st.K:
                    d = 4999;
                    break;

                case st.M:
                    d = 3499;
                    break;

                case st.L:
                    d = 1999;
                    break;

                case st.T:
                    d = 1199;
                    break;

                case st.Y:
                    d = 750;
                    break;
            }

            return d;
        }

        /// <summary>
        /// Returns random temperature between the min and the max temperature of a Temperature Class. (Kelvin)
        /// </summary>
        /// <returns></returns>
        static public double RandomTemperature(st SpectralType)
        {
            Random r = RandomBySeed.GetRandom();
            double min = GetMinTemperature(SpectralType);
            double max = GetMaxTemperature(SpectralType);
            return r.NextDouble() * (max - min) + min;
        }
        #endregion

        #region Radius
        /// <summary>
        /// Get the minimal radius for a Spectral Type (Solar Radii)
        /// </summary>
        /// <param name="SpectralType"></param>
        /// <returns></returns>
        static public double GetMinRadius(st SpectralType, lt LuminosityType)
        {
            double d = 0;
            switch(LuminosityType)
            {
                #region Main Sequence
                case lt.V:
                    switch (SpectralType)
                    {
                        case st.O:
                            d = 8;
                            break;

                        case st.B:
                            d = 3;
                            break;

                        case st.A:
                            d = 1.6;
                            break;

                        case st.F:
                            d = 1.1;
                            break;

                        case st.G:
                            d = 0.85;
                            break;

                        case st.K:
                            d = 0.6;
                            break;

                        case st.M:
                            d = 0.14;
                            break;
                    }
                    break;
                #endregion
            }

            return d;
        }

        /// <summary>
        /// Get the maximal radius for a Spectral Type (Solar Radii)
        /// </summary>
        /// <param name="SpectralType"></param>
        /// <returns></returns>
        static public double GetMaxRadius(st SpectralType, lt LuminosityType)
        {
            double d = 0;
            switch (LuminosityType)
            {
                #region Main Sequence
                case lt.V:
                    switch (SpectralType)
                    {
                        case st.O:
                            d = 15;
                            break;

                        case st.B:
                            d = 8;
                            break;

                        case st.A:
                            d = 3;
                            break;

                        case st.F:
                            d = 1.6;
                            break;

                        case st.G:
                            d = 1.1;
                            break;

                        case st.K:
                            d = 0.85;
                            break;

                        case st.M:
                            d = 0.6;
                            break;
                    }
                    break;
                    #endregion
            }

            return d;
        }

        /// <summary>
        /// Get a random radius for a Spectral Type in meters
        /// </summary>
        static public double RandomRadius(st SpectralType, lt LuminosityType)
        {
            Random r = RandomBySeed.GetRandom();
            double min = GetMinRadius(SpectralType, LuminosityType);
            double max = GetMaxRadius(SpectralType, LuminosityType);
            return (r.NextDouble() * (max - min) + min) * Body.solarRadius;
        }
        #endregion

        #region Mass
        /// <summary>
        /// Get the minimal mass for a Spectral Type (Solar Masses)
        /// </summary>
        static public double GetMinMass(st SpectralType, lt LuminosityType)
        {
            double d = 0;
            switch (LuminosityType)
            {
                #region Main Sequence
                case lt.V:
                    switch (SpectralType)
                    {
                        case st.O:
                            d = 18;
                            break;

                        case st.B:
                            d = 3.5;
                            break;

                        case st.A:
                            d = 2;
                            break;

                        case st.F:
                            d = 1.1;
                            break;

                        case st.G:
                            d = 0.85;
                            break;

                        case st.K:
                            d = 0.5;
                            break;

                        case st.M:
                            d = 0.05;
                            break;
                    }
                    break;
                    #endregion
            }

            return d;
        }

        /// <summary>
        /// Get the maximal mass for a Spectral Type (Solar Masses)
        /// </summary>
        /// <param name="SpectralType"></param>
        /// <returns></returns>
        static public double GetMaxMass(st SpectralType, lt LuminosityType)
        {
            double d = 0;
            switch (LuminosityType)
            {
                #region Main Sequence
                case lt.V:
                    switch (SpectralType)
                    {
                        case st.O:
                            d = 120;
                            break;

                        case st.B:
                            d = 18;
                            break;

                        case st.A:
                            d = 3.5;
                            break;

                        case st.F:
                            d = 2;
                            break;

                        case st.G:
                            d = 1.1;
                            break;

                        case st.K:
                            d = 0.85;
                            break;

                        case st.M:
                            d = 0.5;
                            break;
                    }
                    break;
                    #endregion
            }

            return d;
        }

        /// <summary>
        /// Get a random mass in grams
        /// </summary>
        static public double RandomMass(st SpectralType, lt LuminosityType)
        {
            Random r = RandomBySeed.GetRandom();
            double min = GetMinMass(SpectralType, LuminosityType);
            double max = GetMaxMass(SpectralType, LuminosityType);
            return (r.NextDouble() * (max - min) + min) * Body.solarMass;
        }
        #endregion
    }
}
