namespace DotNet.AdvancedCollections.Concurrent;

public interface ISynchronized
{
    /// <summary>
    /// Gets a value indicating whether access to the data structure is synchronized (thread-safe).
    /// </summary>
    bool IsSynchronized { get; }

    /// <summary>
    /// Gets an object that can be used to synchronize access to the data structure.
    /// </summary>
    object SyncRoot { get; }
}