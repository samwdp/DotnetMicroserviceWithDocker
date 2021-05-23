using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserService.Data;
using UserService.Entities;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServiceContext _context;
        private readonly IQueuePublisher _queuePublisher;

        public UserController(UserServiceContext context, IQueuePublisher queuePublisher)
        {
            _context = context;
            _queuePublisher = queuePublisher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.Id,
                name = user.Name
            });
            _queuePublisher.PublishMessage("user.update", integrationEventData);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var integrationEventData = JsonConvert.SerializeObject(new
            {
                id = user.Id,
                name = user.Name
            });
            _queuePublisher.PublishMessage("user.add", integrationEventData);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
    }
}
