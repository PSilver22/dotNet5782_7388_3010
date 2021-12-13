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

        public void UpdateDrone(int id, string? model = null)
        {
            if (model is null)
            {
                try { dal.UpdateDrone(id, model: model); }
                catch { throw new DroneNotFoundException(id); }
            }
        }
    }
}
