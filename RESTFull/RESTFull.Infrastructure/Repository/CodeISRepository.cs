using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Numerics;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Data;

namespace RESTFull.Infrastructure.Repository
{
    public class CodeISRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public CodeISRepository(Context context) // конструктор
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<CodeIS>> GetAllAsync() // получение всех
        {
            return await _context.CodeIS_s.OrderBy(p => p.ID).ToListAsync();
        }
        
        public async Task<CodeIS> GetByIdAsync(int id) //получение конкретного чувачка
        {
            return await _context.CodeIS_s.Where(p => p.ID == id).FirstOrDefaultAsync();
        }

        public async Task<CodeIS> GetCodeISAsync(string type)
        {
            return await _context.CodeIS_s.Where(p => p.Type == type).Include(p => p.Person).FirstOrDefaultAsync();
        }
        public async Task<CodeIS> GetByNameAsync(string type)
        {
            return await _context.CodeIS_s
                .Where(p => p.Type == type)
                .FirstOrDefaultAsync();
        }
        public async Task AddAsync(CodeIS codeIS) // добавление
        {
            _context.CodeIS_s.Add(codeIS);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(CodeIS codeIS) //редактирование
        {
            var existPerson = GetByIdAsync(codeIS.ID).Result;
            if (existPerson != null)
            {
                _context.Entry(existPerson).CurrentValues.SetValues(codeIS);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            CodeIS codeIS = await _context.CodeIS_s.FindAsync(id);
            _context.Remove(codeIS);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
