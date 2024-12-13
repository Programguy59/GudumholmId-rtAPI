using GudumholmIdærtAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly AppDbContext _context;

    public MemberController(AppDbContext context)
    {
        _context = context;
    }

    // Get all members
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return Ok(await _context.Members.ToListAsync());
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<ActiveMember>>> GetActiveMembers()
    {
        var activeMembers = await _context.Members
            .Where(m => EF.Property<string>(m, "MemberType") == "Active")
            .Cast<ActiveMember>()
            .ToListAsync();

        return Ok(activeMembers);
    }

    [HttpGet("passive")]
    public async Task<ActionResult<IEnumerable<PassiveMember>>> GetPassiveMembers()
    {
        var passiveMembers = await _context.Members
            .Where(m => EF.Property<string>(m, "MemberType") == "Passive")
            .Cast<PassiveMember>()
            .ToListAsync();

        return Ok(passiveMembers);
    }

    // Create a new member
    [HttpPost]
    public async Task<ActionResult<Member>> CreateMember(Member createMemberDto)
    {
        if (createMemberDto == null)
        {
            return BadRequest("Invalid member data.");
        }

        // Map DTO to Member entity (automatically set MemberId to null for auto-generation)
        var member = new Member
        {
            Name = createMemberDto.Name,
            Address = createMemberDto.Address,
            CprNumber = createMemberDto.CprNumber
        };

        // Add the new member to the DbContext
        _context.Members.Add(member);

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return the newly created member (including MemberId in the response)
        return CreatedAtAction(nameof(GetMembers), new { id = member.MemberId }, member);
    }
    [HttpPost("active")]
    public async Task<ActionResult<Member>> CreateActiveMember([FromBody] ActiveMember activeMember)
    {
        if (activeMember == null)
        {
            return BadRequest("Invalid member data.");
        }

        // Add the new active member to the DbContext
        _context.Members.Add(activeMember);

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return the newly created member (including MemberId and MemberType in the response)
        return CreatedAtAction(nameof(GetMembers), new { id = activeMember.MemberId }, activeMember);
    }

    [HttpPost("passive")]
    public async Task<ActionResult<Member>> CreatePassiveMember([FromBody] PassiveMember passiveMember)
    {
        if (passiveMember == null)
        {
            return BadRequest("Invalid member data.");
        }

        // Add the new passive member to the DbContext
        _context.Members.Add(passiveMember);

        // Save changes to the database
        await _context.SaveChangesAsync();

        // Return the newly created member (including MemberId and MemberType in the response)
        return CreatedAtAction(nameof(GetMembers), new { id = passiveMember.MemberId }, passiveMember);
    }

    // Update member details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
    {
        if (id != member.MemberId) return BadRequest("Member ID mismatch.");
        _context.Entry(member).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Members.Any(m => m.MemberId == id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // Delete a member
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null) return NotFound();

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
