#nullable enable

using System;
using System.Collections.Generic;
using BL;
using DalXML;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        private DalApi.IDAL dal;
        private (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate) powerConsumption;
        private static readonly Lazy<BL> instance = new(() => new BL());

        private readonly Random rand = new();

        internal static BL Instance {
            get 
            {
                return instance.Value;
            }
        }

        private BL()
        {
            // dal = new DalObject.DalObject();
            dal = DalXml.Instance;

            powerConsumption = dal.GetPowerConsumption();

            var packages = dal.GetPackageList();
            // Convert to list so we can get a random station without
            // double-enumeration (count + access).
            var stations = dal.GetStationList().ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StartSimulator(int id, Func<bool> condition, Action update) {
            new Simulator(this, id, condition, update);
        }
    }
}
