using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Create a queue with the following tasks and priorities:
    // - Urgent Bug Fix (priority 3)
    // - Regular Meeting (priority 1)
    // - Code Review (priority 2)
    // Expected Result: Tasks should be dequeued in order of priority:
    // 1. Urgent Bug Fix (highest priority 3)
    // 2. Code Review (medium priority 2)
    // 3. Regular Meeting (lowest priority 1)
    // Defect(s) Found: Items were not being dequeued in correct priority order
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Regular Meeting", 1);
        priorityQueue.Enqueue("Urgent Bug Fix", 3);
        priorityQueue.Enqueue("Code Review", 2);

        Assert.AreEqual("Urgent Bug Fix", priorityQueue.Dequeue());
        Assert.AreEqual("Code Review", priorityQueue.Dequeue());
        Assert.AreEqual("Regular Meeting", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Create a queue with tasks of equal priority:
    // - Morning Standup (priority 2)
    // - Team Lunch (priority 2)
    // - Project Update (priority 2)
    // Expected Result: Tasks should be dequeued in FIFO order since they have equal priority:
    // 1. Morning Standup (added first)
    // 2. Team Lunch (added second)
    // 3. Project Update (added last)
    // Defect(s) Found: FIFO order was not preserved for items with equal priority
    public void TestPriorityQueue_EqualPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Morning Standup", 2);
        priorityQueue.Enqueue("Team Lunch", 2);
        priorityQueue.Enqueue("Project Update", 2);

        Assert.AreEqual("Morning Standup", priorityQueue.Dequeue());
        Assert.AreEqual("Team Lunch", priorityQueue.Dequeue());
        Assert.AreEqual("Project Update", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Create a queue with mixed priorities and FIFO ordering:
    // 1. Daily Report (priority 2)
    // 2. Critical Issue (priority 3)
    // 3. Team Meeting (priority 2)
    // 4. System Failure (priority 3)
    // Expected Result: 
    // 1. Critical Issue (highest priority, added first)
    // 2. System Failure (highest priority, added second)
    // 3. Daily Report (medium priority, added first)
    // 4. Team Meeting (medium priority, added second)
    // Defect(s) Found: Complex priority and FIFO ordering was not working correctly
    public void TestPriorityQueue_MixedPrioritiesWithFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Daily Report", 2);
        priorityQueue.Enqueue("Critical Issue", 3);
        priorityQueue.Enqueue("Team Meeting", 2);
        priorityQueue.Enqueue("System Failure", 3);

        Assert.AreEqual("Critical Issue", priorityQueue.Dequeue());
        Assert.AreEqual("System Failure", priorityQueue.Dequeue());
        Assert.AreEqual("Daily Report", priorityQueue.Dequeue());
        Assert.AreEqual("Team Meeting", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue
    // Expected Result: Should throw InvalidOperationException with message 
    // "Cannot dequeue from an empty queue."
    // Defect(s) Found: Empty queue exception handling was not implemented correctly
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        var expectedMessage = "The queue is empty.";

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );
        Assert.AreEqual(expectedMessage, exception.Message);
    }
}