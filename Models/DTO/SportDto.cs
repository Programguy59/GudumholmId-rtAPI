public class SportDto
{
    public string SportName { get; set; }
    public int YearlyFee { get; set; }
    public List<ActiveMemberDto> ActiveMembers { get; set; } = new List<ActiveMemberDto>();
}
public class GetSportDto
{
    public int SportId { get; set; }
    public string SportName { get; set; }
    public int YearlyFee { get; set; }
    public List<ActiveMemberDto> ActiveMembers { get; set; } = new List<ActiveMemberDto>();
}