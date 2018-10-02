using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audela.CelestialBody.Planet.Stepping
{
    class Step
    {
        /*struct StepProperties
        {
            /// <summary>
            /// Distance of the step from the center of the body
            /// </summary>
            public double Distance;
            /// <summary>
            /// Chance of the spawning of a body on the step
            /// </summary>
            public double SpawnChance;
            /// <summary>
            /// Value from 0 to 1 of spawning chance of a solid body
            /// </summary>
            public double SolidChance;
            /// <summary>
            /// Value from 0 to 1 of spawing chance of a gas body
            /// </summary>
            public double GasChance;

        };

        Random r = RandomBySeed.GetRandom();

        /// <summary>
        /// List of all the orbital steps
        /// </summary>
        public static List<Step> Steps = new List<Step>();

        /// <summary>
        /// Adds a Step in the list, Distance >0, SpawnChance between 0 and 1, SolidChance + GasChance = 1
        /// </summary>
        public Step AddStep(double Distance, double SpawnChance, double SolidChance, double GasChance)
        {
            //Step st = new Step();
            StepProperties sp;

            if(Distance >= 0)
            {
                sp.Distance = Distance;
            }

            if(SpawnChance >= 0 && SpawnChance <= 1)
            {
                sp.SpawnChance = SpawnChance;
                
            }

            if((SolidChance + GasChance) == 1)
            {
                sp.SolidChance = SolidChance;
                sp.GasChance = GasChance;
            }

            Steps.Add(this);
            return this;
        }

        public double GetStepSpawnC(Step Step)
        {
            return Step.;
        }
        public double GetStepSMA(Step Step)
        {
            return Step.Dist;
        }

        public static Step GetStep(int StepID)
        {
            return Steps[StepID];
        }

        public static List<Step> ClearStepList()
        {
            Steps = new List<Step>();
            return Steps;
        }

        public List<Step> StepSetup(Star Star)
        {
            ClearStepList();

            double r = Star.Radius / Body.solarRadius;
            AddStep(1e+10 * r, 1, 1, 0);
            AddStep(5e+10 * r, 0.5, 0.5, 0.5);
            AddStep(1.5e+11 * r, 1, 0, 1);
            AddStep(2e+11 * r, 0, 0, 1);

            return Steps;
        }

        public bool IsAbleToSpawn(Step Step)
        {
            double d = r.NextDouble();

            //if (d <= Step.SpawnC) Console.WriteLine("Spawn !");
            //else Console.WriteLine("Nothing !");

            if (d <= Step.SpawnC) return true;
            else return false;
        }

        public BodyType GetPlanetType(Step Step)
        {
            double per = r.NextDouble();

            if (per <= Step.SolidC) return BodyType.SolidPlanet;
            else return BodyType.GasGiant;
            

        }*/

        /// <summary>
        /// Distance of the step from the center of the body
        /// </summary>
        public double Distance = 1e10;
        /// <summary>
        /// Chance of the spawning of a body on the step
        /// </summary>
        public double SpawnChance = 1;
        /// <summary>
        /// Value from 0 to 1 of spawning chance of a solid body
        /// </summary>
        public double SolidChance = 0.5;
        /// <summary>
        /// Value from 0 to 1 of spawing chance of a gas body
        /// </summary>
        public double GasChance = 0.5;
    }
}
