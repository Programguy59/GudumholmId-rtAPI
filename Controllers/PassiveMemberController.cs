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

            var house = await _context.Houses.FindAsync(passiveMemberDto.HouseId);
            if (house == null)
            {
                return BadRequest("House not found.");
            }

            var passiveMember = new PassiveMember
            {
                Name = passiveMemberDto.Name,
                CprNumber = passiveMemberDto.CprNumber,
                HouseId = passiveMemberDto.HouseId,
                Birthday = passiveMemberDto.Birthday,
                DateBecamePassive = DateTime.Now
            };

            _context.Members.Add(passiveMember);

            await _context.SaveChangesAsync();

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

        private bool PassiveMemberExists(int id)
        {
            return _context.Members.OfType<PassiveMember>().Any(pm => pm.MemberId == id);
        }
    }
}
