﻿#nullable enable

using System;
using DO;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL
    {
        /// <summary>
        /// Returns the power consumption/km for a drone carrying a package
        /// of the given weight category.
        /// </summary>
        /// <param name="weight">the weight category</param>
        /// <returns>the power consumption/km for the given weight</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        internal double GetPowerConsumption(WeightCategory weight)
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
