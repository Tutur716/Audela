using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Audela.CelestialBody
{
    class Palette
    {
        private static Random r = RandomBySeed.GetRandom();

        #region Palettes
        /// <summary>
        /// The palette, colors from 0 to 1
        /// </summary>
        public Tuple<double, double, double, double> Color
            = new Tuple<double, double, double, double>(0.7294, 0.8549, 0.3333, 1);
        /// <summary>
        /// The palette in RGBA
        /// </summary>
        public Tuple<int, int, int, int> RGBAPalette
        {
            get
            {
                return ColorToRGBAPalette(Color);
            }
        }
        /// <summary>
        /// The palette in HEX
        /// </summary>
        public string HEXPalette
        {
            get
            {
                return ColorToHexPalette(Color);
            }
        }
        #endregion

        #region Convertors
        /// <summary>
        /// Converts a 0->1 palette to a RGBA
        /// (Format: R, G, B, A)
        /// </summary>
        public Tuple<int, int, int, int> ColorToRGBAPalette(Tuple<double, double, double, double> Palette)
        {
            int r = (int)Math.Round(Palette.Item1 * 255);
            if (r > 255) r = 255;

            int g = (int)Math.Round(Palette.Item2 * 255);
            if (g > 255) g = 255;

            int b = (int)Math.Round(Palette.Item3 * 255);
            if (b > 255) b = 255;

            int a = (int)Math.Round(Palette.Item4 * 255);
            if (a > 255) a = 255;

            return new Tuple<int, int, int, int>(r, g, b, a);
        }
        /// <summary>
        /// Converts 0->1 palette to another an HEX palette (Format: R, G, B)
        /// </summary>
        public string ColorToHexPalette(Tuple<double, double, double, double> Palette)
        {
            Tuple<int, int, int, int> rgba = ColorToRGBAPalette(Palette);

            return rgba.Item1.ToString("X2") +
                   rgba.Item2.ToString("X2") +
                   rgba.Item3.ToString("X2")/* +
                   rgba.Item4.ToString("X2")*/;
        }
        /// <summary>
        /// Converts RGBA palette tp another 1->0 palette (Format: R, G, B, A)
        /// </summary>
        public Tuple<double, double, double, double> RGBAToColorPalette(Tuple<int, int, int, int> Palette)
        {
            return new Tuple<double, double, double, double>((double)Palette.Item1 / 255, (double)Palette.Item2 / 255, (double)Palette.Item3 / 255, (double)Palette.Item4 / 255);
        }
        #endregion

        #region Setters
        /// <summary>
        /// Set a Color palette (0->1)
        /// </summary>
        public Tuple<double, double, double, double> SetColorPalette(double Red, double Green, double Blue, double Alpha)
        {
            //Red
            double r = Red;
            if (r > 1) r = 1;
            if (r < 0) r = 0;

            //Green
            double g = Green;
            if (g > 1) g = 1;
            if (g < 0) g = 0;

            //Blue
            double b = Blue;
            if (b > 1) b = 1;
            if (b < 0) b = 0;

            //Alpha
            double a = Alpha;
            if (a > 1) a = 1;
            if (a < 0) a = 0;

            Tuple<double, double, double, double> p = new Tuple<double, double, double, double>(r, g, b, a);
            Color = p;
            return p;
        }
        /// <summary>
        /// Set a RGBA palette (0->255) (Int)
        /// </summary>
        public Tuple<double, double, double, double> SetRGBAPalette(int Red, int Green, int Blue, int Alpha)
        {
            Tuple<int, int, int, int> p = new Tuple<int, int, int, int>(Red, Green, Blue, Alpha);
            Tuple<double, double, double, double> t = RGBAToColorPalette(p);
            Color = t;
            return t;
        }
        /// <summary>
        /// Set a RGBA palette (0->255) (Int [])
        /// </summary>
        public Tuple<double, double, double, double> SetRGBAPalette(int[] RGBA)
        {
            Tuple<int, int, int, int> p = new Tuple<int, int, int, int>(RGBA[0], RGBA[1], RGBA[2], RGBA[3]);
            Tuple<double, double, double, double> t = RGBAToColorPalette(p);
            Color = t;
            return t;
        }
        /// <summary>
        /// Set a RGBA palette (0->255) (Double [])
        /// </summary>
        public Tuple<double, double, double, double> SetRGBAPalette(double[] RGBA)
        {
            Tuple<int, int, int, int> p = new Tuple<int, int, int, int>((int)RGBA[0], (int)RGBA[1], (int)RGBA[2], (int)RGBA[3]);
            Tuple<double, double, double, double> t = RGBAToColorPalette(p);
            Color = t;
            return t;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Gets the RGBA color as a array (0 = Red, 1 = Green, 2 = Blue, 3 = Alpha)
        /// </summary>
        /// <returns></returns>
        public double[] RGBAArray
        {
            get
            {
                //Tuple<int, int, int, int> p = new Tuple<int, int, int, int>(ColorToRGBAPalette(ColorPalette));
                return new double[]
                {
                    RGBAPalette.Item1,
                    RGBAPalette.Item2,
                    RGBAPalette.Item3,
                    RGBAPalette.Item4
                };
            }
        }
        /// <summary>
        /// Gets this object
        /// </summary>
        /// <returns></returns>
        public Palette GetPalette()
        {
            return this;
        }
        /// <summary>
        /// Gets the color palette
        /// </summary>
        public Tuple<double, double, double, double> ColorPalette
        {
            get
            {
                return Color;
            }
        }

        /// <summary>
        /// String of the color in the format r, g, b, a
        /// </summary>
        public override string ToString()
        {
            return ColorPalette.Item1 + ", " + ColorPalette.Item2 + ", " + ColorPalette.Item3 + ", " + ColorPalette.Item4;
        }

        public string RGBAString
        {
            get
            {
                return "RGBA(" + RGBAPalette.Item1 + ", " + RGBAPalette.Item2 +
                       ", " + RGBAPalette.Item3 + ", " + RGBAPalette.Item4 + ")";
            }
        }
        #endregion

        #region Other Methods
        /// <summary>
        /// Returns a new random Palette
        /// </summary>
        /// <returns></returns>
        public static Palette RandomColor()
        {
            Palette p = new Palette();
            p.SetColorPalette(r.NextDouble(), r.NextDouble(), r.NextDouble(), 1);
            return p;
        }

        public static Palette RandomColor(Palette Palette1, Palette Palette2)
        {
            //Colors
            double r1 = Palette1.ColorPalette.Item1;
            double r2 = Palette2.ColorPalette.Item1;
            double g1 = Palette1.ColorPalette.Item2;
            double g2 = Palette2.ColorPalette.Item2;
            double b1 = Palette1.ColorPalette.Item3;
            double b2 = Palette2.ColorPalette.Item3;
            double a1 = Palette1.ColorPalette.Item4;
            double a2 = Palette2.ColorPalette.Item4;

            //Best and worse colors
            double br;
            double sr;
            double bg;
            double sg;
            double bb;
            double sb;
            double ba;
            double sa;

            CompareTwoNumbers(r1, r2, out br, out sr);
            CompareTwoNumbers(g1, g2, out bg, out sg);
            CompareTwoNumbers(b1, b2, out bb, out sb);
            CompareTwoNumbers(a1, a2, out ba, out sa);

            Palette p = new Palette();
            p.SetColorPalette(r.NextDouble() * (br - sr) + sr, r.NextDouble() * (bg - sg) + sg, r.NextDouble() * (bb - sb) + sb, r.NextDouble() * (ba - sa) + sa);
            return p;
        }

        /// <summary>
        /// Returns if numbers are equals and outs bigger and smaller numbers
        /// If they are equals, it returns NumberA as Bigger and NumberB as Smaller
        /// </summary>
        private static bool CompareTwoNumbers(int NumberA, int NumberB, out int Bigger, out int Smaller)
        {
            if (NumberA > NumberB)
            {
                Bigger = NumberA;
                Smaller = NumberB;
                return false;
            }

            if (NumberA < NumberB)
            {
                Bigger = NumberB;
                Smaller = NumberA;
                return false;
            }

            else
            {
                Bigger = NumberA;
                Smaller = NumberB;
                return true;
            }
        }

        /// <summary>
        /// Returns if numbers are equals and outs bigger and smaller numbers
        /// If they are equals, it returns NumberA as Bigger and NumberB as Smaller
        /// </summary>
        private static bool CompareTwoNumbers(double NumberA, double NumberB, out double Bigger, out double Smaller)
        {
            if (NumberA > NumberB)
            {
                Bigger = NumberA;
                Smaller = NumberB;
                return false;
            }

            if (NumberA < NumberB)
            {
                Bigger = NumberB;
                Smaller = NumberA;
                return false;
            }

            else
            {
                Bigger = NumberA;
                Smaller = NumberB;
                return true;
            }
        }
        #endregion
    }
}
