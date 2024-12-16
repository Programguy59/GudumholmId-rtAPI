using GudumholmIdærtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class HouseController : ControllerBase
{
    private readonly AppDbContext _context;

    public HouseController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/House
    [HttpPost]
    public async Task<ActionResult<House>> CreateHouse([FromBody] HouseDto houseDto)
    {
        if (houseDto == null)
        {
            return BadRequest("Invalid house data.");
        }

        var house = new House
        {
            HouseName = houseDto.HouseName,
            Address = houseDto.Address
        };

        _context.Houses.Add(house);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHouse), new { id = house.HouseId }, house);
    }

    // GET: api/House/5
    [HttpGet("{id}")]
    public async Task<ActionResult<House>> GetHouse(int id)
    {
        var house = await _context.Houses.FindAsync(id);

        if (house == null)
        {
            return NotFound();
        }

        return house;
    }

    // GET: api/House
    [HttpGet]
    public async Task<ActionResult<IEnumerable<House>>> GetHouses()
    {
        return await _context.Houses.ToListAsync();
    }

    // PUT: api/House/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHouse(int id, [FromBody] HouseDto houseDto)
    {
        var house = await _context.Houses.FindAsync(id);
        if (house == null)
        {
            return NotFound();
        }

        house.HouseName = houseDto.HouseName;
        house.Address = houseDto.Address;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/House/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHouse(int id)
    {
        var house = await _context.Houses.FindAsync(id);
        if (house == null)
        {
            return NotFound();
        }

        _context.Houses.Remove(house);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
