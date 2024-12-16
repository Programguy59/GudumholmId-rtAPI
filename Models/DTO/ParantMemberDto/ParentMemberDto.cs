public class ParentMemberDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string CprNumber { get; set; }
    public DateTime Birthday { get; set; }
    public int HouseId { get; set; }
    public int NumberOfChildren { get; set; }

    public List<ChildMemberDto> Children { get; set; } = new List<ChildMemberDto>(); 
}

public class ChildMemberDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
}

public class PostParentMemberDto
{
    public string Name { get; set; }
    public string CprNumber { get; set; }
    public DateTime Birthday { get; set; }
    public int HouseId { get; set; }
}
