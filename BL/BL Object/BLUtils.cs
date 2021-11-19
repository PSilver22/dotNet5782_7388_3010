#nullable enable

using System;
using IDAL.DO;

namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// Returns the power consumption/km for a drone carrying a package
        /// of the given weight category.
        /// </summary>
        /// <param name="weight">the weight category</param>
        /// <returns>the power consumption/km for the given weight</returns>
        private double getPowerConsumption(WeightCategory weight)
        {
            return weight switch
            {
                WeightCategory.light => powerConsumption.LightWeight,
                WeightCategory.medium => powerConsumption.MidWeight,
                WeightCategory.heavy => powerConsumption.HeavyWeight,
                _ => 0,
            };
        }
    }
}
