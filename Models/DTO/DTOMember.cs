namespace GudumholmIdærtAPI.Models.DTO
{
    public class DTOMember
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CprNumber { get; set; }
    }
}
public class ActiveMemberDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string CprNumber { get; set; }
    public bool IsActive { get; set; }
}

public class PassiveMemberDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string CprNumber { get; set; }
    public bool IsPassive { get; set; }
}