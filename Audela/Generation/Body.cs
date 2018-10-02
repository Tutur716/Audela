using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Audela.Generation
{
    /// <summary>
    /// All the bodies must have this class in their genealogy.
    /// It contains all basic componants defining a body.
    /// </summary>
    class Body
    {
        /// <summary>
        /// The type of the body
        /// </summary>
        public virtual BodyType BodyType { get; }

        #region Mass
        /// <summary>
        /// The mass of the body in grams
        /// </summary>
        public virtual double Mass { get; set; }
        /// <summary>
        /// The mass of the body in solar masses
        /// </summary>
        public double SolarMasses
        {
            get
            {
                return Mass / solarMass;
            }
        }
        /// <summary>
        /// The mass of the Sun in grams
        /// </summary>
        public const double solarMass = 1.989e+33;
        /// <summary>
        /// The mass of the body in Jupiter masses
        /// </summary>
        public double JupiterMasses
        {
            get
            {
                return Mass / jupiterMass;
            }
        }
        /// <summary>
        /// The mass of Jupiter in grams
        /// </summary>
        public const double jupiterMass = 1.898e+30;
        /// <summary>
        /// The mass of the body in Earth masses
        /// </summary>
        public double EarthMasses
        {
            get
            {
                return Mass / earthMass;
            }
        }
        /// <summary>
        /// The mass of the Earth in grams
        /// </summary>
        public const double earthMass = 5.972e+27;
        /// <summary>
        /// The mass of the body in Moon masses
        /// </summary>
        public double MoonMasses
        {
            get
            {
                return Mass / moonMass;
            }
        }
        /// <summary>
        /// The mass of the Moon in grams
        /// </summary>
        public const double moonMass = 7.342e+22;
        #endregion

        #region Size
        /// <summary>
        /// The radius of the body in meters
        /// </summary>
        public virtual double Radius { get; set; }
        /// <summary>
        /// The radius of the body in Solar Radii
        /// </summary>
        public double SolarRadii
        {
            get
            {
                return Radius / solarRadius;
            }
        }
        /// <summary>
        /// The radius of the Sun in meters
        /// </summary>
        public const double solarRadius = 6.957e+8;
        /// <summary>
        /// The radius of the body in Jupiter Radii
        /// </summary>
        public double JupiterRadii
        {
            get
            {
                return Radius / jupiterRadius;
            }
        }
        /// <summary>
        /// The radius of Jupiter in meters
        /// </summary>
        public const double jupiterRadius = 6.9911e+7;
        /// <summary>
        /// The radius of the body in Earth Radii
        /// </summary>
        public double EarthRadii
        {
            get
            {
                return Radius / earthRadius;
            }
        }
        /// <summary>
        /// The radius of the Earth in meters
        /// </summary>
        public const double earthRadius = 6.371e+6;
        /// <summary>
        /// The radius of the body in Moon Radii
        /// </summary>
        public double MoonRadii
        {
            get
            {
                return Radius / moonRadius;
            }
        }
        /// <summary>
        /// The radius of the Moon in meters
        /// </summary>
        public const double moonRadius = 1.737e+6;

        /// <summary>
        /// The Diameter of the body in meter
        /// </summary>
        public double Diameter
        {
            get
            {
                return Radius * 2;
            }
        }
        #endregion

        #region Volume
        /// <summary>
        /// The volume of the body in cubic meters
        /// </summary>
        public double Volume
        {
            get
            {
                return (4 * Math.PI / 3) * Math.Pow(Radius, 3);
            }
        }
        /// <summary>
        /// The volumic mass of the body in g/m^3
        /// </summary>
        public double VolumicMass
        {
            get
            {
                return Mass / Volume;
            }
        }
        /// <summary>
        /// The density of the body
        /// </summary>
        public double Density
        {
            get
            {
                return VolumicMass / waterVolumicMass;
            }
        }
        /// <summary>
        /// The volumic mass of the Water in g/m^3
        /// </summary>
        public const double waterVolumicMass = 1000000;
        #endregion

        #region Gravity
        /// <summary>
        /// The surface gravity in m/s^2
        /// </summary>
        public double SurfaceGravity
        {
            get
            {
                return (G * (Mass / 1000)) / Math.Pow(Radius, 2);
            }
        }
        /// <summary>
        /// The Gravitational Constant in m^3/kg/s^2
        /// </summary>
        public const double G = 6.670831e-11;
        /// <summary>
        /// Surface gravity in g
        /// </summary>
        public double EarthGravity
        {
            get
            {
                return SurfaceGravity / earthSurfaceGravity;
            }
        }
        /// <summary>
        /// The surface gravity of 1 g
        /// </summary>
        public const double earthSurfaceGravity = 9.80665;
        #endregion

        #region Orbit
        /// <summary>
        /// Orbital reference body of the body
        /// </summary>
        public string ReferenceBody = "Sun";
        /// <summary>
        /// Orbital inclination of the body
        /// </summary>
        public double Inclination = 0;
        /// <summary>
        /// Orbital eccentricity of the body
        /// </summary>
        public double Eccentricity = 0;
        /// <summary>
        /// Orbital semi major axis of the body
        /// </summary>
        public double SemiMajorAxis = 27200000000;
        /// <summary>
        /// Orbital mean anomaly at epoch of the body
        /// </summary>
        public double MeanAnomalyAtEpoch = 0;
        /// <summary>
        /// Orbital longitude of ascending node of the body
        /// </summary>
        public double LongitudeOfAscendingNode = 0;
        /// <summary>
        /// Orbital epoch of the body
        /// </summary>
        public double Epoch = 2000;
        #endregion

        #region Star
        public virtual Data.SpectralTypes SpectralType { get; set; }
        /// <summary>
        /// [STAR] Temperature of the Star in Kelvin
        /// </summary>
        public virtual double Temperature { get; set; }
        /// <summary>
        /// [STAR] The color of the star in RGBA
        /// </summary>
        public double[] Color { get; set; }
        /// <summary>
        /// [STAR] The luminosity of the star in Watt
        /// </summary>
        public double Luminosity { get; set; }
        /// <summary>
        /// [STAR] The luminosity of the Sun in Watt
        /// </summary>
        public const double SunLuminosity = 3.846e+26;
        /// <summary>
        /// [STAR] Game's Sun luminosity in Watt
        /// </summary>
        public const double KerbolLuminosity = 3.16e+24;
        #endregion

        #region Others
        /// <summary>
        /// The name of the Body
        /// </summary>
        public virtual string Name { get; set; } = "Name of the Body is not set!";
        /// <summary>
        /// The description of the body
        /// </summary>
        public virtual string Description { get; set; } = "The description of the Body is not set!";
        #endregion

        
    }
}
