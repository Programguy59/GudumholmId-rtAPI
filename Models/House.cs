using System.Text.Json.Serialization;

namespace GudumholmIdærtAPI.Models
{
    public class House
    {
        public int HouseId { get; set; }
        public string HouseName { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public ICollection<Member> Members { get; set; } = new List<Member>();
    }
}
