#nullable enable

using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateBaseStation(int id, string? name = null, int? numChargingSlots = null)
        {
            lock (Dal) {
                try { Dal.UpdateStation(id, name: name, chargeSlots: numChargingSlots); } catch { throw new StationNotFoundException(id); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int id, string? name = null, string? phone = null)
        {
            lock (Dal) {
                try { Dal.UpdateCustomer(id, name: name, phone: phone); } catch { throw new CustomerNotFoundException(id); }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(int id, string model)
        {
            lock (Dal) {
                Dal.UpdateDrone(id, model: model);
            }
        }
    }
}
