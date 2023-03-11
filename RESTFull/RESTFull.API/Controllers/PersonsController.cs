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
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        //подключение репозитория
        private readonly Context _context;
        private PersonRepository _personRepository;
        public PersonsController(Context context)
        {
            _context = context;
            _personRepository = new PersonRepository(_context);
        }
        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>?>> GetPersons()
        {
            //return await _personRepository.ToListAsync();
            var usernames=await _personRepository.GetAllAsync();
            return usernames;
            //return await _context.Persons.ToListAsync();

        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.ID)
            {
                return BadRequest();
            }

            await _personRepository.UpdateAsync(person);

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            //_context.Persons.Add(person);
            //await _context.SaveChangesAsync();
            await _personRepository.AddAsync(person);
            return CreatedAtAction("GetPerson", new { id = person.ID }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            Console.WriteLine("deleted");
            //var person = await _context.Persons.FindAsync(id);
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            //_context.Persons.Remove(person);
            //await _context.SaveChangesAsync();
            await _personRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Persons.Any(e => e.ID == id);
        }
            }
        }


