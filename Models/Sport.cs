using GudumholmIdærtAPI.Models;

public class Sport
{
    public int SportId { get; set; }
    public string SportName { get; set; }
    public int YearlyFee { get; set; }
    public ICollection<ActiveMemberSport> ActiveMemberSports { get; set; }
}