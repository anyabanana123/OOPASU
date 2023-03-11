using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure.Data;
using RESTFull.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    public class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            //Используем базу обычную базу данных, не в памяти
            //Имя тестовой базы данных должно отличатсья от базы данных проекта
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test0")
                .Options;

            _context = new Context(contextOptions);


            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
            var person1 = new Person
            {
                Familia="Kulichkina",
                Name="Anna",
                Otchestvo="Djulustanovna",
                DateBirth="05.10.2002",
                City="Yakutsk",
            };
            person1.AddCodeIS(new CodeIS { Type = "GosUslugi", Code = 3334333 });
            person1.AddCodeIS(new CodeIS { Type = "Ebibl", Code = 11111 });

            _context.Persons.Add(person1);
            _context.SaveChanges();
            //Запрещаем отслеживание (разрываем связи с БД)
            _context.ChangeTracker.Clear();
        }

        public PersonRepository PersonRepository
        {
            get
            {
                return new PersonRepository(_context);
            }
        }
    }
}
