using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lt = Audela.Generation.Data.LuminosityTypes;
using st = Audela.Generation.Data.SpectralTypes;

namespace Audela.Generation.Data.Fraction
{
    class SpectralTypeDistribution
    {
        /// <summary>
        /// Returns the distribution of a star spectral type (0 to 1)
        /// </summary>
        /// <returns></returns>
        static public double GetDistributionSingle(lt LuminosityClass, st SpectralType)
        {
            double d = 0;

            switch(LuminosityClass)
            {
                case lt.V:
                    switch (SpectralType)
                    {
                        case st.O:
                            d = 0.0000003;
                            break;

                        case st.B:
                            d = 0.0013;
                            break;

                        case st.A:
                            d = 0.006;
                            break;

                        case st.F:
                            d = 0.03;
                            break;

                        case st.G:
                            d = 0.0771997; //0.076;
                            break;

                        case st.K:
                            d = 0.121;
                            break;

                        case st.M:
                            d = 0.7645;
                            break;
                    }
                    break;
            }

            return d;
        }

        /// <summary>
        /// Returns the a spectral type by a number between 0 and 1
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        static public st GetTypeFromNumber(lt lt, double Number)
        {
            st st = st.G;
            double cumuledTotal = 0;

            switch (lt)
            {
                case lt.V:
                    {
                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.M) + cumuledTotal) st = st.M;
                        cumuledTotal = GetDistributionSingle(lt, st.M);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.K) + cumuledTotal) st = st.K;
                        cumuledTotal = GetDistributionSingle(lt, st.K);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.G) + cumuledTotal) st = st.G;
                        cumuledTotal = GetDistributionSingle(lt, st.G);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.F) + cumuledTotal) st = st.F;
                        cumuledTotal = GetDistributionSingle(lt, st.F);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.A) + cumuledTotal) st = st.A;
                        cumuledTotal = GetDistributionSingle(lt, st.A);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.B) + cumuledTotal) st = st.B;
                        cumuledTotal = GetDistributionSingle(lt, st.B);

                        if (Number >= cumuledTotal && Number < GetDistributionSingle(lt, st.O) + cumuledTotal) st = st.O;
                        cumuledTotal = GetDistributionSingle(lt, st.O);

                        break;
                    }
            }


            return st;
        }

        /// <summary>
        /// Selects a random Spectral Type by its Luminosity Type
        /// </summary>
        /// <returns></returns>
        static public st RandomType(lt LuminosityType)
        {
            return GetTypeFromNumber(LuminosityType, RandomBySeed.GetRandom().NextDouble());
        }
    }
}
