namespace GudumholmIdærtAPI.Models
{
    public class ActiveMemberSport
    {
        public int ActiveMemberId { get; set; }
        public ActiveMember ActiveMember { get; set; }

        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
