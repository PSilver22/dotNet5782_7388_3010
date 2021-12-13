#nullable enable

namespace IBL
{
    public partial class BL : IBL
    {
        public void UpdateBaseStation(int id, string? name = null, int? numChargingSlots = null)
        {
            try { dal.UpdateStation(id, name: name, chargeSlots: numChargingSlots); }
            catch { throw new StationNotFoundException(id); }
        }

        public void UpdateCustomer(int id, string? name = null, string? phone = null)
        {
            try { dal.UpdateCustomer(id, name: name, phone: phone); }
            catch { throw new CustomerNotFoundException(id); }
        }

        public void UpdateDrone(int id, string model)
        {
            try
            {
                dal.UpdateDrone(id, model: model);
                var droneIndex = drones.FindIndex(d => d.Id == id);
                var drone = drones[droneIndex];
                drone.Model = model;
                drones[droneIndex] = drone;
            }
            catch { throw new DroneNotFoundException(id); }
        }
    }
}
