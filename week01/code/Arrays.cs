public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create an array of size 'length'
        double[] multiples = new double[length];

        // Step 2: Use a loop to calculate each multiple of 'number'
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1); // Calculate the multiple and store it in the array
        }

        // Step 3: Return the array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        // Step 1: Use modulo to handle cases where 'amount' exceeds the size of the list
        amount %= data.Count;

        // Step 2: Slice the list into two parts
        List<int> lastPart = data.GetRange(data.Count - amount, amount); // Last 'amount' elements
        List<int> firstPart = data.GetRange(0, data.Count - amount); // Remaining elements

        // Step 3: Clear the original list and concatenate the slices
        data.Clear();
        data.AddRange(lastPart); // Add the last part first
        data.AddRange(firstPart); // Add the first part next
    }
}
