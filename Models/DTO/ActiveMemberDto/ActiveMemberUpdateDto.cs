public class ActiveMemberUpdateDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string CprNumber { get; set; }
    public int HouseId { get; set; }  // Foreign Key to House
    public List<string> SportsNames { get; set; }  // List of sports names
}
