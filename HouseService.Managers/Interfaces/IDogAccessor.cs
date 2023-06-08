using HouseService.Accessors.Entities;
using HouseService.Accessors.Entities.Dtos;

namespace HouseService.Managers.Interfaces
{
    public interface IDogAccessor
    {
        Task<IEnumerable<Dog>> GetAllDogsAsync();
        Task<Dog> GetDogByIdAsync(int id);
        Task CreateDogAsync(Dog dog);
        Task UpdateDogAsync(Dog dog);
        Task DeleteDogAsync(int id);
        Task<IEnumerable<Dog>> GetDogsAsync(DogsFilterPaginationDto dogsFilterPaginationDto);
    }
}