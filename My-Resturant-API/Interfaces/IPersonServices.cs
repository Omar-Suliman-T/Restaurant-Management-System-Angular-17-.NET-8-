using My_Resturant.DTOs.Person;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface IPersonServices
    {
        Task CreatePerson(CreatePersonDTOs person);
        Task DeletePerson(int id);
        Task<List<GetPersonDTOs>> GetAllPeople();
        Task<GetPersonDTOs> GetPersonById(int personId);
        Task<string> UpdatePerson(int id, UpdatePersonDTOs person);
        Task<Person?> GetPersonByEmailAsync(string email);
    }
}
