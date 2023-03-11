using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Numerics;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Data;

namespace RESTFull.Infrastructure.Repository
{
    public class PersonRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public PersonRepository(Context context) // конструктор
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Person>> GetAllAsync() // получение всех
        {
           // return await _context.Persons.OrderBy(p => p.Name).ToListAsync();
           return await _context.Persons.OrderBy(p => p.Name).ToListAsync();
        }
        public async Task<Person> GetByIdAsync(int id) //получение конкретного чувачка
        {
            return await _context.Persons.Where(p => p.ID == id).FirstOrDefaultAsync();
        }

        public async Task<Person?> GetPersonAsync(string name)
        {
            return await _context.Persons.Where(p => p.Name == name).Include(p => p.CodeIS_s).FirstOrDefaultAsync();
        }
        public async Task<Person> GetByNameAsync(string name)
        {
            return await _context.Persons.Where(p => p.Name == name).FirstOrDefaultAsync();
        }
        public async Task AddAsync(Person person) // добавление
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Person person) //редактирование
        {
            var existPerson = GetByIdAsync(person.ID).Result;
            if (existPerson != null)
            {
                _context.Entry(existPerson).CurrentValues.SetValues(person);
                foreach (var codeIS in person.CodeIS_s)
                {
                    var existcodeIS = existPerson.CodeIS_s.FirstOrDefault(p => p.ID == codeIS.ID);
                    if (existcodeIS == null)
                    {
                        existPerson.CodeIS_s.Add(codeIS);
                    }
                    else
                    {
                        _context.Entry(existcodeIS).CurrentValues.SetValues(codeIS);
                    }
                }
                foreach (var codeIS in existPerson.CodeIS_s)
                {
                    if (!person.CodeIS_s.Any(pn => pn.ID == codeIS.ID))
                    {
                        _context.Remove(codeIS);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Person person = await _context.Persons.FindAsync(id);
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
