using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela
{
    class UsefulValues
    {
        #region Real Life
        /// <summary>
        /// The Gravitational Constant in m^3/kg/s^2
        /// </summary>
        public const double G = 6.670831e-11;

        /// <summary>
        /// 1 g in Newton
        /// </summary>
        public const double OneGee = 9.80665;

        /// <summary>
        /// The volumic mass of the Water in kg/m^3
        /// </summary>
        public const int WaterVolumicMass = 997;

        /// <summary>
        /// The mass of the Sun (IRL) in kg
        /// </summary>
        public const double SunMass = 1.989e30;

        /// <summary>
        /// The radius of the Sun (IRL) in km
        /// </summary>
        public const double SunRadius = 695508;

        /// <summary>
        /// The luminosity of the Sun in Watt
        /// </summary>
        public const double SunLuminosity = 3.846e+26;

        /// <summary>
        /// Game's Sun luminosity in Watt
        /// </summary>
        public const double KerbolLuminosity = 3.16e+24;

        /// <summary>
        /// A light-year in KM
        /// </summary>
        public const double LightYear = 9460730472580.8;
        #endregion

        #region Kerbal

        #region Kerbin
        /// <summary>
        /// Radius of Kerbin in meters
        /// </summary>
        public const int KerbinRadius = 600000;

        /// <summary>
        /// Mass of Kerbin in kg
        /// </summary>
        public const double KerbinMass = 5.2915158e22;

        /// <summary>
        /// Volumic mass of Kerbin in kg/m^3
        /// </summary>
        public const double KerbinVolumicMass = 58484.09;
        #endregion

        #region Jool
        /// <summary>
        /// Radius of Jool in meters
        /// </summary>
        public const int JoolRadius = 6000000;

        /// <summary>
        /// Mass of Jool in kg
        /// </summary>
        public const double JoolMass = 4.2332127e24;

        /// <summary>
        /// Volumic mass of Jool in kg/m^3
        /// </summary>
        public const double JoolVolumicMass = 4678.7273;
        #endregion

        #endregion
    }
}