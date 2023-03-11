using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RESTFull.Domain.Model;
using RESTFull.Infrastructure;
using System.Numerics;


namespace TestProject1
{
    public class TestPersonRepository
    {
        [Fact]
        //Тест, проверяющий, что база данных создалась
        public void VoidTest()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;

            Assert.Equal(1, 1);
        }

        [Fact]
        public void TestAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = new Person {
                Familia = "Rebrova",
                Name = "Tatiana",
                Otchestvo = "Ivanovna",
                DateBirth = "29.05.2002",
                City = "Yakutsk",
            };

            personRepository.AddAsync(person).Wait();
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();

            Assert.True(personRepository.GetAllAsync().Result.Count == 2);
            Assert.Equal("Tatiana", personRepository.GetByNameAsync("Tatiana").Result.Name);
            Assert.Equal("Anna", personRepository.GetByNameAsync("Anna").Result.Name);
            Assert.Equal(2, personRepository.GetByNameAsync("Anna").Result.CodeISCount);
        }

        [Fact]
        public void TestUpdateAdd()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetByNameAsync("Anna").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();
            person.Name = "Tatiana";
            var CodeISNumber = new CodeIS { Type = "GosUslugi", Code = 3334333 };
            person.AddCodeIS(CodeISNumber);

            personRepository.UpdateAsync(person).Wait();

            Assert.Equal("Tatiana", personRepository.GetByNameAsync("Tatiana").Result.Name);
            Assert.Equal(3, personRepository.GetByNameAsync("Tatiana").Result.CodeISCount);
        }

        [Fact]
        public void TestUpdateDelete()
        {
            var testHelper = new TestHelper();
            var personRepository = testHelper.PersonRepository;
            var person = personRepository.GetByNameAsync("Anna").Result;
            //Запрещаем отслеживание сущностей (разрываем связи с БД)
            personRepository.ChangeTrackerClear();
            person.RemoveAt(0);

            personRepository.UpdateAsync(person).Wait();

            Assert.Equal(1, personRepository.GetByNameAsync("Anna").Result.CodeISCount);
        }
    }
}
