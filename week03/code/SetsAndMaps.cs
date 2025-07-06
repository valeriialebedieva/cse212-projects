using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
     public static string[] FindPairs(string[] words)
    {
        if (words == null)
            throw new ArgumentNullException(nameof(words), "Input array cannot be null.");

        // Initialize a HashSet for O(1) lookups and handle duplicates efficiently
        var wordSet = new HashSet<string>(words.Length);

        // Validate input and populate the HashSet
        foreach (var word in words)
        {
            if (word == null)
                throw new ArgumentException("Words in the array cannot be null.", nameof(words));

            if (word.Length != 2)
                throw new ArgumentException("All words must be exactly two characters long.", nameof(words));

            wordSet.Add(word);
        }

        // List to hold the final paired words
        var pairedWords = new List<string>();

        // Iterate through each unique word to find its reversed counterpart
        foreach (var word in wordSet)
        {
            // Compute the reversed word using direct character access for efficiency
            string reversedWord;
            reversedWord = $"{word[1]}{word[0]}";

            // Check if the reversed word exists in the set and avoid self-pairing
            if (word != reversedWord && wordSet.Contains(reversedWord))
            {
                // To prevent duplicate pairs (e.g., "ab & ba" and "ba & ab"), enforce a consistent order
                if (string.Compare(word, reversedWord, StringComparison.Ordinal) < 0)
                {
                    pairedWords.Add($"{reversedWord} & {word}");
                }
            }
        }

        return pairedWords.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        foreach (var line in File.ReadLines(filename))
        {
            // Split the line into fields using comma as delimiter
            var fields = line.Split(",");
            
            // Get the degree from the 4th column (index 3)
            // Trim any whitespace to ensure consistent keys
            var degree = fields[3].Trim();
            
            // If the degree is already in the dictionary, increment its count
            // Otherwise, add it with an initial count of 1
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }  

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Remove spaces and convert to lowercase for case-insensitive comparison
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // Quick check - if lengths are different, they can't be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Create dictionary to store character frequencies for word1
        var letterCount = new Dictionary<char, int>();

        // Count frequencies of letters in word1
        foreach (char c in word1)
        {
            if (letterCount.ContainsKey(c))
                letterCount[c]++;
            else
                letterCount[c] = 1;
        }

        // Check letters in word2 against the frequencies in word1
        foreach (char c in word2)
        {
            // If letter doesn't exist in word1 or count becomes negative,
            // words are not anagrams
            if (!letterCount.ContainsKey(c) || letterCount[c] == 0)
                return false;
                
            letterCount[c]--;
        }

        // All letters matched with correct frequencies
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        return featureCollection.Features
        .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Magnitude}")
        .ToArray();
    }
}