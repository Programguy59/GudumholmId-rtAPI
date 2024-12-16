using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GudumholmIdærtAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class ParentMemberController : ControllerBase
{
    private readonly AppDbContext _context;

    public ParentMemberController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParentMemberDto>>> GetParentMembers()
    {
        var parents = await _context.Members
            .OfType<ParentMember>()
            .Include(pm => pm.Children) // Include children in the query
            .ToListAsync();

        var parentDtos = parents.Select(pm => new ParentMemberDto
        {
            MemberId = pm.MemberId,
            Name = pm.Name,
            CprNumber = pm.CprNumber,
            Birthday = pm.Birthday,
            HouseId = pm.HouseId,
            NumberOfChildren = pm.Children.Count,
            Children = pm.Children.Select(c => new ChildMemberDto
            {
                MemberId = c.MemberId,
                Name = c.Name,
                Birthday = c.Birthday,
                // Map any other properties for the child if needed
            }).ToList()
        }).ToList();

        return Ok(parentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParentMemberDto>> GetParentMember(int id)
    {
        var parent = await _context.Members
            .OfType<ParentMember>()
            .Include(pm => pm.Children) // Include children in the query
            .FirstOrDefaultAsync(pm => pm.MemberId == id);

        if (parent == null)
        {
            return NotFound();
        }

        var parentDto = new ParentMemberDto
        {
            MemberId = parent.MemberId,
            Name = parent.Name,
            CprNumber = parent.CprNumber,
            Birthday = parent.Birthday,
            HouseId = parent.HouseId,
            NumberOfChildren = parent.Children.Count,
            Children = parent.Children.Select(c => new ChildMemberDto
            {
                MemberId = c.MemberId,
                Name = c.Name,
                Birthday = c.Birthday,
                // Map any other properties for the child if needed
            }).ToList()
        };

        return Ok(parentDto);
    }
    // Create a new parent member
    [HttpPost]
    public async Task<ActionResult<ParentMemberDto>> CreateParentMember([FromBody] PostParentMemberDto postParentMemberDto)
    {
        if (postParentMemberDto == null)
        {
            return BadRequest("Invalid parent member data.");
        }

        // Create the new ParentMember based on the POST DTO data (Id will be generated automatically)
        var parentMember = new ParentMember
        {
            Name = postParentMemberDto.Name,
            CprNumber = postParentMemberDto.CprNumber,
            Birthday = postParentMemberDto.Birthday,
            HouseId = postParentMemberDto.HouseId,
            Children = new List<Member>() // Start with an empty list of children
        };

        // Add the parent member to the database
        _context.Members.Add(parentMember);
        await _context.SaveChangesAsync();

        // Set up a ParentMemberDto response to return
        var parentDto = new ParentMemberDto
        {
            MemberId = parentMember.MemberId,
            Name = parentMember.Name,
            CprNumber = parentMember.CprNumber,
            Birthday = parentMember.Birthday,
            HouseId = parentMember.HouseId,
            NumberOfChildren = parentMember.Children.Count
        };

        return CreatedAtAction(nameof(GetParentMember), new { id = parentMember.MemberId }, parentDto);
    }

    // Delete a parent member by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParentMember(int id)
    {
        var parent = await _context.Members
            .OfType<ParentMember>()
            .Include(pm => pm.Children)
            .FirstOrDefaultAsync(pm => pm.MemberId == id);

        if (parent == null)
        {
            return NotFound();
        }

        // Remove all children associated with this parent before deleting the parent
        _context.Members.Remove(parent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{parentId}/children")]
    public async Task<IActionResult> AddChildToParent(int parentId, [FromBody] AddChildRequest addChildRequest)
    {
        var parent = await _context.Members
            .OfType<ParentMember>()
            .FirstOrDefaultAsync(pm => pm.MemberId == parentId);

        if (parent == null)
        {
            return NotFound("Parent member not found.");
        }

        var child = await _context.Members.FindAsync(addChildRequest.ChildId);

        if (child == null)
        {
            return NotFound("Child member not found.");
        }

        // Add the child to the parent
        parent.Children.Add(child);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DTO for posting a child ID
    public class AddChildRequest
    {
        public int ChildId { get; set; }
    }

    [HttpDelete("{parentId}/children/{childId}")]
    public async Task<IActionResult> RemoveChildFromParent(int parentId, int childId)
    {
        var parent = await _context.Members
            .OfType<ParentMember>()
            .Include(pm => pm.Children)
            .FirstOrDefaultAsync(pm => pm.MemberId == parentId);

        if (parent == null)
        {
            return NotFound("Parent member not found.");
        }

        var child = parent.Children.FirstOrDefault(c => c.MemberId == childId);

        if (child == null)
        {
            return NotFound("Child member not found.");
        }

        parent.Children.Remove(child);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
