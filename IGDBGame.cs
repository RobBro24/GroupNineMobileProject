using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace GroupNineMobileProject
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("rating")]
        public double? Rating { get; set; }

        [JsonPropertyName("involved_companies")]
        public List<InvolvedCompany> InvolvedCompanies { get; set; }
    }

    public class InvolvedCompany
    {
        [JsonPropertyName("company")]
        public Company Company { get; set; }

        [JsonPropertyName("publisher")]
        public bool Publisher { get; set; }
    }

    public class Company
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

