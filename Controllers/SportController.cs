using GudumholmIdærtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SportController : ControllerBase
{
    private readonly AppDbContext _context;

    public SportController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetSportDto>> GetSport(int id)
    {
        var sport = await _context.Sports
            .Include(s => s.ActiveMemberSports)
            .ThenInclude(ams => ams.ActiveMember)
            .FirstOrDefaultAsync(s => s.SportId == id);

        if (sport == null)
        {
            return NotFound();
        }

        var sportDto = new GetSportDto
        {
            SportId = sport.SportId,
            SportName = sport.SportName,
            YearlyFee = sport.YearlyFee,
            ActiveMembers = sport.ActiveMemberSports.Select(ams => new ActiveMemberDto
            {
                MemberId = ams.ActiveMember.MemberId,
                Name = ams.ActiveMember.Name,
                CprNumber = ams.ActiveMember.CprNumber,
                Birthday = ams.ActiveMember.Birthday,
                HouseId = ams.ActiveMember.HouseId
            }).ToList()
        };

        return Ok(sportDto);
    }

    // POST: api/Sport
    [HttpPost]
    public async Task<ActionResult<Sport>> CreateSport([FromBody] SportDto sportDto)
    {
        var existingSport = await _context.Sports
            .FirstOrDefaultAsync(s => s.SportName == sportDto.SportName);

        if (existingSport != null)
        {
            return Conflict("Sport with this name already exists.");
        }

        var sport = new Sport
        {
            SportName = sportDto.SportName,
            YearlyFee = sportDto.YearlyFee
        };

        _context.Sports.Add(sport);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSport), new { id = sport.SportId }, sport);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetSportDto>>> GetSports()
    {
        var sports = await _context.Sports
            .Include(s => s.ActiveMemberSports)
            .ThenInclude(ams => ams.ActiveMember)
            .ToListAsync();

        var sportDtos = sports.Select(sport => new GetSportDto
        {
            SportId = sport.SportId,
            SportName = sport.SportName,
            YearlyFee = sport.YearlyFee,
            ActiveMembers = sport.ActiveMemberSports.Select(ams => new ActiveMemberDto
            {
                MemberId = ams.ActiveMember.MemberId,
                Name = ams.ActiveMember.Name,
                CprNumber = ams.ActiveMember.CprNumber,
                Birthday = ams.ActiveMember.Birthday,
                HouseId = ams.ActiveMember.HouseId
            }).ToList()
        }).ToList();

        return Ok(sportDtos);
    }

    // PUT: api/Sport/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSport(int id, [FromBody] SportDto sportDto)
    {
        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
        {
            return NotFound();
        }

        sport.SportName = sportDto.SportName;
        sport.YearlyFee = sportDto.YearlyFee;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Sport/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSport(int id)
    {
        var sport = await _context.Sports.FindAsync(id);
        if (sport == null)
        {
            return NotFound();
        }

        _context.Sports.Remove(sport);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
