#nullable enable

namespace IBL
{
    public partial class BL : IBL
    {
        public void UpdateBaseStation(int id, string? name = null, int? numChargingSlots = null)
        {
            try { dal.UpdateStation(id, name: name, chargeSlots: numChargingSlots); }
            catch { /* TODO: throw StationNotFound */ }
        }

        public void UpdateCustomer(int id, string? name = null, string? phone = null)
        {
            try { dal.UpdateCustomer(id, name: name, phone: phone); }
            catch { /* TODO: throw CustomerNotFound */ }
        }

        public void UpdateDrone(int id, string model)
        {
            try { dal.UpdateDrone(id, model: model); }
            catch { /* TODO: throw DroneNotFound */ }
        }
    }
}
