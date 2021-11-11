using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDal
{
	interface IDal
	{
		public void AddStation(Station station);

		public void AddDrone(Drone drone);

		public void AddCustomer(Customer customer);

		public void AddPackage(int senderId, int targetId, WeightCategory weight, Priority priority);

		public void AssignPackage(int packageId);

		public void CollectPackage(int packageId);

		public void ProvidePackage(int packageId);

		public void ChargeDrone(int droneId, int stationId);

		public void ReleaseDrone(int droneId);

		public Station GetStation(int id);

		public Drone GetDrone(int id);

		public Customer GetCustomer(int id);

		public Package GetPackage(int id);

		public string GetStationList();

		public string GetUnoccupiedStationsList();

		public string GetDroneList();

		public string GetPackageList();

		public string GetUnassignedPackageList();

		public string GetCustomerList();
	}
}
