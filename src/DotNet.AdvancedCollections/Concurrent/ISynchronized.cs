namespace DotNet.AdvancedCollections.Concurrent;

/// <summary>
/// Defines synchronization members for thread-safe collection access.
/// </summary>
public interface ISynchronized
{
    /// <summary>
    /// Gets a value indicating whether access to the collection is synchronized (thread-safe).
    /// </summary>
    bool IsSynchronized { get; }

    /// <summary>
    /// Gets an object that can be used to synchronize access to the collection.
    /// </summary>
    object SyncRoot { get; }
}
