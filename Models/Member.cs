using GudumholmIdærtAPI.Models;
using System.Text.Json.Serialization;

public class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string CprNumber { get; set; }
    public int HouseId { get; set; }
    public DateTime Birthday { get; set; }

    [JsonIgnore]
    public House House { get; set; }
}

public class ActiveMember : Member
{
    [JsonIgnore]
    public ICollection<ActiveMemberSport> ActiveMemberSports { get; set; }
}

public class PassiveMember : Member
{
    public DateTime DateBecamePassive { get; set; }
}

public class BestyrelseMember : Member
{
    public int? SportId { get; set; } 
    public Sport Sport { get; set; } 
}
public class ParentMember : Member
{
    public ICollection<Member> Children { get; set; } = new List<Member>();
}
