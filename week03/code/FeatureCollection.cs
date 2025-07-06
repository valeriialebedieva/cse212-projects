using System.Text.Json.Serialization;

public class FeatureCollection
{
    [JsonPropertyName("features")]
    public Feature[] Features { get; set; }

    public string[] GetFormattedEarthquakes()
    {
        return Features.Select(f => $"{f.Properties.Place} - Mag {f.Properties.Magnitude}").ToArray();
    }
}

public class Feature
{
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}

public class Properties
{
    [JsonPropertyName("place")]
    public string Place { get; set; }

    [JsonPropertyName("mag")]
    public double Magnitude { get; set; }
}