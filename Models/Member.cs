public class Member
{
    public int MemberId { get; set; }   
    public string Name { get; set; }    
    public string Address { get; set; } 
    public string CprNumber { get; set; } 
}

public class ActiveMember : Member
{
}

public class PassiveMember : Member
{
    public string TimeSincePassive { get; set; }
}
