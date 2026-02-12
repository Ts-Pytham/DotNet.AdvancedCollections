using DotNet.AdvancedCollections.Queue.PriorityQueue;
using DotNet.AdvancedCollections.Stack.PriorityStack;

namespace DotNet.AdvancedCollections.Utils;

/// <summary>
/// Represents an object with a priority. This class defines comparison operators to allow objects of this type to be used in <see cref="PriorityStack{T}"/> and <see cref="PriorityQueue{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the object being given a priority.</typeparam>
internal sealed class PriorityObject<T>(T value, int priority)
    where T : notnull
{
    /// <summary>
    /// The value of the object being given a priority.
    /// </summary>
    public T Value { get; set; } = value;

    /// <summary>
    /// The priority of the object.
    /// </summary>
    public int Priority { get; set; } = priority;

    /// <summary>
    /// Defines the "greater than" comparison operator for objects of this type.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the first object has a greater priority than the second object, or false otherwise.</returns>
    public static bool operator >(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority > o2.Priority;
    }

    /// <summary>
    /// Defines the "less than" comparison operator for objects of this type.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the first object has a lower priority than the second object, or false otherwise.</returns>
    public static bool operator <(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority < o2.Priority;
    }

    /// <summary>
    /// Defines the "greater than or equal to" comparison operator for objects of this type.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the first object has a greater or equal priority than the second object, or false otherwise.</returns>
    public static bool operator >=(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority >= o2.Priority;
    }

    /// <summary>
    /// Defines the "less than or equal to" comparison operator for objects of this type.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the first object has a less or equal priority than the second object, or false otherwise.</returns>
    public static bool operator <=(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority <= o2.Priority;
    }

    /// <summary>
    /// The operator overloads allow comparison of PriorityObjects based on their Priority property.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the Priority property of the two objects is equal, false otherwise.</returns>
    public static bool operator ==(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority == o2.Priority;
    }

    /// <summary>
    /// The operator overloads allow comparison of PriorityObjects based on their Priority property.
    /// </summary>
    /// <param name="o">The first object being compared.</param>
    /// <param name="o2">The second object being compared.</param>
    /// <returns>True if the Priority property of the two objects is not equal, false otherwise.</returns>
    public static bool operator !=(PriorityObject<T> o, PriorityObject<T> o2)
    {
        return o.Priority != o2.Priority;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="PriorityObject{T}"/>.
    /// </summary>
    /// <param name="obj">The object to compare with the current <see cref="PriorityObject{T}"/>.</param>
    /// <returns>True if the specified object is equal to the current <see cref="PriorityObject{T}"/>, false otherwise.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not PriorityObject<T> obj2)
        {
            return false;
        }

        return obj2.Priority == Priority;

    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="PriorityObject{T}"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="PriorityObject{T}"/>, including its Value and Priority properties.</returns>
    public override string ToString()
    {
        return $"Value: {Value}, Priority: {Priority}";
    }
}
