using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public partial class UpdateStationPage : Utilities.IObservable<UpdateStationPage>
    {
        List<Utilities.IObserver<UpdateStationPage>> observers = new();

        public void AddObserver(Utilities.IObserver<UpdateStationPage> observer) {
            observers.Add(observer);
        }

        public void NotifyObservers() {
            foreach (var observer in observers) {
                observer.Update(this);
            }
        }
    }
}
