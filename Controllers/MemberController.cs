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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
    {
        var members = await _context.Members
            .Include(m => m.House)  
            .ToListAsync();

        var memberDtos = members.Select(m => new MemberDto
        {
            MemberId = m.MemberId,
            Name = m.Name,
            CprNumber = m.CprNumber,
            Birthday = m.Birthday,
            HouseId = m.HouseId,
            MemberType = m.GetType().Name 
        }).ToList();

        return Ok(memberDtos);
    }
}
