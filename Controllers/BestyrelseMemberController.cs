using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/members/bestyrelse")]
public class BestyrelseMemberController : ControllerBase
{
    private readonly AppDbContext _context;

    public BestyrelseMemberController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BestyrelseMember>>> GetBestyrelseMembers()
    {
        var members = await _context.Members
            .OfType<BestyrelseMember>()
            .Include(m => m.Sport) // Include associated sport
            .ToListAsync();

        return Ok(members);
    }

    [HttpPost]
    public async Task<ActionResult<BestyrelseMember>> CreateBestyrelseMember([FromBody] BestyrelseMemberDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid member data.");

        var house = await _context.Houses.FindAsync(dto.HouseId);
        if (house == null)
            return BadRequest("House not found.");

        Sport sport = null;
        if (dto.SportId.HasValue)
        {
            sport = await _context.Sports.FindAsync(dto.SportId);
            if (sport == null)
                return BadRequest("Sport not found.");
        }

        var member = new BestyrelseMember
        {
            Name = dto.Name,
            CprNumber = dto.CprNumber,
            HouseId = dto.HouseId,
            Birthday = dto.Birthday,
            SportId = dto.SportId,
            Sport = sport
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBestyrelseMembers), new { id = member.MemberId }, member);
    }

}