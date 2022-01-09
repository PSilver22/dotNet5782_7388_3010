namespace PL.Utilities
{
    /// <summary>
    /// Observer interface
    /// </summary>
    /// <typeparam name="T">The object that will be observed</typeparam>
    public interface IObserver<T>
    {
        /// <summary>
        /// Updates observer with new data
        /// </summary>
        /// <param name="data">The object that was updated</param>
        public void Update(T data);
    }
}
