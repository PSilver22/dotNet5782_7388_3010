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
        public DalApi.IDAL Dal { get; }

        public (double Free, double LightWeight, double MidWeight, double HeavyWeight, double ChargeRate)
            PowerConsumption { get; }

        private static readonly Lazy<BL> instance = new(() => new BL());

        private readonly Random _rand = new();

        internal static BL Instance
        {
            get { return instance.Value; }
        }

        private BL()
        {
            // dal = new DalObject.DalObject();
            Dal = DalXml.Instance;

            PowerConsumption = Dal.GetPowerConsumption();

            var packages = Dal.GetPackageList();
            // Convert to list so we can get a random station without
            // double-enumeration (count + access).
            var stations = Dal.GetStationList().ToList();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StartSimulator(int id, Func<bool> condition, Action update)
        {
            new Simulator(this, id, condition, update);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void StartSimulator(int id, Func<bool> condition, Action update) {
            new Simulator(this, id, condition, update);
        }
    }
}