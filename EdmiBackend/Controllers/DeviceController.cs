using EdmiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EdmiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        public DeviceController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<DeviceController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var deviceList = await _context.EdmiDevice.ToListAsync();
                return Ok(deviceList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST api/<DeviceController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Device device)
        {
            try
            {
                _context.Add(device);
                var validateExistence = await _context.EdmiDevice.AnyAsync(x => x.SerialNumber == device.SerialNumber);
                
                if (!validateExistence)
                {                    
                    await _context.SaveChangesAsync();
                    return Ok(device);
                }
                else
                {
                    return BadRequest(new { message = "Serial Number " + device.SerialNumber + " already exists" });
                }                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT api/<DeviceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Device device)
        {
            try
            {
                if (id != device.Id)
                {
                    return NotFound();
                }
                _context.Update(device);
                var validateExistence = await _context.EdmiDevice.AnyAsync(x => x.SerialNumber == device.SerialNumber);

                if (validateExistence)
                {
                    return BadRequest(new { message = "Serial Number " + device.SerialNumber + " already exists" });
                }
                await _context.SaveChangesAsync();
                return Ok(new { message = "The device was successfully upgraded" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<DeviceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var device = await _context.EdmiDevice.FindAsync(id);

                if (device == null)
                {
                    return NotFound();
                }

                _context.EdmiDevice.Remove(device);
                await _context.SaveChangesAsync();
                return Ok(new { message = "The device was successfully removed." });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
