using System;
using System.Collections.Generic;

public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.  The
    /// node is always added to the back of the queue regardless of 
    /// the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    public string Dequeue()
    {
        if (_queue.Count == 0) // Verify the queue is not empty
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Initialize with first element
        int highestPriorityIndex = 0;
        int highestPriority = _queue[0].Priority;

        // Find item with highest priority
        for (int i = 1; i < _queue.Count; i++)
        {
            // Current element has higher priority
            if (_queue[i].Priority > highestPriority)
            {
                highestPriorityIndex = i;
                highestPriority = _queue[i].Priority;
            }
            // If equal priority, take the one that was added first (FIFO for same priority)
            else if (_queue[i].Priority == highestPriority && i < highestPriorityIndex)
            {
                highestPriorityIndex = i;
            }
        }

        // Store the value to return
        string valueToReturn = _queue[highestPriorityIndex].Value;

        // Remove the item
        _queue.RemoveAt(highestPriorityIndex);

        return valueToReturn;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}