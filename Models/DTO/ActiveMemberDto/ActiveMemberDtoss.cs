public class ActiveMemberDto
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string CprNumber { get; set; }
    public int HouseId { get; set; }
    public string HouseName { get; set; }
    public DateTime Birthday { get; set; }

    public List<SportDto> Sports { get; set; }
}