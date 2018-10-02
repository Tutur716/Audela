using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lc = Audela.Generation.Data.LuminosityTypes;

namespace Audela.Generation.Data.Fraction
{
    static class LuminosityTypesDistribution
    {
        /// <summary>
        /// Returns the distribution of a star luminosity type (0 to 1)
        /// </summary>
        /// <param name="Class"></param>
        /// <returns></returns>
        static public double GetDistributionSingle(lc Class)
        {
            double d = 0;

            switch (Class)
            {
                case lc.Ia0:
                    d = 0.0003;
                    break;

                case lc.Ia:
                    d = 0.0043;
                    break;

                case lc.Iab:
                    d = 0.0034;
                    break;

                case lc.Ib:
                    d = 0.0123;
                    break;

                case lc.I:
                    d = 0.0003;
                    break;

                case lc.II:
                    d = 0.0289;
                    break;

                case lc.III:
                    d = 0.3913;
                    break;

                case lc.IV:
                    d = 0.114;
                    break;

                case lc.V:
                    d = 0.442;
                    break;

                case lc.VI:
                    d = 0.0016;
                    break;

                case lc.VII:
                    d = 0.0016;
                    break;
            }

            return d;
        }

        /// <summary>
        /// Returns the a luminosity type by a number between 0 and 1
        /// (Example : 0.0015 would give VI and 0.7 would give V)
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        static public lc GetTypeFromNumber(double Number)
        {
            lc lc = lc.V;
            double cumuledTotal = 0;

            
            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.Ia0) + cumuledTotal) lc = lc.Ia0;
            cumuledTotal = GetDistributionSingle(lc.Ia0); //0.0003


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.I) + cumuledTotal) lc = lc.I;
            cumuledTotal += GetDistributionSingle(lc.I); //0.0006


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.VI) + cumuledTotal) lc = lc.VI;
            cumuledTotal += GetDistributionSingle(lc.VI); //0.0022


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.VII) + cumuledTotal) lc = lc.VII;
            cumuledTotal += GetDistributionSingle(lc.VII); //0.0038


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.Iab) + cumuledTotal) lc = lc.Iab;
            cumuledTotal += GetDistributionSingle(lc.Iab); //0.0072


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.Ia) + cumuledTotal) lc = lc.Ia;
            cumuledTotal += GetDistributionSingle(lc.Ia); //0.0115


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.Ib) + cumuledTotal) lc = lc.Ib;
            cumuledTotal += GetDistributionSingle(lc.Ib); //0.0238


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.II) + cumuledTotal) lc = lc.II;
            cumuledTotal += GetDistributionSingle(lc.II); //0.0527


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.IV) + cumuledTotal) lc = lc.IV;
            cumuledTotal += GetDistributionSingle(lc.IV); //0.1667


            if (Number >= cumuledTotal && Number < GetDistributionSingle(lc.III) + cumuledTotal) lc = lc.III;
            cumuledTotal += GetDistributionSingle(lc.III); //0.558


            if (Number >= cumuledTotal && Number <= GetDistributionSingle(lc.V) + cumuledTotal) lc = lc.V;
            cumuledTotal += GetDistributionSingle(lc.V); //1

            return lc;
        }

        /// <summary>
        /// Selects a random Luminosity Type
        /// </summary>
        /// <returns></returns>
        static public lc RandomType()
        {
            Random r = RandomBySeed.GetRandom();
            double d = r.NextDouble();
            return GetTypeFromNumber(d);
        }
    }
}
