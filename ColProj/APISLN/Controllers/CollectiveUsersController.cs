using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollectiveUsersController : ControllerBase
    {
        private readonly DataContext _context;

        public CollectiveUsersController(DataContext context)
        {
            _context = context;
        }

        // GET: /CollectiveUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectiveUserDto>>> GetCollectiveUsers()
        {
            //Mapping to Dto
           List<CollectiveUser> templist = await _context.CollectiveUsers.ToListAsync();

           List<CollectiveUserDto> outlist = new List<CollectiveUserDto>();

            foreach (var item in templist)
            {
                var dto = new CollectiveUserDto() { CollectiveId = item.CollectiveId, UserId = item.UserId };
                outlist.Add(dto);
            }
            return outlist;
        }

        // GET: /CollectiveUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectiveUser>> GetCollectiveUser(int id)
        {
            var collectiveUser = await _context.CollectiveUsers.FindAsync(id);

            if (collectiveUser == null)
            {
                return NotFound();
            }

            return collectiveUser;
        }


        [HttpGet("RelevantCollectives/{id}")]
        public async Task<IEnumerable<Collective>> GetRelevantCollectives(int id)
        {
            var collectivesData = new CollectivesData();
            collectivesData.CollectiveUsers = await _context.CollectiveUsers.Include(i => i.Collective).Include(i => i.User).ToListAsync();
            var collectivelist = new List<Collective>();
            foreach (var item in collectivesData.CollectiveUsers)
            {
                if (item.UserId == id)
                {
                    collectivelist.Add(_context.Collectives.Find(item.CollectiveId));
                }
            }
            return collectivelist;
        }

        [HttpGet("RelevantUsers/{id}")]
        public async Task<IEnumerable<User>> GetRelevantUsers(int id)
        {
            var collectivesData = new CollectivesData();
            collectivesData.CollectiveUsers = await _context.CollectiveUsers.Include(i => i.Collective).Include(i => i.User).ToListAsync();
            var userlist = new List<User>();
            foreach (var item in collectivesData.CollectiveUsers)
            {
                if (item.CollectiveId == id)
                {
                    userlist.Add(_context.Users.Find(item.UserId));
                }
            }
            return userlist;
        }

        // PUT: /CollectiveUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollectiveUser(int id, CollectiveUser collectiveUser)
        {
            if (id != collectiveUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(collectiveUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectiveUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: /CollectiveUsers
        [HttpPost]
        public async Task<ActionResult<CollectiveUser>> PostCollectiveUser(CollectiveUser collectiveUser)
        {
            _context.CollectiveUsers.Add(collectiveUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CollectiveUserExists(collectiveUser.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCollectiveUser", new { id = collectiveUser.UserId }, collectiveUser);
        }

        // DELETE: /CollectiveUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CollectiveUser>> DeleteCollectiveUser(int id)
        {
            var collectiveUser = await _context.CollectiveUsers.FindAsync(id);
            if (collectiveUser == null)
            {
                return NotFound();
            }

            _context.CollectiveUsers.Remove(collectiveUser);
            await _context.SaveChangesAsync();

            return collectiveUser;
        }

        // DELETE: /CollectiveUsers/5+5
        [HttpDelete("LeaveCollective/{collectiveid}+{userid}")]
        public async Task<ActionResult<CollectiveUser>> LeaveCollective(int collectiveid, int userid)
        {
            var collectiveUserList = await _context.CollectiveUsers.ToListAsync();
            CollectiveUser collectiveUser = new CollectiveUser();
            foreach (var item in collectiveUserList)
            {
                if (item.CollectiveId == collectiveid && item.UserId == userid)
                {
                    collectiveUser = item;
                    _context.CollectiveUsers.Remove(item);
                }
            }
            await _context.SaveChangesAsync();

            if (collectiveUser == null)
            {
                return NotFound();
            }
            return collectiveUser;

        }

        private bool CollectiveUserExists(int id)
        {
            return _context.CollectiveUsers.Any(e => e.UserId == id);
        }

    }
}
