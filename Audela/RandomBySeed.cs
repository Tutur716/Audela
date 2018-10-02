using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Audela
{
    static class RandomBySeed
    {
        static Random random;
        static int seed;

        /// <summary>
        /// Creates the random by a seed
        /// </summary>
        /// <param name="Seed"></param>
        /// <returns></returns>
        public static Random SetRandom(int Seed)
        {
            seed = Seed;
            return random = new Random(Seed);
        }

        /// <summary>
        /// Get the Random used for random values
        /// </summary>
        /// <returns></returns>
        public static Random GetRandom()
        {
            return random;
        }

        /// <summary>
        /// Get the Seed used for the Random
        /// </summary>
        /// <returns></returns>
        public static int GetSeed()
        {
            return seed;
        }
    }
}
