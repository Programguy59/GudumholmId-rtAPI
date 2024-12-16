using GudumholmIdærtAPI.Models;
using GudumholmIdærtAPI.Models.DTO.PassiveMemberDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GudumholmIdærtAPI.Controllers
{
    [ApiController]
    [Route("api/members/passive")]
    public class PassiveMemberController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PassiveMemberController(AppDbContext context)
        {
            _context = context;
        }

        // Get all passive members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassiveMember>>> GetPassiveMembers()
        {
            var passiveMembers = await _context.Members
                .OfType<PassiveMember>()
                .ToListAsync();

            return Ok(passiveMembers);
        }

        [HttpGet("passive/{id}/time")]
        public async Task<ActionResult<string>> GetTimeSincePassive(int id)
        {
            var member = await _context.Members
                .OfType<PassiveMember>()
                .FirstOrDefaultAsync(m => m.MemberId == id);

            if (member == null)
            {
                return NotFound();
            }

            var timeSincePassive = DateTime.Now - member.DateBecamePassive;

            return Ok($"This member has been passive for {timeSincePassive.Days} days.");
        }

        // Get a specific passive member by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PassiveMember>> GetPassiveMember(int id)
        {
            var passiveMember = await _context.Members
                .OfType<PassiveMember>()
                .FirstOrDefaultAsync(pm => pm.MemberId == id);

            if (passiveMember == null)
                return NotFound();

            return Ok(passiveMember);
        }

        [HttpPost("passive")]
        public async Task<ActionResult<PassiveMember>> CreatePassiveMember([FromBody] PassiveMemberDto passiveMemberDto)
        {
            if (passiveMemberDto == null)
            {
                return BadRequest("Invalid member data.");
            }

            // Ensure the House exists based on the HouseId provided
            var house = await _context.Houses.FindAsync(passiveMemberDto.HouseId);
            if (house == null)
            {
                return BadRequest("House not found.");
            }

            // Create a new PassiveMember instance
            var passiveMember = new PassiveMember
            {
                Name = passiveMemberDto.Name,
                CprNumber = passiveMemberDto.CprNumber,
                HouseId = passiveMemberDto.HouseId,
                Birthday = passiveMemberDto.Birthday,
                DateBecamePassive = DateTime.Now
            };

            // Add the new passive member to the DbContext
            _context.Members.Add(passiveMember);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return the newly created passive member
            return CreatedAtAction(nameof(GetPassiveMember), new { id = passiveMember.MemberId }, passiveMember);
        }

        // Delete a passive member
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassiveMember(int id)
        {
            var passiveMember = await _context.Members
                .OfType<PassiveMember>()
                .FirstOrDefaultAsync(pm => pm.MemberId == id);

            if (passiveMember == null)
                return NotFound();

            _context.Members.Remove(passiveMember);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper: Check if a passive member exists
        private bool PassiveMemberExists(int id)
        {
            return _context.Members.OfType<PassiveMember>().Any(pm => pm.MemberId == id);
        }
    }
}
