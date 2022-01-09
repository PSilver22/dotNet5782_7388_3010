namespace PL.Utilities
{
    /// <summary>
    /// Observable interface
    /// </summary>
    /// <typeparam name="T">Type of the observable object</typeparam>
    public interface IObservable<T>
    {
        /// <summary>
        /// Adds an observer to observers container
        /// </summary>
        /// <param name="observer">The observer to add</param>
        public void AddObserver(IObserver<T> observer);

        /// <summary>
        /// Notifies the observers in registered in container
        /// </summary>
        public void NotifyObservers();
    }
}
