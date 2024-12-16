using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GudumholmIdærtAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly AppDbContext _context;

    public MemberController(AppDbContext context)
    {
        _context = context;
    }

    // Get all members with their basic attributes and type
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
    {
        // Retrieve all members from the database
        var members = await _context.Members
            .Include(m => m.House)  // Include House information
            .ToListAsync();

        // Convert the list of members into a list of MemberDto
        var memberDtos = members.Select(m => new MemberDto
        {
            MemberId = m.MemberId,
            Name = m.Name,
            CprNumber = m.CprNumber,
            Birthday = m.Birthday,
            HouseId = m.HouseId,
            MemberType = m.GetType().Name  // Dynamically get the type of the member (ActiveMember, PassiveMember, etc.)
        }).ToList();

        return Ok(memberDtos);
    }
}
