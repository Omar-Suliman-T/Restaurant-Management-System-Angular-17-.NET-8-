using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Person;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Interfaces;
using System;

namespace My_Resturant.Implementations
{
    public class PersonServices : IPersonServices
    {
        private readonly RestDbContext _context;
        public PersonServices(RestDbContext context)
        {
            _context = context;
        }
        public async Task CreatePerson(CreatePersonDTOs person)
        {
            LookupItem role = new LookupItem();
            if (!string.IsNullOrEmpty(person.role))
            {
                 role = await _context.LookupItems
                     .FirstOrDefaultAsync(li =>
                         li.name == person.role &&
                         li.lookupTypeID == 4);
            }
       
            var customerRole = await _context.LookupItems
                .FirstOrDefaultAsync(li =>
                    li.name == "customer" &&
                    li.lookupTypeID == 4);

            
            Person thePerson = new Person()
            {
                email = EncryptionHelper.GenerateSHA384String(person.email),
                password = EncryptionHelper.GenerateSHA384String(person.password),
                firstName = person.firstName,
                lastName = person.lastName,
                phone = person.phone,
                role = string.IsNullOrEmpty(person.role)? customerRole.id: role.id, 
                isActive = person.isActive,
            };

            await _context.AddAsync(thePerson);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
            var person = await _context.People.FirstOrDefaultAsync(person => person.id == id);
            _context.Remove(person);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetPersonDTOs>> GetAllPeople()
        {
            var peopleWithRoles = await (
                from person in _context.People
                join role in _context.LookupItems on person.role equals role.id
                select new GetPersonDTOs
                {
                    id = person.id,
                    email = person.email,
                    firstName = person.firstName,
                    lastName = person.lastName,
                    phone = person.phone,
                    isActive = person.isActive,
                    creationDate = person.creationDate,
                    role = role.name
                })
                .ToListAsync();

            return peopleWithRoles;
        }
        public async Task<GetPersonDTOs> GetPersonById(int id)
        {
            var thePerson = await (
                 from person in _context.People
                 join role in _context.LookupItems on person.role equals role.id
                 where person.id == id
                 select new GetPersonDTOs
                 {
                     id = person.id,
                     email = person.email,
                     firstName = person.firstName,
                     lastName = person.lastName,
                     phone = person.phone,
                     isActive = person.isActive,
                     role = role.name
                 }).FirstOrDefaultAsync();
            return thePerson;

        }
        public async Task<Person?> GetPersonByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var person=await _context.People.FirstOrDefaultAsync(p=>p.email == email);
            return person;

        }
        public async Task<string> UpdatePerson(int id, UpdatePersonDTOs person)
        {

            var Role = await _context.LookupItems
                .FirstOrDefaultAsync(li =>
                    li.name == person.role &&
                    li.lookupTypeID == 4);

            int changed = 0;
            var theperson = await _context.People.SingleOrDefaultAsync(x => x.id == id);
            if (person == null)
            {
                throw new Exception($"coudn't found any person with the id: {id}");
            }
            else
            {
                if (person.email != "emailNotChanged")
                {
                    theperson.email = EncryptionHelper.GenerateSHA384String(person.email);
                    changed++;
                }
                if (person.firstName != null)
                {
                    theperson.firstName = person.firstName;
                    changed++;
                }
                if (person.lastName != null)
                {
                    theperson.lastName = person.lastName;
                    changed++;
                }
                if (person.phone != null)
                {
                    theperson.phone = person.phone;
                    changed++;
                }
                if (person.password != null)
                {
                    theperson.password = EncryptionHelper.GenerateSHA384String(person.password);
                    changed++;
                }
                if (person.role != null)
                {
                    theperson.role = Role.id;
                    changed++;
                }
                if(person.isActive != null)
                {
                    theperson.isActive = person.isActive??true;
                }
                theperson.modificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return ($"{changed} elements has been changed");
            }
        }
    }
}
