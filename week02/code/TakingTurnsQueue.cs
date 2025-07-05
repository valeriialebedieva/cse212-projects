/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private class QueueEntry
    {
        public string Name { get; }
        public int Turns { get; set; }

        public QueueEntry(string name, int turns)
        {
            Name = name;
            Turns = turns;
        }

        public Person ToPerson()
        {
            return new Person(Name, Turns);
        }
    }

    private readonly Queue<QueueEntry> _queue = new();

    public int Length => _queue.Count;
    
    // <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        _queue.Enqueue(new QueueEntry(name, turns));
    }
    
    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left.  Note that a turns value of 0 or less means the 
    /// person has an infinite number of turns.  An error exception is thrown 
    /// if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        var entry = _queue.Dequeue();
        var result = new Person(entry.Name, entry.Turns); // return copy for comparison

        if (entry.Turns <= 0)
        {
            // Infinite turns — re-enqueue unchanged
            _queue.Enqueue(entry);
        }
        else if (entry.Turns > 1)
        {
            entry.Turns -= 1;
            _queue.Enqueue(entry);
        }

        return result;
    }

    public override string ToString()
    {
        return string.Join(", ", _queue.Select(e => $"{e.Name}({e.Turns})"));
    }
}
