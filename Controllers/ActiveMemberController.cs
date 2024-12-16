using GudumholmIdærtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ActiveMemberController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActiveMemberController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/ActiveMember
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActiveMemberDto>>> GetActiveMembers()
    {
        var activeMembers = await _context.ActiveMembers
            .Include(am => am.House)
            .Include(am => am.ActiveMemberSports)
            .ThenInclude(ams => ams.Sport)
            .ToListAsync();

        var activeMemberDtos = activeMembers.Select(am => new ActiveMemberDto
        {
            MemberId = am.MemberId,
            Name = am.Name,
            CprNumber = am.CprNumber,
            Birthday = am.Birthday, 
            HouseId = am.HouseId,
            HouseName = am.House.HouseName,
            Sports = am.ActiveMemberSports.Select(ams => new SportDto
            {
                SportName = ams.Sport.SportName,
                YearlyFee = ams.Sport.YearlyFee
            }).ToList()
        }).ToList();

        return Ok(activeMemberDtos);
    }

    // GET: api/ActiveMember/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ActiveMemberDto>> GetActiveMember(int id)
    {
        var activeMember = await _context.ActiveMembers
            .Include(am => am.House)
            .Include(am => am.ActiveMemberSports)
            .ThenInclude(ams => ams.Sport)
            .FirstOrDefaultAsync(am => am.MemberId == id);

        if (activeMember == null)
        {
            return NotFound();
        }

        var activeMemberDto = new ActiveMemberDto
        {
            MemberId = activeMember.MemberId,
            Name = activeMember.Name,
            CprNumber = activeMember.CprNumber,
            Birthday = activeMember.Birthday,
            HouseId = activeMember.HouseId,
            HouseName = activeMember.House.HouseName,
            Sports = activeMember.ActiveMemberSports.Select(ams => new SportDto
            {
                SportName = ams.Sport.SportName,
                YearlyFee = ams.Sport.YearlyFee
            }).ToList()
        };

        return Ok(activeMemberDto);
    }

    // POST: api/ActiveMember
    [HttpPost]
    public async Task<ActionResult<ActiveMemberDto>> CreateActiveMember([FromBody] ActiveMemberCreateDto activeMemberCreateDto)
    {
        if (activeMemberCreateDto == null)
        {
            return BadRequest("Invalid member data.");
        }

        var house = await _context.Houses.FindAsync(activeMemberCreateDto.HouseId);
        if (house == null)
        {
            return BadRequest("House not found.");
        }

        var activeMember = new ActiveMember
        {
            Name = activeMemberCreateDto.Name,
            CprNumber = activeMemberCreateDto.CprNumber,
            Birthday = activeMemberCreateDto.Birthday,
            HouseId = activeMemberCreateDto.HouseId
        };

        _context.ActiveMembers.Add(activeMember);
        await _context.SaveChangesAsync(); 

        if (activeMemberCreateDto.SportNames != null && activeMemberCreateDto.SportNames.Any())
        {
            foreach (var sportName in activeMemberCreateDto.SportNames)
            {
                var sport = await _context.Sports.FirstOrDefaultAsync(s => s.SportName == sportName);

                if (sport == null)
                {
                    return BadRequest($"Sport '{sportName}' not found.");
                }

                var activeMemberSport = new ActiveMemberSport
                {
                    ActiveMemberId = activeMember.MemberId,
                    SportId = sport.SportId
                };

                _context.ActiveMemberSports.Add(activeMemberSport);
            }

            await _context.SaveChangesAsync();
        }

        var createdActiveMemberDto = new ActiveMemberDto
        {
            MemberId = activeMember.MemberId,
            Name = activeMember.Name,
            CprNumber = activeMember.CprNumber,
            Birthday = activeMember.Birthday,
            HouseId = activeMember.HouseId,
            HouseName = house.HouseName,
            Sports = activeMemberCreateDto.SportNames.Select(sportName => new SportDto
            {
                SportName = sportName,
                YearlyFee = 100
            }).ToList()
        };

        return CreatedAtAction(nameof(GetActiveMember), new { id = activeMember.MemberId }, createdActiveMemberDto);
    }

    // DELETE: api/ActiveMember/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActiveMember(int id)
    {
        var activeMember = await _context.ActiveMembers.FindAsync(id);

        if (activeMember == null)
        {
            return NotFound();
        }

        _context.ActiveMembers.Remove(activeMember);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
