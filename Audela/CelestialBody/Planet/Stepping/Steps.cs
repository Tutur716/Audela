using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Stepping
{
    /// <summary>
    /// The steps list
    /// </summary>
    class Steps
    {
        // ! Think to make a resonance system for later ! //

        /// <summary>
        /// The List containing the steps
        /// </summary>
        public static List<Step> PlanetList = new List<Step>();
        public static List<Step> MoonList = new List<Step>();


        #region List Manager
        /// <summary>
        /// Create a blank list
        /// </summary>
        public static List<Step> Clear(List<Step> List)
        {
            if (List == PlanetList)
            {
                return PlanetList = new List<Step>();
            }

            else
            {
                return MoonList = new List<Step>();
            }
        }

        /// <summary>
        /// Adds a step in the list
        /// </summary>
        /// <param name="Step"></param>
        /// <returns></returns>
        public static void AddStep(Step Step, List<Step> List)
        {
            if(List == PlanetList)
            {
                PlanetList.Add(Step);
            }

            else
            {
                MoonList.Add(Step);
            }

            
        }
        #endregion

        #region Step Manager
        /// <summary>
        /// Get the Step correspond to the ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Step GetStep(int ID, List<Step> List)
        {
            if (List == PlanetList)
            {
                return PlanetList[ID];
            }

            else
            {
                return MoonList[ID];
            }
        }

        /// <summary>
        /// Creates a Step
        /// </summary>
        public static Step CreateStep(double Distance, double SpawnChance, double SolidChance, double GasChance, List<Step> List)
        {
            Step s = new Step();

            if (Distance >= 0)
            {
                s.Distance = Distance;
            }

            if (SpawnChance >= 0 && SpawnChance <= 1)
            {
                s.SpawnChance = SpawnChance;
            }

            if ((SolidChance + GasChance) == 1)
            {
                s.SolidChance = SolidChance;
                s.GasChance = GasChance;
            }

            if(List == PlanetList)
            {
                AddStep(s, PlanetList);
            }

            else
            {
                AddStep(s, MoonList);
            } 

            return s;
        }

        /// <summary>
        /// Create a Random value to check if the Planet can spawn for a said Step
        /// </summary>
        public static bool RandomSpawn(int ID, List<Step> List)
        {
            if (RandomBySeed.GetRandom().NextDouble() <= GetStep(ID, List).SpawnChance) return true;
            else return false;
        }
        public static bool RandomSpawn(Step Step)
        {
            if (RandomBySeed.GetRandom().NextDouble() <= Step.SpawnChance) return true;
            else return false;
        }

        /// <summary>
        /// Create a Random planet type for a said Step
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static BodyType RandomBodyType(int ID, List<Step> List)
        {
            Random r = RandomBySeed.GetRandom();
            if (r.NextDouble() <= List[ID].SolidChance) return BodyType.Selenia;
            else return BodyType.GasGiant;
        }
        public static BodyType RandomBodyType(Step Step)
        {
            Random r = RandomBySeed.GetRandom();

            if(r.NextDouble() <= Step.SolidChance)
            {
                return BodyType.Selenia;
            }

            else
            {
                return BodyType.GasGiant;
            }
        }

        #region Returns
        /// <summary>
        /// Returns Step's Distance
        /// </summary>
        public static double GetStepDistance(int ID, List<Step> List)
        {
            return List[ID].Distance;
        }
        public static double GetStepDistance(Step Step)
        {
            return Step.Distance;
        }

        /// <summary>
        /// Returns Step's Spawn Chance
        /// </summary>
        public static double GetStepSpawnChance(int ID, List<Step> List)
        {
            return List[ID].SpawnChance;
        }
        public static double GetStepSpawnChance(Step Step)
        {
            return Step.SpawnChance;
        }

        /// <summary>
        /// Returns Step's Solid Chance
        /// </summary>
        public static double GetStepSolidChance(int ID, List<Step> List)
        {
            return List[ID].SolidChance;
        }
        public static double GetStepSolidChance(Step Step)
        {
            return Step.SolidChance;
        }

        /// <summary>
        /// Returns Step's Solid Chance
        /// </summary>
        public double GetStepGasChance(int ID, List<Step> List)
        {
            return List[ID].GasChance;
        }
        public double GetStepGasChance(Step Step)
        {
            return Step.GasChance;
        }
        #endregion

        #endregion
    }
}
