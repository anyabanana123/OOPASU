using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Data;
using RESTFull.Infrastructure.Repository;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTFull.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class CodeISController : ControllerBase
    {
        //подключение репозитория
        private readonly Context _context;
        private readonly CodeISRepository _codeISRepository;
        private PersonRepository _personRepository;
        public CodeISController(Context context)
        {
            _context = context;
            _codeISRepository = new CodeISRepository(_context);
            _personRepository = new PersonRepository(_context);
        }
        // GET: api/CodeIS
        [HttpGet]
        public async Task<List<CodeIS>> GetCodeIS()
        {
            //return await _context.Persons.ToListAsync();
            var usernames= await _codeISRepository.GetAllAsync();
            return usernames;
        }

        // GET: api/CodeIS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodeIS>> GetCodeIS_s(int id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _codeISRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodeIS(int id, CodeIS codeIS)
        {
            Console.WriteLine("JOJO_REFERENCE_TWO");
            if (id != codeIS.ID)
            {
                return BadRequest();
            }

            await _codeISRepository.UpdateAsync(codeIS);

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task PostCodeIS(CodeIS codeIS)
        {

            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();
            // await _personRepository.AddAsync(person);
            // return CreatedAtAction("GetPerson", new { id = person.ID }, person);
            await _codeISRepository.AddAsync(codeIS);
            
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodeIS(int id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _codeISRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            //_context.Persons.Remove(person);
            //await _context.SaveChangesAsync();
            await _codeISRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool CodeISExists(int id)
        {
            return _context.CodeIS_s.Any(e => e.ID == id);
        }
    }
}


