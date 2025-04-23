using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GroupNineMobileProject
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("rating")]
        public double? Rating { get; set; }

        [JsonPropertyName("involved_companies")]
        public List<InvolvedCompany>? InvolvedCompanies { get; set; }

        [JsonIgnore]
        public string? PublisherName => InvolvedCompanies?
            .FirstOrDefault(c => c.Publisher && c.Company != null)?.Company?.Name;  
    }

    public class InvolvedCompany
    {
        [JsonPropertyName("company")]
        public Company? Company { get; set; }

        [JsonPropertyName("publisher")]
        public bool Publisher { get; set; }
    }

    public class Company
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
